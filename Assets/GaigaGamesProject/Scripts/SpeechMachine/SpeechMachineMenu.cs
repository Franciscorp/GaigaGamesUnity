using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechMachineMenu : MonoBehaviour
{
    public void BackToGameMenu()
    {
        SceneLoader.Load(SceneLoader.Scene.GamesMenu);
    }
}
