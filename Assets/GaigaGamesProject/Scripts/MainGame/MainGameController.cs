using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using static Utils;
using static UtilsDialogues;
using static UtilsSpeechMachine;

public class MainGameController : MonoBehaviour
{
    // Game Vars
    private PlayerInformation playerInformation;
    private int currentDialogue = 0;

    [field: Header("Game Objects")]
    // Game Objects 
    public GameObject VideDialoguePanel;
    private VideUIManager videDialogueManager;
    public GameObject MobileControls;
    public GameObject GamesPanel;
    public GamePanelController gamePanelController;
    private GameObject gameObjectCamera;
    private Camera camera;
    private Animator cameraAnimator;
    public GameObject Grandpa;
    public List<InteractionController> interactions;

    [field: Header("Sprites")]
    // Player and Tobias
    public GameObject Player;
    public GameObject grandpaKitchen;
    public GameObject spriteBoy;
    public GameObject spriteGirl;
    public CatSpriteController TobiasSprite;

    [field: Header("Animations")]
    // Events nad Playlable directors
    public PlayableDirector Scene1;
    public PlayableDirector TobiasAppearsTimeline;
    public PlayableDirector TobiasDissapearsTimeline;
    public PlayableDirector AfterIntroductionTimeline;



    private void Awake()
    {
        Utils.SetScreenAlwaysOn();
        playerInformation = new PlayerInformation();
        //temp();
    }


    public void temp()
    {
        //playerInformation.SetCurrentGameStage(GameStages.SpeechMachineDone);
        playerInformation.SetCurrentGameStage(GameStages.IntroductionCompleted);
        playerInformation.SetCharacterName("Jogador");
        playerInformation.SetGender(Gender.Female);
    }

    private void Start()
    {
        ShowCorrectGenderSprite();

        gameObjectCamera = GameObject.FindGameObjectsWithTag("MainCamera").FirstOrDefault();
        if (gameObjectCamera != null)
        {
            camera = gameObjectCamera.GetComponent<Camera>();
            cameraAnimator = gameObjectCamera.GetComponent<Animator>();
        }
        else
            Debug.LogError("Can't find camera in Main game");

        if (VideDialoguePanel != null)
            videDialogueManager = VideDialoguePanel.GetComponentInChildren<VideUIManager>();
        else
            Debug.LogError("VideDialoguePanel is null");


        gamePanelController = GamesPanel.GetComponent<GamePanelController>();
        if (gamePanelController != null)
            gamePanelController.buttonIDEvent.AddListener(GameButtonPressed);
        else
            Debug.LogWarning("gamePanelController not available");

        AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.mainGameMusic);
        SetInitialGameStatus();
        SetGameStage();
    }

    private void SetInitialGameStatus()
    {
        playerInformation = new PlayerInformation();
        Debug.Log("Camera Idle");
        currentDialogue = 0;

        Grandpa.SetActive(false);
        VideDialoguePanel.SetActive(false);
        SetActiveMobileControls(false);
    }

    #region GameStages

    public void SetIntroductionCompletedGameStage()
    {
        currentDialogue = 5;
        SetActiveMobileControls(true);
        Debug.Log("Tobias transform = " + TobiasSprite.transform.localPosition);
        Transform tobiasTransform = TobiasSprite.transform;
        tobiasTransform.localPosition = new Vector3(19.63f, 1.94f, 0f);
    }

    public void SetGrandmaIntroductionCompletedGameStage()
    {
        Debug.Log("SetGrandmaIntroductionCompletedGameStage");

        Grandpa.SetActive(true);
        grandpaKitchen.SetActive(false);
        SetActiveMobileControls(true);

        TobiasSprite.transform.localPosition = new Vector3(15.55f, 5.02f, 0f);
        Player.transform.localPosition = new Vector3(21.63f, -0.3699484f, 3.5f);
    }

    public void SetSpeechMachineDoneGameStage()
    {
        Debug.Log("SetSpeechMachineDoneGameStage");

        Grandpa.SetActive(true);
        grandpaKitchen.SetActive(false);
        SetActiveMobileControls(true);

        TobiasSprite.transform.localPosition = new Vector3(15.55f, 5.02f, 0f);
        // real
        //Player.transform.localPosition = new Vector3(20f, 5.44f, 3.5f);
        // tmp
        Player.transform.localPosition = new Vector3(10.75f, -0.37f, 3.5f);
    }

    public void SetIdentifyStutterGameStage()
    {
        Debug.Log("SetIdentifyStutterGameStage");

        Grandpa.SetActive(true);
        grandpaKitchen.SetActive(false);
        SetActiveMobileControls(true);

        TobiasSprite.transform.localPosition = new Vector3(15.55f, 5.02f, 0f);
        Player.transform.localPosition = new Vector3(10.75f, -0.37f, 3.5f);
    }

    #endregion

    #region GameControl

    public void SetGameStage()
    {
        Debug.Log("Set game stage = " + playerInformation.GetCurrentGameStage());

        switch (playerInformation.GetCurrentGameStage())
        {
            case GameStages.Beginning:
                Scene1.Play();
                break;
            case GameStages.IntroductionCompleted:
                SetIntroductionCompletedGameStage();
                break;
            case GameStages.GrandmaIntroductionCompleted:
                SetGrandmaIntroductionCompletedGameStage();
                break;
            case GameStages.SpeechMachineDone:
                SetSpeechMachineDoneGameStage();
                break;
            case GameStages.IdentifyStutter:
                SetIdentifyStutterGameStage();
                break;
            default:
                Debug.LogError("SetGameStage default value. Shouldn't happen");
                break;
        }
    }

    // Called by the first timeline signal
    public async void StartGameIntroduction()
    {
        cameraAnimator.Play("CameraOut");
        await Task.Delay(300);
        VideDialoguePanel.SetActive(true);
        videDialogueManager.SetupAndStartDialogue(GetMainGameDialogueKey(MainGameDialogues.MainGameStory1));
        await Task.Delay(700);
        currentDialogue++;
    }

    public void DialogueCompleted()
    {
        Debug.Log("DialogueCompleted - currentDialogue: " + currentDialogue);

        switch (currentDialogue)
        {
            case 1:
                DisplayPhotos();
                break;
            case 2:
                TobiasAppears();
                break;
            case 3:
                TobiasDissapears();
                break;
            case 4:
                TobiasDissapears();
                break;
            default:
                Debug.LogWarning("Current scene was not planned. Error in Main Game Controller");
                break;
        }
    }

    public async void DisplayPhotos()
    {
        videDialogueManager.SetupAndRestartDialogue(GetMainGameDialogueKey(MainGameDialogues.MainGameStory2));
        currentDialogue++;
    }

    public void TobiasAppears()
    {
        videDialogueManager.DisableContinueButton();
        TobiasAppearsTimeline.Play();
        currentDialogue++;
    }

    public async void TobiasMeows()
    {
        TobiasSprite.UpdateContent(CatSpriteController.CatSpriteID.CatMeows);
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.stopTvCatSFX);
        await Task.Delay(500);
        videDialogueManager.SetupAndRestartDialogue(GetMainGameDialogueKey(MainGameDialogues.MainGameStory3));
        await Task.Delay(500);
        TobiasSprite.UpdateContent(CatSpriteController.CatSpriteID.NormalCat);
        currentDialogue++;
    }

    public async void TobiasDissapears()
    {
        videDialogueManager.DisableContinueButton();
        TobiasDissapearsTimeline.Play();
        await Task.Delay(1400);
        videDialogueManager.EnableContinueButton();
        videDialogueManager.SetupAndRestartDialogue(GetMainGameDialogueKey(MainGameDialogues.MainGameStory4));
        currentDialogue++;
    }

    // updates after story4 in VIDE
    public void EndIntroduction()
    {
        Debug.Log("EndIntroduction");
        playerInformation.SetCurrentGameStage(GameStages.IntroductionCompleted);
        DisableDialogue();
    }

    public void GrandmaInteraction()
    {
        if (playerInformation.GetCurrentGameStage() == GameStages.IntroductionCompleted)
        {
            //SetActiveInteraction(false);
            ActivateDialogue(MainGameDialogues.GrandmaStory1);
            currentDialogue = 5;
        }
        else if (playerInformation.GetCurrentGameStage() == GameStages.GrandmaIntroductionCompleted)
        {
            ActivateDialogue(MainGameDialogues.GrandmaAltStory1);
        }
        else if (playerInformation.GetCurrentGameStage() == GameStages.SpeechMachineDone)
        {
            ActivateDialogue(MainGameDialogues.GrandmaAltStory2);
        }
        else
        {
        }
    }

    public void GrandpaInteraction()
    {
        Debug.Log("Set game stage = " + playerInformation.GetCurrentGameStage());

        if (playerInformation.GetCurrentGameStage() == GameStages.GrandmaIntroductionCompleted)
        {
            ActivateDialogue(MainGameDialogues.GrandpaAltStory1);
        }
        if (playerInformation.GetCurrentGameStage() == GameStages.SpeechMachineDone)
        {
            ActivateDialogue(MainGameDialogues.IdentifyStutterStory1);
        }
    }

    public void SpeechMachineInteraction()
    {
        Debug.Log("SpeechMachineInteractione = " + playerInformation.GetCurrentGameStage());

        if (playerInformation.GetCurrentGameStage() == GameStages.GrandmaIntroductionCompleted)
        {
            ActivateDialogue(MainGameDialogues.SpeechMachineStory1);
        }
        else if(playerInformation.GetCurrentGameStage() == GameStages.SpeechMachineDone)
        {
            ActivateDialogue(MainGameDialogues.SpeechMachineAlt2);
        }
        else
        {

        }
    }


    public void GameButtonPressed(int option = -1)
    {
        Debug.Log("Game button pressed with option = " + option);

        if (option == -1 || option == 0)
        {
            DisableDialogue();
        }
        if (option == 1)
        {
            //DisableDialogue();
            //await Task.Delay(400);
            gamePanelController.HideButtons();
            SceneLoader.Load(SceneLoader.Scene.SpeechMachine);
        }
        if (option == 2)
        {
            Debug.Log("GameButtonPressed = 2");
            //DisableDialogue();
            //await Task.Delay(400);
            gamePanelController.HideButtons();
            SceneLoader.Load(SceneLoader.Scene.IdentifyStutter);
        }
    }

    public async void EndFirstInteractionWithGrandma()
    {
        //SetActiveInteraction(true);
        DisableDialogue();
        AfterIntroductionTimeline.Play();
        currentDialogue = 6;
        await Task.Delay(1800);
        //SetActiveInteraction(true);
        playerInformation.SetCurrentGameStage(GameStages.GrandmaIntroductionCompleted);
        //Debug.Log("EndFirstInteractionWithGrandma = " + playerInformation.GetCurrentGameStage());
    }

    #endregion

    #region GameVisualsControl

    public void SetActiveMobileControls(bool isActive)
    {
#if UNITY_ANDROID || UNITY_IOS
            Debug.Log("This is an Android device.");
            MobileControls.SetActive(isActive);

#elif UNITY_STANDALONE || UNITY_EDITOR
            Debug.Log("This is a PC (Windows, macOS, or Linux).");
            MobileControls.SetActive(false);
#endif
    }

    public void SetActiveInteraction(bool active)
    {
        foreach(InteractionController obj in interactions)
        {
            obj.SetInteractionActive(active);
        }
    }

    public async void ActivateDialogue()
    {
        cameraAnimator.Play("CameraOut");
        await Task.Delay(300);
        VideDialoguePanel.SetActive(true);
        SetActiveMobileControls(false);
        await Task.Delay(700);
    }

    public async void ActivateDialogue(MainGameDialogues dialogueID)
    {
        cameraAnimator.Play("CameraOut");
        await Task.Delay(300);
        videDialogueManager.SetupAndStartDialogue(GetMainGameDialogueKey(dialogueID));
        VideDialoguePanel.SetActive(true);
        SetActiveMobileControls(false);
        await Task.Delay(700);
    }

    public async void DisableDialogue()
    {
        cameraAnimator.Play("CameraIn");
        await Task.Delay(300);
        VideDialoguePanel.SetActive(false);
        SetActiveMobileControls(true);
        await Task.Delay(700);
    }


    private async void TransitionCameraToDialogue()
    {
        await Task.Delay(4000);

        Debug.Log("Camera Rect = " + camera.rect);
        cameraAnimator.Play("CameraOut");

        await Task.Delay(4000);

        cameraAnimator.Play("CameraIn");
    }


    private void ShowCorrectGenderSprite()
    {
        var sprites = Player.GetComponents<SpriteRenderer>();

        if (playerInformation.GetGender() == Gender.Male)
        {
            spriteBoy.SetActive(true);
            spriteGirl.SetActive(false);
        }
        else
        {
            spriteBoy.SetActive(false);
            spriteGirl.SetActive(true);
        }
    }


    #endregion






}
