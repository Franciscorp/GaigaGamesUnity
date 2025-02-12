using FMODUnity;
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
    private bool isObjectivePresented = false;
    private bool isErrorMessagePresented = false;
    private int currentQuestionVoiceLine = -1;
    IdentifyStutterDialogues currentQuestion;
    private bool isEndGame = false;
    private Dictionary<string, bool> availableQuestions;
    private Dictionary<VoiceLines, EventReference> questionsVoiceLines;
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
        isObjectivePresented = false;
        isErrorMessagePresented = false;

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

        CreateVoiceLineDictionary();

        // play audio
        AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.IdentifyStutterMusic);
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
        //if (rightAnswers < 1)
        {
            // NOTE: will not be used
            //AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.tvTalkingSFX);
            TVContent.UpdateContent(TVContentID.FriendTalking);
            currentQuestion = GetAvailableQuestionEnum();
            Debug.Log(currentQuestion);
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(currentQuestion));
            //dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.Question1));
            currentQuestionVoiceLine = (int)currentQuestion - 1;
            Debug.Log($"Voice Line ID = {currentQuestionVoiceLine}");
            //currentQuestionVoiceLine = 1;
            PlayVoiceLine(currentQuestionVoiceLine);
        }
        else
        {
            AudioManager.Instance.PlayOneShot(FModEvents.Instance.conclusionPartySFX);
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.Conclusion));
            Debug.Log("End game");
        }
    }

    // When it displays the first error message, it presents this case instead of a new one
    public void RepeatPreviousQuestion()
    {
        Debug.Log(currentQuestion);
        dialogueManager.SetupAndRestartDialogue(GetDialogueKey(currentQuestion));
        currentQuestionVoiceLine = (int)currentQuestion - 1;
        PlayVoiceLine(currentQuestionVoiceLine);
    }

    public void RightAnswer()
    {
        rightAnswers++;
        Debug.Log("RightAnswer");
        currentQuestionVoiceLine = -1;
        
        if (!isObjectivePresented)
        {
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.PresentObjective));
            isObjectivePresented = true;
        }
        else
            AskQuestion();
    }

    public void WrongQuestion()
    {
        wrongAnswers++;
        Debug.Log("WrongQuestion");
        // TODO very inneficient, but a working shortcut
        playerInformation.identifyStuterGameInfo.SetIdentifyStutterWrongAnswersDone(wrongAnswers);

        if (!isErrorMessagePresented)
        {
            isErrorMessagePresented = true;
            dialogueManager.SetupAndRestartDialogue(GetDialogueKey(IdentifyStutterDialogues.FirstError));
        }
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
        //SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
        // TODO delete
        await Task.Delay(1000);
        Application.Quit();
    }


    #endregion

    #region GameEvents

    public void CatStopsTV()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.stopTvCatSFX);
        PlayCatSprite(CatSpriteID.CatMeows);
    }

    public void PlayVoiceLine(int voiceLineID)
    {
        EventReference voiceLineEvent = questionsVoiceLines[(VoiceLines)voiceLineID];
        AudioManager.Instance.PlayVoiceLine(voiceLineEvent);
    }
    public void PlayCurrentVoiceLine()
    {
        if (currentQuestionVoiceLine == -1)
        {
            Debug.LogError("No voice line available");
            return;
        }

        EventReference voiceLineEvent = questionsVoiceLines[(VoiceLines)currentQuestionVoiceLine];
        AudioManager.Instance.PlayVoiceLine(voiceLineEvent);
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

    private void CreateVoiceLineDictionary()
    {
        questionsVoiceLines = new Dictionary<VoiceLines, EventReference>
        {
            { VoiceLines.ISVoiceLineQuestion1, FModEvents.Instance.ISVoiceLineQuestion1},
            { VoiceLines.ISVoiceLineQuestion2, FModEvents.Instance.ISVoiceLineQuestion2},
            { VoiceLines.ISVoiceLineQuestion3, FModEvents.Instance.ISVoiceLineQuestion3},
            { VoiceLines.ISVoiceLineQuestion4, FModEvents.Instance.ISVoiceLineQuestion4},
            { VoiceLines.ISVoiceLineQuestion5, FModEvents.Instance.ISVoiceLineQuestion5},
            { VoiceLines.ISVoiceLineQuestion6, FModEvents.Instance.ISVoiceLineQuestion6},
            { VoiceLines.ISVoiceLineQuestion7, FModEvents.Instance.ISVoiceLineQuestion7},
            { VoiceLines.ISVoiceLineQuestion8, FModEvents.Instance.ISVoiceLineQuestion8},
            { VoiceLines.ISVoiceLineQuestion9, FModEvents.Instance.ISVoiceLineQuestion9}
        };
    }

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
