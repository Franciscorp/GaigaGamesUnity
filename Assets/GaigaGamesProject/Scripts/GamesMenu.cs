using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesMenu : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }

    public void StartSpeechMachineGame()
    {
        SceneLoader.Load(SceneLoader.Scene.SpeechMachine);
    }

    public void StartMainGame()
    {
        SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }
}
