using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UtilsSpeechMachine;


public class DialogueManager : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject continueIcon;
    public Animator continueAnimation;

    public string[] dialogue;
    public List<string> dialogueKeys;
    public float wordSpeed;
    public UnityEvent OnGameIsOverDialogueCompleted;
    public UnityEvent OnIntroductionDialogueCompleted;

    private bool isDialogueActive;
    private int index;
    private bool isGameCompleted = false;
    private bool didIntroductionPlay = false;
    private bool isIntroductionCompleted = false;
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

        UpdateDialogueDataStructure();

        ResetText();
    }

    //void Update()
    //{
    //    // Works on tablet, because it simulates the touch with mouse
    //    if (Input.GetMouseButtonDown(0))
    //    {
            
    //    }    
    //}

    public void OnDialogueClick()
    {
        if (dialogueText == null || dialogue == null)
            return;

        // TODO CHECK if crashes
        if (dialogue.Length < index)
            return;

        if (!isDialogueActive)
            dialogue = new string[] { "Diálogo temporário para apresentar a próxima pista..." };

        if (dialogueText.text.Length < dialogue[index].Length)
        {
            CompleteTyping();
        }
        else if (dialogueText.text == dialogue[index])
        {
            AudioManager.Instance.PlayOneShot(FModEvents.Instance.buttonClick);
            NextLine();
        }
        else
        {
            //dialogue = new string[] { "Diálogo temporário para apresentar a próxima pista..."};
            // note: doesn't work here, because it is the activate dialogue
            DisableDialogue();
        }
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
        StopContinueAnimation();
        //dialoguePanel.SetActive(false);
    }

    private void CompleteTyping()
    {
        StopCoroutine(typingCoroutine);
        dialogueText.text = dialogue[index];
        PlayContinueAnimation();
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
            AudioManager.Instance.PlayOneShot(FModEvents.Instance.typingSFX);
            yield return new WaitForSeconds(wordSpeed);
        }

        // Sets the continue button to active
        if (dialogueText.text == dialogue[index])
            PlayContinueAnimation();
    }

    public void NextLine()
    {
        //Debug.Log("[DialogueManager] NextLine");
        StopContinueAnimation();

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartTyping();
        }
        else
        {
            DisableDialogue();
            if (isGameCompleted)
                FinishGameMode();
            if (didIntroductionPlay && !isIntroductionCompleted)
                FinishIntroduction();
        }
    }

    // TODO decouple, it is game logic
    private void FinishGameMode()
    {
        //SceneLoader.Load(SceneLoader.Scene.GamesMenu);
        OnGameIsOverDialogueCompleted.Invoke();
    }

    private void FinishIntroduction()
    {
        didIntroductionPlay = true;
        isIntroductionCompleted = true;
        OnIntroductionDialogueCompleted.Invoke();
    }

    public void OnDialogueEvent(DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        Debug.Log("dialogueTypeEvent = " + dialogueTypeEvent + " speechElementID = " + speechElementID);
        FormDialogueKeySequence(dialogueTypeEvent, speechElementID);
        ActivateDialogue();
    }

    // This interprets the signal receive and selects next keys for dialogue
    private void FormDialogueKeySequence(DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        if (speechElementID != SpeechElements.None)
        {
            if (dialogueTypeEvent == DialogueEventType.Suggestion)
            {
                // TODO moca de sono check if it makes sense
                if (isDialogueActive)
                    return;

                // add suggestion before presenting it
                dialogue = GetRandomDialogueFromList(dialogueDataStructure.speechMachineDialogues.askSuggestion);

                // from the list of speech Elements that exist, take out all the avaiable suggestions
                var speechElementSuggestions = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.Suggestion);
                // from that suggestion, remove one list of dialogues at random
                //dialogue = GetRandomDialogueFromList(speechElementSuggestions);
                dialogue = dialogue.Concat(GetRandomDialogueFromList(speechElementSuggestions)).ToArray();

                AudioManager.Instance.PlayOneShot(FModEvents.Instance.presentSuggestionSFX);
                ActivateDialogue();
            }

            if (dialogueTypeEvent == DialogueEventType.RightAnswer)
            {
                var speechElementRightAnswer = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.RightAnswer);
                
                dialogue = GetRandomDialogueFromList(speechElementRightAnswer);
                AudioManager.Instance.PlayOneShot(FModEvents.Instance.rightAnswerSFX);
                PlaySoundAccordingToElement(speechElementID);
                NextLine();
            }

            if (dialogueTypeEvent == DialogueEventType.WrongAnswer)
            {
                var speechElementWrongAnswer = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.WrongAnswer);

                dialogue = GetRandomDialogueFromList(speechElementWrongAnswer);
                AudioManager.Instance.PlayOneShot(FModEvents.Instance.wrongAnswerSFX);
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
                    didIntroductionPlay = true;
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

    private void PlaySoundAccordingToElement(SpeechElements speechElementID)
    {
        switch (speechElementID)
        {
            case SpeechElements.Lungs:
                AudioManager.Instance.PlayOneShot(FModEvents.Instance.breathingSFX);
                break;
        }
    }

    private string[] GetRandomDialogueFromList(List<Dialogue> possibleDialogues)
    {
        int randomDialogue = Random.Range(0, possibleDialogues.Count);
        return possibleDialogues[randomDialogue].GetDialoguesToArray(dialogueDataStructure.language);
    }

    private void PlayContinueAnimation()
    {
        continueIcon.SetActive(true);
        continueAnimation.Play("ContinueIndication");
    }

    private void StopContinueAnimation()
    {
        continueIcon.SetActive(false);
        continueAnimation.StopPlayback();
    }

    public void ActivateDialogue()
    {
        // If dialogue is already active, don't do anything
        if (isDialogueActive)
            return;

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
        // new string no work
        //dialogue = new string[] { "Diálogo temporário para apresentar a próxima pista..."};
        //dialoguePanel.SetActive(false);
    }
}
