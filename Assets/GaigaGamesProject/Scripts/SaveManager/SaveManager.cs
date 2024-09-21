using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }


    // load and then start saving every 30 seconds
    void Awake()
    {
        // This is the singleton pattern, if instance exists, destroy the current one
        if (Instance != null && Instance != this)
            Destroy(this);

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // On first load loads data
            Load();
        }

    }


    // add event handler to receive notification when the app is trying to quit
    // callback when the runtime is starting up and loading the first scene
    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        // Unity raises this event when the player application wants to quit
        Application.wantsToQuit += WantsToQuit;
    }

    // On player trying to quit, start last save couroutine 
    static bool WantsToQuit()
    {
        // only run if there's a save manager 
        Application.wantsToQuit -= WantsToQuit;

        if (Instance != null)
        {
            Instance.StopCoroutine(nameof(Save));

            if (Instance != null)
                Instance.Save(true); // save all state that are set to save on quit
        }

        return true;
    }

    // task class: single operation that does not return a value and that executes asynchronously
    // on a thread pool thread rather than synchronously on the main application thread
    public async Task WriteToFileAsync(string path, string json)
    {
        using StreamWriter outputFile = new(path);
        await outputFile.WriteAsync(json);
    }


    public static void StartSave()
    {
        if (Instance != null)
            Instance.Save();
        else
            Debug.LogError("Could not save game, SaveManager not set");
    }


    // not a thread-safe procedure
    // the IEnumerator provides async save, i.e. game lag-free
    // find all objects that extend from the BaseState class and call their save method if need be 
    private void Save(bool quitting = false)
    {
        foreach (BaseState state in GameObject.FindObjectsOfType<BaseState>())
        {
            // if quitting, state must also be saveable on quit to be saved
            bool canSave = quitting ? (state.saveOnQuit && state.ShouldSave()) : state.ShouldSave();
            if (canSave)
            {
                string json = state.SaveState();
                // _ discards return value
                _ = WriteToFileAsync(state.GetSaveDir() + "/" + state.GetUID() + ".save", json);
                //Debug.Log("[DataManager.Save] json = " + json);
                Debug.Log("[DataManager.Save] json Saved");
            }
        }
    }

    // every savable object with Base state is detected and if they need to be loaded, pass them the new information 
    private void Load()
    {
        foreach (BaseState state in GameObject.FindObjectsOfType<BaseState>())
        {
            if (state.ShouldLoad())
            {
                string expectedFileLocation = state.GetSaveDir() + "/" + state.GetUID() + ".save";

                if (File.Exists(expectedFileLocation))
                {
                    string json = File.ReadAllText(expectedFileLocation);
                    state.LoadState(json);
                    //Debug.Log("[DataManager.Load] json = " + json);
                    Debug.Log("Dialogue Json Loaded");
                }
            }
        }

    }

}


