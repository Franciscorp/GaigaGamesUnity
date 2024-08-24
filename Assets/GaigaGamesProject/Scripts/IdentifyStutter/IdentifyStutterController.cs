using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TVContent;
using static UtilsDialogues;

public class IdentifyStutterController : MonoBehaviour
{
    private int currentStage = 1;

    public GameObject VideDialoguePanel;
    public VideUIManager dialogueManager;
    public GameObject StartButton;
    public TVContent TVContent;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialGameStatus();
        StartIntroduction();
    }

    private void SetInitialGameStatus()
    {
        currentStage = 1;
        VideDialoguePanel.SetActive(true);
        StartButton.SetActive(false);

        if (dialogueManager != null)
            dialogueManager.dialogueEvent.AddListener(DialogueCompleted);
    }

    private void DialogueCompleted()
    {
        currentStage = 2;
        Debug.Log("Dialogue Completed");
        StartButton.SetActive(true);
    }

    private void StartIntroduction()
    {
        dialogueManager.SetupAndStartDialogue(GetDialogueKey(IdentifyStutterDialogues.GameIntroduction));
    }

    public void StartGame()
    {
        StartButton.SetActive(false);
        dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.GameIntroduction2));
        TVContent.UpdateContent(TVContentID.FriendTalking);
    }


    public void AskQuestion()
    {
        dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.Question1));
    }

}
