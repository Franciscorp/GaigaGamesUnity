using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UtilsSpeechMachine;


public class IntroductionController : MonoBehaviour
{
    private PlayerInformation playerInformation;

    // Game Objects
    private GameObject dialogueManager;
    public GameObject nameRequestTextInput;
    public GameObject genderChoicePanelButton;


    private Image carImage;
    private PlayableDirector Timeline;

    private Canvas scene1;
    private Canvas scene2;

    public BaseDialogueEvent dialogueEvent;


    public UnityEvent enableSpeechElements;


    // Start is called before the first frame update
    void Start()
    {
        string imageName = "Car";
        string scene1Name = "Scene1";
        string scene2Name = "Scene2";

        // Find all Image components in children and filter by name
        //carImage = GetComponentsInChildren<Image>()
            //.FirstOrDefault(image => image.gameObject.name == imageName);

        scene1 = GameObject.Find(scene1Name).GetComponent<Canvas>();
        scene2 = GameObject.Find(scene2Name).GetComponent<Canvas>();

        dialogueManager = GameObject.FindGameObjectsWithTag("DialogueManager").FirstOrDefault();
        if (dialogueManager != null)
        {
            //dialogueManager.GetComponent<DialogueManager>().OnGameIsOverDialogueCompleted.AddListener(GameCompleted);
            dialogueManager.GetComponent<DialogueManager>().OnIntroductionDialogueCompleted.AddListener(IntroductionCompleted);
        }

        SetInitialGameStatus();

    }

    private void SetInitialGameStatus()
    {
        playerInformation = new PlayerInformation();

        nameRequestTextInput.SetActive(false);
        genderChoicePanelButton.SetActive(false);

        if (scene2 != null)
        {
            scene2.gameObject.SetActive(false);
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
        playerInformation.SetCharacterName(textInputText);
        playerInformation.GetCharacterName();
        AskGender();
    }


    public void AskGender()
    {
        Debug.Log("SpeechMachine Controller - Ask Gender()");

        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        if (dialogueEvent != null)
            dialogueEvent.Invoke(Scene.Introduction, DialogueEventType.AskGender);

        genderChoicePanelButton.SetActive(true);
    }


    public async void ChangeToScene2()
    {
        Debug.Log("Change to Scene 2");
        //scene1.enabled = false;
        scene1.gameObject.SetActive(false);
        //scene1.enabled = true;
        scene2.gameObject.SetActive(true);

        SendIntroduction();

        // Wait for 4 seconds
        await Task.Delay(4000);

        //SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
