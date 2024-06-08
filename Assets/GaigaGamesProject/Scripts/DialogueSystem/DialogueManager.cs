using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UtilsSpeechMachine;


public class DialogueManager : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject dialoguePanel;
    public GameObject ChoicePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    public List<string> dialogueKeys;
    private int index;

    public GameObject continueButton;
    public bool isDialogueActive;
    public float wordSpeed;

    private bool isGameCompleted = false;
    private bool isChoicePanelActive = false;
    private DialogueDataStructure dialogueDataStructure;
    private Coroutine typingCoroutine;
    private Coroutine suggestionCoroutine;

    // To help build the dialogue system
    // https://stackoverflow.com/questions/36274948/converting-a-json-string-to-csv-using-csvhelper
    // use a json format
    // convert json to excel in future work

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("Game Manager not linked to Dialogue Manager");
            return;
        }
        else
        {
            // TODO temp
            gameManager.GetComponent<SpeechMachineGameManager>().dialogueEvent.AddListener(OnDialogueEvent);
            Debug.Log("[DialogueManager] Dialogue Events are connected");
        }

        DisableChoicePanel();

        UpdateDialogueDataStructure();

        ResetText();
    }

    public void UpdateDialogueDataStructure()
    {
        // TODO Upate language in data structure
        //dialogueDataStructure.language = LocalizationSettings.SelectedLocale;

        if (gameManager != null)
        {
            dialogueDataStructure = DialogueSafeStateController.Instance.dialogueStructure;
            //dialogueDataStructure.language = Utils.Language.Portuguese;

            if (!dialogueDataStructure.IsThereDialoguesAvaiable())
                Debug.LogError("[DialogueManager] - No Speech Machine Dialogues Avaiable");
        }
        else
        {
            Debug.LogError("[DialogueManager] - game manager is null");
        }
    }

    public void ResetText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        continueButton.SetActive(false);

        if (isChoicePanelActive == false)
            ChoicePanel.SetActive(false);
    }


    // Checks if it was already typing something and stops it
    public void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            // Adds next letter
            // Waits for a dew seconds for the next letter to be added
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        // Sets the continue button to active
        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextLine()
    {
        //Debug.Log("[DialogueManager] NextLine");

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartTyping();
        }
        else
        {
            DisableDialogue();
            DisableChoicePanel();
            if (isGameCompleted)
                FinishGameMode();
        }
    }

    // TODO decouple, it is game logic
    private void FinishGameMode()
    {
        SceneLoader.Load(SceneLoader.Scene.GamesMenu);
    }

    public void OnDialogueEvent(DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        Debug.Log("dialogueTypeEvent = " + dialogueTypeEvent + " speechElementID = " + speechElementID);
        FormDialogueKeySequence(dialogueTypeEvent, speechElementID);
        if (!isDialogueActive)
            ActivateDialogue();
    }

    // This interprets the signal receive and selects next keys for dialogue
    private void FormDialogueKeySequence(DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        if (speechElementID != SpeechElements.None)
        {
            if (dialogueTypeEvent == DialogueEventType.Suggestion)
            {
                // from the list of speech Elements that exist, take out all the avaiable suggestions
                var speechElementSuggestions = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.Suggestion);
                // from that suggestion, remove one list of dialogues at random
                dialogue = GetRandomDialogueFromList(speechElementSuggestions);
                // goes to next line but disables chat
                NextLine();
                DisableChoicePanel();
            }

            if (dialogueTypeEvent == DialogueEventType.RightAnswer)
            {
                var speechElementRightAnswer = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.RightAnswer);

                dialogue = GetRandomDialogueFromList(speechElementRightAnswer);
                NextLine();
            }

            if (dialogueTypeEvent == DialogueEventType.WrongAnswer)
            {
                var speechElementWrongAnswer = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.WrongAnswer);

                dialogue = GetRandomDialogueFromList(speechElementWrongAnswer);
                NextLine();
            }

        }

        if (SpeechElements.None == speechElementID)
        {
            switch (dialogueTypeEvent)
            {
                case DialogueEventType.None:
                    dialogue = new string[] { "Error, None type of error found" };
                    break;

                case DialogueEventType.Intro:
                    dialogue = GetRandomDialogueFromList(dialogueDataStructure.speechMachineDialogues.introduction);
                    break;

                case DialogueEventType.Help:
                    if (!isDialogueActive)
                    {
                        dialogue = GetRandomDialogueFromList(dialogueDataStructure.speechMachineDialogues.askSuggestion);
                        EnableChoicePanel();
                    }
                    break;

                case DialogueEventType.Conclusion:
                    dialogue = dialogue.Concat(GetRandomDialogueFromList(dialogueDataStructure.speechMachineDialogues.conclusion)).ToArray();
                    isGameCompleted = true;
                    break;

                default:
                    dialogue = new string[]{ "Default value with no speech element, This is an error" };
                    break;
            }
        }
    }

    private string[] GetRandomDialogueFromList(List<Dialogue> possibleDialogues)
    {
        int randomDialogue = Random.Range(0, possibleDialogues.Count);
        return possibleDialogues[randomDialogue].GetDialoguesToArray(dialogueDataStructure.language);
    }

    public void ActivateDialogue()
    {
        //Debug.Log("[DialogueManager] ActivateDialogue");

        isDialogueActive = true;

        ResetText();
        dialoguePanel.SetActive(true);
        StartTyping();
    }

    public void DisableDialogue()
    {
        isDialogueActive = false;
        ResetText();
        dialoguePanel.SetActive(false);
    }

    public void DisableChoicePanel()
    {
        isChoicePanelActive = false;
        ChoicePanel.SetActive(isChoicePanelActive);
    }

    public void EnableChoicePanel()
    {
        isChoicePanelActive = true;
        ChoicePanel.SetActive(isChoicePanelActive);
    }
}
