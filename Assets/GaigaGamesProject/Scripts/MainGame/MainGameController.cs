using System;
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

    // Game Objects 
    public GameObject VideDialoguePanel;
    private VideUIManager videDialogueManager;
    public GameObject MobileControls;
    private GameObject gameObjectCamera;
    private Camera camera;
    private Animator cameraAnimator;

    // Player and Tobias
    public GameObject Player;
    public GameObject spriteBoy;
    public GameObject spriteGirl;
    public CatSpriteController TobiasSprite;


    // Events nad Playlable directors
    public PlayableDirector TobiasAppearsTimeline;
    public PlayableDirector TobiasDissapearsTimeline;



    private void Awake()
    {
        playerInformation = new PlayerInformation();
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

        SetInitialGameStatus();

    }

    private void SetInitialGameStatus()
    {
        playerInformation = new PlayerInformation();
        Debug.Log("Camera Idle");
        currentDialogue = 0;

        VideDialoguePanel.SetActive(false);
        MobileControls.SetActive(false);
    }

    #region GameControl

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

    public void EndIntroduction()
    {
        DisableDialogue();
    }

    #endregion

    #region GameVisualsControl

    public async void ActivateDialogue()
    {
        cameraAnimator.Play("CameraOut");
        await Task.Delay(300);
        VideDialoguePanel.SetActive(true);
        MobileControls.SetActive(false);
        await Task.Delay(700);
    }

    public async void DisableDialogue()
    {
        cameraAnimator.Play("CameraIn");
        await Task.Delay(300);
        VideDialoguePanel.SetActive(false);
        MobileControls.SetActive(true);
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
