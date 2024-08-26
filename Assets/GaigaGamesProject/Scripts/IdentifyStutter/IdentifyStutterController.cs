using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static CatSpriteController;
using static TVContent;
using static UtilsDialogues;

public class IdentifyStutterController : MonoBehaviour
{
    private const int MAX_AVAILABLE_ANSWERS = 9;
    private int currentStage = 1;
    private int wrongAnswers = 0;
    private int rightAnswers = 0;
    private Dictionary<string, bool> availableQuestions;

    public int catSpriteDuration = 2500;
    public GameObject VideDialoguePanel;
    public VideUIManager dialogueManager;
    public GameObject StartButton;
    public TVContent TVContent;
    public CatSpriteController CatSprite;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialGameStatus();
        StartIntroduction();
    }

    #region GameLogic

    private void SetInitialGameStatus()
    {
        currentStage = 1;
        VideDialoguePanel.SetActive(true);
        StartButton.SetActive(false);

        if (dialogueManager != null)
            dialogueManager.dialogueEvent.AddListener(DialogueCompleted);

        availableQuestions = new Dictionary<string, bool>{
            { "IdentifyStutterQuestion1", true },
            { "IdentifyStutterQuestion2", true },
            { "IdentifyStutterQuestion3", true },
            { "IdentifyStutterQuestion4", true },
            { "IdentifyStutterQuestion5", true },
            { "IdentifyStutterQuestion6", true },
            { "IdentifyStutterQuestion7", true },
            { "IdentifyStutterQuestion8", true },
            { "IdentifyStutterQuestion9", true }
        };

        //doTempShit();
    }

    private void doTempShit()
    {
        PlayerInformation playerInformation = new PlayerInformation();
        playerInformation.SetGender(Utils.Gender.Male);
        playerInformation.SetCharacterName("Franciscorp");
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
        Debug.Log("AskQuestion. Number of Answers given: " + rightAnswers);
        CatSprite.UpdateContent(CatSpriteID.NormalCat);
        
        if (rightAnswers < MAX_AVAILABLE_ANSWERS)
        {
            IdentifyStutterDialogues currentQuestion = GetAvailableQuestionEnum();
            Debug.Log(currentQuestion);
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(currentQuestion));
            //dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.Question2));
        }
        else
        {
            Debug.Log("End game");
        }
    }

    public void RightAnswer()
    {
        rightAnswers++;
        Debug.Log("RightAnswer");
        AskQuestion();
    }

    public void WrongQuestion()
    {
        wrongAnswers++;
        Debug.Log("WrongQuestion");
    }

    #endregion

    #region GameEvents

    public void CatStopsTV()
    {
        PlayCatSprite(CatSpriteID.StopCat);
    }

    public void PlayConfusedCat()
    {
        PlayCatSprite(CatSpriteID.ConfusedCat);
    }

    public void PlayHappyCat()
    {
        PlayCatSprite(CatSpriteID.HappyCat);
    }


    #endregion

    #region Auxiliary

    public async void PlayCatSprite(CatSpriteID spriteToPlay)
    {
        Debug.Log("PlayCatSprite");
        CatSprite.UpdateContent(spriteToPlay);
        await Task.Delay(catSpriteDuration);
        CatSprite.UpdateContent(CatSpriteID.NormalCat);
    }

    private string GetRandomAvailableQuestion()
    {
        System.Random rand = new System.Random();
        var temp = availableQuestions.Where(x => x.Value == true);
        string questionID = temp.ElementAt(rand.Next(temp.Count())).Key;
        Debug.Log(questionID);
        availableQuestions[questionID] = false;
        return questionID;
    }

    private IdentifyStutterDialogues GetAvailableQuestionEnum()
    {
        string dialogueID = GetRandomAvailableQuestion();
        return GetDialogueByValueID(dialogueID);
    }

    #endregion
}
