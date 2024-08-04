using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenGamesMenu()
    {
        SceneLoader.Load(SceneLoader.Scene.GamesMenu);
    }

    public void PlayStoryMode()
    {
        SceneLoader.Load(SceneLoader.Scene.Introduction);
        //SceneLoader.Load(SceneLoader.Scene.MainStoryGame);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
