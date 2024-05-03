using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesMenu : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }

}
