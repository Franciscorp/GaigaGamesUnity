using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UtilsDialogues;
using static UtilsSpeechMachine;


public class IntroductionController : MonoBehaviour
{
    private PlayerInformation playerInformation;

    // Game Objects
    public GameObject VideDialoguePanel;
    public VideUIManager VideDialogueManager;
    public GameObject nameRequestTextInput;
    public GameObject genderChoicePanelButton;

    private Image carImage;
    private PlayableDirector Timeline;

    private int currentScene = 0;
    private GameObject scene1;
    private GameObject scene2;
    private GameObject scene3;
    private GamesMenu gamesMenu;

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

        scene1 = GameObject.Find(scene1Name);
        scene2 = GameObject.Find(scene2Name);
        scene3 = GameObject.Find(scene3Name);

        gamesMenu = GetComponent<GamesMenu>();

        AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.introductionMusic);
        SetInitialGameStatus();
    }

    private void SetInitialGameStatus()
    {
        playerInformation = new PlayerInformation();

        VideDialoguePanel.SetActive(false);
        nameRequestTextInput.SetActive(false);
        genderChoicePanelButton.SetActive(false);

        currentScene = 1;

        if (scene2 != null)
            scene2.SetActive(false);

        if (scene3 != null)
            scene3.SetActive(false);

    }

    public void SendIntroduction()
    {
        VideDialogueManager.SetupAndStartDialogue(GetIntroductionDialogueKey(IntroductionDialogues.IntroductionAskName));
    }

    public async void IntroductionCompleted()
    {
        Debug.Log("SpeechMachine Controller - Introduction Completed()");
        
        await Task.Delay(1300);

        nameRequestTextInput.SetActive(true);
    }


    public void OnNameEntered()
    {
        string textInputText = nameRequestTextInput.GetComponent<TMP_InputField>().text;
        
        // Check if string empty, if it is, repeats
        if (textInputText == string.Empty || textInputText == null)
            return;

        playerInformation.SetCharacterName(textInputText);
        playerInformation.LoadCharacterName();
        
        nameRequestTextInput.SetActive(false);

        VideDialogueManager.SetupAndRestartDialogue(GetIntroductionDialogueKey(IntroductionDialogues.IntroductionAskGender));
    }

    public async void AskGender()
    {
        Debug.Log("SpeechMachine Controller - Ask Gender()");

        await Task.Delay(1800);

        // activates buttons to choose character
        genderChoicePanelButton.SetActive(true);
    }

    // NOTE: Male = 0, Female = 1
    public void GenderEntered(int selectedGender)
    {
        genderChoicePanelButton.SetActive(false);

        Debug.Log("IntroductionController - Gender Entered()");

        playerInformation.SetGender((Utils.Gender) selectedGender);
        playerInformation.GetGender();

        VideDialogueManager.SetupAndRestartDialogue(GetIntroductionDialogueKey(IntroductionDialogues.IntroductionStartGame));

        currentScene = 3;
    }

    public void ChangeToScene2()
    {
        Debug.Log("Change to Scene 2");
        currentScene = 2;
        scene1.SetActive(false);
        scene2.SetActive(true);
        VideDialoguePanel.SetActive(true);

        SendIntroduction();
    }

    public async void ChangeToScene3()
    {
        Debug.Log("Change to Scene 3");
        VideDialoguePanel.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
        scene3.GetComponent<IntroductionScene3Controller>().SetSceneAccordingToGender(playerInformation.GetGender());

        ThirdSceneTimeline.Play();
        await Task.Delay(3600);

        //AudioManager.Instance.StopAllAudios();
        gamesMenu.StartMainGame();
    }

    public void ChangeToGrandmasHouseScene1()
    {
        Debug.Log("Change to Grandmas House Scene 1");
        VideDialoguePanel.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
        scene3.GetComponent<IntroductionScene3Controller>().SetSceneAccordingToGender(playerInformation.GetGender());

        ThirdSceneTimeline.Play();
    }

}
