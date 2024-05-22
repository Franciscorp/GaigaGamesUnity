using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UtilsSpeechMachine;


public class DialogueManager : MonoBehaviour
{
    public GameObject gameManager;
    public DialogueDataStructure dialogueDataStructure;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject continueButton;
    public float wordSpeed;
    public bool isDialogueActive;

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

        // TODO TEMP
        dialogueDataStructure = new DialogueDataStructure();

        ResetText();
    }

    public void ResetText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        continueButton.SetActive(false);
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
        Debug.Log("[DialogueManager] NextLine");

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ResetText();
        }
    }

    public void OnDialogueEvent(DialogueEventType dialogueTypeEvent, SpeechElements speechElementID)
    {
        Debug.Log("dialogueTypeEvent = " + dialogueTypeEvent + " speechElementID = " + speechElementID);
        ActivateDialogue();
    }

    public void ActivateDialogue()
    {
        Debug.Log("[DialogueManager] ActivateDialogue");

        isDialogueActive = true;

        ResetText();
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());
    }

    public void DisableDialogue()
    {
        isDialogueActive = false;
        ResetText();
        dialoguePanel.SetActive(false);
    }
}
