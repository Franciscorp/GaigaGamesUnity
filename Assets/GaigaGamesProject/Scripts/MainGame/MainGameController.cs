using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static Utils;

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


        //dialoguePanel.SetActive(false);
        TransitionCameraToDialogue();
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
