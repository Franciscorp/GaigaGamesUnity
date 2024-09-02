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
    private const int NUMBER_OF_WRONG_ANSWERS_TO_DISPLAY_GOOD_CONCLUSION = 3;
    private int currentStage = 1;
    private int wrongAnswers = 0;
    private int rightAnswers = 0;
    private bool isEndGame = false;
    private Dictionary<string, bool> availableQuestions;
    private PlayerInformation playerInformation;

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
        playerInformation = new PlayerInformation();
        // TODO delete
        playerInformation.CreateBasePlayerInfo();
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

        // play audio
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.IdentifyStutterMusic);
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
        PlayNewsIntro();
        dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.GameIntroduction2));
    }

    public void AskQuestion()
    {
        Debug.Log("AskQuestion. Number of Answers given: " + rightAnswers);
        CatSprite.UpdateContent(CatSpriteID.NormalCat);

        if (rightAnswers < MAX_AVAILABLE_ANSWERS)
        {
            AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.tvTalkingSFX);
            TVContent.UpdateContent(TVContentID.FriendTalking);
            IdentifyStutterDialogues currentQuestion = GetAvailableQuestionEnum();
            Debug.Log(currentQuestion);
            //dialogueManager.SetupAndRestartDialogue(GetDialogueKey(currentQuestion));
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.Question1));
        }
        else
        {
            AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.conclusionPartySFX);
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.Conclusion));
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
        // TODO very inneficient, but a working shortcut
        playerInformation.identifyStuterGameInfo.SetIdentifyStutterWrongAnswersDone(wrongAnswers);
    }

    public void PresentConclusion()
    {
        if (rightAnswers < MAX_AVAILABLE_ANSWERS)
        {
            Debug.LogWarning("Something went wrong. Not all questions were asked");
        }

        playerInformation.identifyStuterGameInfo.EndIdentifyStutterGame(wrongAnswers);

        if (wrongAnswers == 0)
        {
            Debug.Log("Best Conclusion");
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.BestConclusion));
        }
        else if (wrongAnswers <= NUMBER_OF_WRONG_ANSWERS_TO_DISPLAY_GOOD_CONCLUSION)
        {
            Debug.Log("Good Conclusion");
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.GoodConclusion));
        }

        else
        {
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.BadConclusion));
            Debug.Log("Bad Conclusion");
        }
    }


    public void EndGame()
    {
        if (isEndGame)
            return;

        isEndGame = true;
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.gameOverSFX);
        Debug.Log("End Game");
        playerInformation.SetCurrentGameStage(Utils.GameStages.IdentifyStutter);
        WaitForJingleToEnd();
    }

    public async void WaitForJingleToEnd()
    {
        await Task.Delay(4800);
        Debug.Log("WaitForJingleToEnd");
        SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }


    #endregion

    #region GameEvents

    public void CatStopsTV()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.stopTvCatSFX);
        PlayCatSprite(CatSpriteID.CatMeows);
    }

    public void PlayConfusedCat()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.wrongAnswerCatSFX);
        //AudioManager.Instance.PlayOneShot(FModEvents.Instance.wrongAnswerAltSFX);
        PlayCatSprite(CatSpriteID.ConfusedCat);
        // TODO bad code, just to avoid making actions in vide
        EnableContinueButton();
    }

    public void PlayHappyCat()
    {
        PlayCatSprite(CatSpriteID.HappyCat);
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.rightAnswerCatSFX);
        //AudioManager.Instance.PlayOneShot(FModEvents.Instance.rightAnswerAltSFX);
        // TODO bad code, just to avoid making actions in vide
        EnableContinueButton();
    }

    public void ChangeTVToReporterAndJoao()
    {
        TVContent.UpdateContent(TVContentID.BothTalking);
    }

    public void ChangeTVToReporter()
    {
        TVContent.UpdateContent(TVContentID.ReporterTalking);
    }


    #endregion

    #region Auxiliary

    // TODO delete maybe
    public void EnableContinueButton()
    {
        dialogueManager.EnableContinueButton();
    }

    public void PlayNewsIntro()
    {
        Debug.Log("PlayNewsIntro");
        TVContent.UpdateContent(TVContentID.News);
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.newsIntroSFX);
        // 2600 is the tv intro length
        //await Task.Delay(2600);
        //TVContent.UpdateContent(TVContentID.FriendTalking);
    }

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
