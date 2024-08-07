using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UtilsSpeechMachine;


public class IntroductionController : MonoBehaviour
{
    private GameObject dialogueManager;
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

        if (scene2 != null )
        {
            scene2.gameObject.SetActive(false);
        }

        //scene1 = GetComponentsInChildren<Canvas>()
        //.FirstOrDefault(canvas => canvas.gameObject.name == scene1Name);

        //scene2 = GetComponentsInChildren<Canvas>()
        //.FirstOrDefault(canvas => canvas.gameObject.name == scene2Name);

        dialogueManager = GameObject.FindGameObjectsWithTag("DialogueManager").FirstOrDefault();
        if (dialogueManager != null)
        {
            //dialogueManager.GetComponent<DialogueManager>().OnGameIsOverDialogueCompleted.AddListener(GameCompleted);
            //dialogueManager.GetComponent<DialogueManager>().OnIntroductionDialogueCompleted.AddListener(IntroductionCompleted);
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

        SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
