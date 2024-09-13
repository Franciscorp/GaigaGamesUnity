using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utils;

public class MainMenu : MonoBehaviour
{
    PlayerInformation playerInformation;

    private void Awake()
    {
        playerInformation = new PlayerInformation();
        playerInformation.SetCurrentGameStage(GameStages.Beginning);
        AudioManager.Instance.PlayUniqueOneShot(FModEvents.Instance.mainMenuMusic);
        //playerInformation.SetCurrentGameStage(GameStages.IntroductionCompleted);
    }

    public void OpenGamesMenu()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.buttonClick);
        SceneLoader.Load(SceneLoader.Scene.GamesMenu);
    }

    public void PlayStoryMode()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.buttonClick);
        playerInformation.SetCurrentGameStage(GameStages.Beginning);
        SceneLoader.Load(SceneLoader.Scene.Introduction);
        //SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayStoryModeSpeechMachine()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.buttonClick);
        playerInformation.SetCurrentGameStage(GameStages.IntroductionCompleted);
        SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }

    public void PlayStoryModeIdentifyStutter()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.buttonClick);
        playerInformation.SetCurrentGameStage(GameStages.SpeechMachineDone);
        SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.buttonClick);
        Debug.Log("Quit");
        Application.Quit();
    }
}
