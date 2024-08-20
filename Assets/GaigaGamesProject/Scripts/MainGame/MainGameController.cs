using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static Utils;
using static UtilsSpeechMachine;

public class MainGameController : MonoBehaviour
{
    private PlayerInformation playerInformation;

    public GameObject dialoguePanel;
    private GameObject dialogueManager;
    private GameObject gameObjectCamera;
    private Camera camera;
    private Animator cameraAnimator;

    public GameObject Player;
    public GameObject spriteBoy;
    public GameObject spriteGirl;

    // Events
    public BaseDialogueEvent dialogueEvent;
    
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

        dialogueManager = GameObject.FindGameObjectsWithTag("DialogueManager").FirstOrDefault();
        if (dialogueManager != null)
        {
            //dialogueManager.GetComponent<DialogueManager>().OnGameIsOverDialogueCompleted.AddListener(GameCompleted);
            //dialogueManager.GetComponent<DialogueManager>().OnIntroductionDialogueCompleted.AddListener(IntroductionCompleted);
            //dialogueManager.GetComponent<DialogueManager>().OnDialogueCompleted.AddListener(DialogueCompleted);
        }

        SetInitialGameStatus();
    }

    private void SetInitialGameStatus()
    {
        playerInformation = new PlayerInformation();
        Debug.Log("Camera Idle");


        dialoguePanel.SetActive(false);
    }

    public async void StartGameIntroduction()
    {
        cameraAnimator.Play("CameraOut");
        dialoguePanel.SetActive(true);
        await Task.Delay(1000);
        InvokeDialogueEvent(Scene.MainGame, DialogueEventType.Intro);
    }

    // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
    // if so, it doesn't show anything
    private void InvokeDialogueEvent(Scene currentScene, DialogueEventType dialogueEventType)
    {
        if (dialogueEvent != null)
            dialogueEvent.Invoke(currentScene, dialogueEventType);
        else
            Debug.LogWarning("Invoking Dialogue Event went wrong. Check Main Game Controller");
    }

    private async void TransitionCameraToDialogue()
    {
        await Task.Delay(4000);

        //camera.View
        Debug.Log("Camera Rect = " + camera.rect);
        //cameraAnimator.SetBool("CameraOut", true);
        //cameraAnimator.SetBool("CameraIn", false);
        cameraAnimator.Play("CameraOut");
        
        await Task.Delay(4000);

        cameraAnimator.Play("CameraIn");

        //camera.rect = new Rect(0.0f, 0.26f, 1.0f, 1.0f);
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
}
