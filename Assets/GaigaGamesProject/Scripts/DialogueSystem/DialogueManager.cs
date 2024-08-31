using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UtilsSpeechMachine;
using static Utils;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI npcName;
    public Image npcIcon;
    public GameObject continueIcon;
    public Animator continueAnimation;

    //images
    public List<Sprite> npcImages;


    public ReadyToDisplayDialogue readyToDisplayDialogue;
    //public string[] dialogue;
    public List<string> dialogueKeys;
    public float wordSpeed;
    public UnityEvent OnGameIsOverDialogueCompleted;
    public UnityEvent OnIntroductionDialogueCompleted;
    public UnityEvent OnDialogueCompleted;

    private bool isDialogueActive;
    private int index;

    private bool isDialogueCompleted = false;
    private bool isGameCompleted = false;
    private bool didIntroductionPlay = false;
    private bool isIntroductionCompleted = false;

    private DialogueDataStructure dialogueDataStructure;
    private PlayerInformation playerInformation;
    private Coroutine typingCoroutine;
    private Coroutine suggestionCoroutine;

    // To help build the dialogue system
    // https://stackoverflow.com/questions/36274948/converting-a-json-string-to-csv-using-csvhelper
    // use a json format
    // convert json to excel in future work

    // Start is called before the first frame update
    void Start()
    {
        playerInformation = new PlayerInformation();

        if (gameManager == null)
        {
            Debug.LogWarning("Game Manager not linked to Dialogue Manager");
            return;
        }
        else
        {
            // TODO temp
            try
            {
                gameManager.GetComponent<SpeechMachineGameManager>().dialogueEvent.AddListener(OnDialogueEvent);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            try
            {
                gameManager.GetComponent<IntroductionController>().dialogueEvent.AddListener(OnDialogueEvent);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            try
            {
                gameManager.GetComponent<MainGameController>().dialogueEvent.AddListener(OnDialogueEvent);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

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
        if (dialogueText == null || readyToDisplayDialogue == null)
            return;

        // TODO CHECK if crashes
        if (readyToDisplayDialogue.text.Length < index)
            return;

        // TODO check
        //if (!isDialogueActive)
            //dialogue = new string[] { "Diálogo temporário para apresentar a próxima pista..." };

        if (dialogueText.text.Length < readyToDisplayDialogue.text[index].Length)
        {
            CompleteTyping();
        }
        else if (dialogueText.text == readyToDisplayDialogue.text[index])
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
        dialogueText.text = readyToDisplayDialogue.text[index];
        PlayContinueAnimation();
    }

    private void ChangeDisplayedNpcNameAndImage()
    {
        //gameObject.GetComponent().sprite = myImage;
        if (readyToDisplayDialogue.npc == Npc.Player)
        {
            if (playerInformation.GetGender() == Gender.Male)
                readyToDisplayDialogue.npc = Npc.Boy;
            else
                readyToDisplayDialogue.npc = Npc.Girl;
        }

        npcIcon.sprite = npcImages[(int)readyToDisplayDialogue.npc];
        npcName.text = GetPortugueseTranslatedNpcList(readyToDisplayDialogue.npc);
    }

    // Checks if it was already typing something and stops it
    public void StartTyping()
    {
        ChangeDisplayedNpcNameAndImage();
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        foreach (char letter in readyToDisplayDialogue.text[index].ToCharArray())
        {
            // Adds next letter
            // Waits for a dew seconds for the next letter to be added
            dialogueText.text += letter;
            AudioManager.Instance.PlayOneShot(FModEvents.Instance.typingSFX);
            yield return new WaitForSeconds(wordSpeed);
        }

        // Sets the continue button to active
        if (dialogueText.text == readyToDisplayDialogue.text[index])
            PlayContinueAnimation();
    }

    public void NextLine()
    {
        //Debug.Log("[DialogueManager] NextLine");
        StopContinueAnimation();

        if (index < readyToDisplayDialogue.text.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartTyping();
        }
        else
        {
            DisableDialogue();
            if (isDialogueCompleted)
                DialogueIsFinished();
            if (isGameCompleted)
                FinishGameMode();
            if (didIntroductionPlay && !isIntroductionCompleted)
                FinishIntroduction();
        }
    }

    private void DialogueIsFinished()
    {
        isDialogueCompleted = false;
        OnDialogueCompleted.Invoke();
    }

    private void FinishGameMode()
    {
        OnGameIsOverDialogueCompleted.Invoke();
    }

    private void FinishIntroduction()
    {
        didIntroductionPlay = true;
        isIntroductionCompleted = true;
        OnIntroductionDialogueCompleted.Invoke();
    }

    private void OnDialogueEvent(Scene scene, DialogueEventType dialogueTypeEvent)
    {
        InterpretDialogueEvent(scene, dialogueTypeEvent, SpeechElements.None);
    }

    private void OnDialogueEvent(Scene scene, DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        InterpretDialogueEvent(scene, dialogueTypeEvent, speechElementID);
    }

    private void InterpretDialogueEvent(Scene scene, DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        Debug.Log("Scene = " + scene + " dialogueTypeEvent = " + dialogueTypeEvent + " speechElementID = " + speechElementID);

        switch (scene)
        {
            case Scene.Introduction:
            case Scene.MainGame:
                FormComonDialogueKeySequence(scene, dialogueTypeEvent, speechElementID);
                break;
            case Scene.SpeechMachine:
                // If Speech Element exists, plays interprets type of Speech Dialogue
                if (speechElementID != SpeechElements.None)
                    FormSpeechMachineElementDialogueKeySequence(dialogueTypeEvent, speechElementID);
                else
                    FormComonDialogueKeySequence(scene, dialogueTypeEvent, speechElementID);
                break;
            default:
                Debug.LogWarning("Scene not identified correctly on Dialogue Manager");
                break;
        }

        ActivateDialogue();
    }

    private void FormComonDialogueKeySequence(Scene scene, DialogueEventType dialogueTypeEvent, SpeechElements speechElementID = SpeechElements.None)
    {
        switch (dialogueTypeEvent)
        {
            case DialogueEventType.None:
                readyToDisplayDialogue = new ReadyToDisplayDialogue(Npc.Error, new string[] { "Error, None type of error found" });  
                break;

            case DialogueEventType.NextDialogueLine:
                OnDialogueClick();
                break;

            case DialogueEventType.Intro:
                readyToDisplayDialogue = DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
                didIntroductionPlay = true;
                break;

            case DialogueEventType.LongerIntro:
                readyToDisplayDialogue = DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
                isDialogueCompleted = true;
                break;

            case DialogueEventType.AskName:
            case DialogueEventType.AskGender:
            case DialogueEventType.GenderEntered:
                readyToDisplayDialogue = DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
                isDialogueCompleted = true;
                break;

            case DialogueEventType.NameEntered:
                readyToDisplayDialogue = DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
                isDialogueCompleted = true;
                break;

            //case DialogueEventType.AskGender:
            //    dialogue = DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
            //    break;

            //case DialogueEventType.GenderEntered:
            //    dialogue = DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
            //    break;

            case DialogueEventType.Conclusion:

                readyToDisplayDialogue =  DialogueFetcher.GetRequestedRandomDialogueFromList(dialogueDataStructure, dialogueTypeEvent, scene);
                readyToDisplayDialogue.text = readyToDisplayDialogue.text.Concat(readyToDisplayDialogue.text).ToArray();
                isGameCompleted = true;
                break;

            default:
                readyToDisplayDialogue = new ReadyToDisplayDialogue(Npc.Error, new string[] { "Default value with no speech element, This is an error" });
                break;
        }
    }

    private void FormSpeechMachineElementDialogueKeySequence(DialogueEventType dialogueTypeEvent, SpeechElements speechElementID = SpeechElements.None)
    {
        if (dialogueTypeEvent == DialogueEventType.Suggestion)
        {
            // TODO moca de sono check if it makes sense
            if (isDialogueActive)
                return;

            // add suggestion before presenting it
            readyToDisplayDialogue = GetRandomDialogueFromList(dialogueDataStructure.speechMachineDialogues.askSuggestion);

            // from the list of speech Elements that exist, take out all the avaiable suggestions
            var speechElementSuggestions = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.Suggestion);
            // from that suggestion, remove one list of dialogues at random
            //dialogue = GetRandomDialogueFromList(speechElementSuggestions);
            readyToDisplayDialogue.text = readyToDisplayDialogue.text.Concat(readyToDisplayDialogue.text).ToArray();

            AudioManager.Instance.PlayOneShot(FModEvents.Instance.presentSuggestionSFX);
            ActivateDialogue();
        }

        if (dialogueTypeEvent == DialogueEventType.RightAnswer)
        {
            var speechElementRightAnswer = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.RightAnswer);

            readyToDisplayDialogue = GetRandomDialogueFromList(speechElementRightAnswer);
            AudioManager.Instance.PlayOneShot(FModEvents.Instance.rightAnswerSFX);
            PlaySoundAccordingToElement(speechElementID);
            NextLine();
        }

        if (dialogueTypeEvent == DialogueEventType.WrongAnswer)
        {
            var speechElementWrongAnswer = dialogueDataStructure.speechMachineDialogues.GetSpeechElementDialogues(speechElementID, DialogueEventType.WrongAnswer);

            readyToDisplayDialogue = GetRandomDialogueFromList(speechElementWrongAnswer);
            AudioManager.Instance.PlayOneShot(FModEvents.Instance.wrongAnswerSFX);
            NextLine();
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

    private ReadyToDisplayDialogue GetRandomDialogueFromList(List<Dialogue> possibleDialogues)
    {
        int randomDialogue = UnityEngine.Random.Range(0, possibleDialogues.Count);
        Dialogue currentDialogue = possibleDialogues[randomDialogue];
        return new ReadyToDisplayDialogue(currentDialogue.npc, currentDialogue.GetDialoguesToArray(dialogueDataStructure.language));
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
