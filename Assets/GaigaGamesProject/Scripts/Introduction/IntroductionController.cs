using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UtilsSpeechMachine;


public class IntroductionController : MonoBehaviour
{
    private PlayerInformation playerInformation;

    // Game Objects
    public GameObject dialoguePanel;
    private GameObject dialogueManager;
    public GameObject nameRequestTextInput;
    public GameObject genderChoicePanelButton;


    private Image carImage;
    private PlayableDirector Timeline;

    private int currentScene = 0;
    private GameObject scene1;
    private GameObject scene2;
    private GameObject scene3;

    public BaseDialogueEvent dialogueEvent;


    public UnityEvent enableSpeechElements;

    // timelines / Scenes play
    public PlayableDirector FirstSceneTimeline;
    public PlayableDirector ThirdSceneTimeline;




    // Start is called before the first frame update
    void Start()
    {
        string imageName = "Car";
        string scene1Name = "Scene1";
        string scene2Name = "Scene2";
        string scene3Name = "Scene3";

        // Find all Image components in children and filter by name
        //carImage = GetComponentsInChildren<Image>()
        //.FirstOrDefault(image => image.gameObject.name == imageName);

        scene1 = GameObject.Find(scene1Name);
        scene2 = GameObject.Find(scene2Name);
        scene3 = GameObject.Find(scene3Name);

        dialogueManager = GameObject.FindGameObjectsWithTag("DialogueManager").FirstOrDefault();
        if (dialogueManager != null)
        {
            //dialogueManager.GetComponent<DialogueManager>().OnGameIsOverDialogueCompleted.AddListener(GameCompleted);
            dialogueManager.GetComponent<DialogueManager>().OnIntroductionDialogueCompleted.AddListener(IntroductionCompleted);
            dialogueManager.GetComponent<DialogueManager>().OnDialogueCompleted.AddListener(DialogueCompleted);
        }

        SetInitialGameStatus();

    }

    private void SetInitialGameStatus()
    {
        playerInformation = new PlayerInformation();

        dialoguePanel.SetActive(false);
        nameRequestTextInput.SetActive(false);
        genderChoicePanelButton.SetActive(false);

        currentScene = 1;

        if (scene2 != null)
        {
            scene2.SetActive(false);
        }

        if (scene3 != null)
        {
            scene3.SetActive(false);
        }

    }

    public void SendIntroduction()
    {
        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        // if so, it doesn't show anything
        if (dialogueEvent != null)
        {
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.Intro);
        }
    }

    public void IntroductionCompleted()
    {
        Debug.Log("SpeechMachine Controller - Introduction Completed()");
        
        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.AskName);
        
        nameRequestTextInput.SetActive(true);
    }


    public void OnNameEntered()
    {
        string textInputText = nameRequestTextInput.GetComponent<TMP_InputField>().text;
        
        // Check if string empty, if it is, repeats
        if (textInputText == string.Empty || textInputText == null)
            return;

        playerInformation.SetCharacterName(textInputText);
        playerInformation.GetCharacterName();
        
        nameRequestTextInput.SetActive(false);

        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.NameEntered);

        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.NextDialogueLine);
    }

    public void DialogueCompleted()
    {
        Debug.Log("DialogueCompleted");

        switch (currentScene)
        {
            case 1:
                // nothing to do
                break; 
            case 2:
                AskGender();
                break;
            case 3:
                ChangeToScene3();
                break;
            default:
                Debug.LogWarning("Current scene was not planned. Error in Introduction Controller");
                break;
        }
    }


    public void AskGender()
    {
        Debug.Log("SpeechMachine Controller - Ask Gender()");

        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.AskGender);

        genderChoicePanelButton.SetActive(true);
    }

    // NOTE: Male = 0, Female = 1
    public void GenderEntered(int selectedGender)
    {
        genderChoicePanelButton.SetActive(false);

        Debug.Log("IntroductionController - Gender Entered()");

        playerInformation.SetGender((Utils.Gender) selectedGender);
        playerInformation.GetGender();

        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.GenderEntered);

        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.NextDialogueLine);

        currentScene = 3;
    }

    public async void ChangeToScene2()
    {
        Debug.Log("Change to Scene 2");
        currentScene = 2;
        scene1.SetActive(false);
        scene2.SetActive(true);
        dialoguePanel.gameObject.SetActive(true);

        SendIntroduction();

        // Wait for 4 seconds
        //await Task.Delay(4000);

        //SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }

    public async void ChangeToScene3()
    {
        Debug.Log("Change to Scene 3");
        dialoguePanel.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
        scene3.GetComponent<IntroductionScene3Controller>().SetSceneAccordingToGender(playerInformation.GetGender());

        ThirdSceneTimeline.Play();
    }

    public void ChangeToGrandmasHouseScene1()
    {
        Debug.Log("Change to Grandmas House Scene 1");
        dialoguePanel.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
        scene3.GetComponent<IntroductionScene3Controller>().SetSceneAccordingToGender(playerInformation.GetGender());

        ThirdSceneTimeline.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
