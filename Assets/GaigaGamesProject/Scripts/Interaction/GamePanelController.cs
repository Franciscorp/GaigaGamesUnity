using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    // Define a UnityEvent with an integer parameter
    [System.Serializable]
    public class GamePanelButtonID : UnityEvent<int> { }

    public Button startGame;
    public Button returnGame;
    public GamePanelButtonID buttonIDEvent;

    public enum Options
    {
        Return = 0,
        SpeechMachine = 1,
        IdentifyStutter = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonIDEvent = new GamePanelButtonID();

        if (startGame != null)
        {
            // Add the onClick listener and associate it with the method
            startGame.onClick.AddListener(() => GameButtonPressed((int)Options.SpeechMachine));  
        }
        else
        {
            Debug.LogError("Button is not assigned.");
        }

        HideButtons();
    }

    public void SetOptionIDAndDisplayButtons(int optionID)
    {
        startGame.gameObject.SetActive(true);
        startGame.onClick.AddListener(() => GameButtonPressed(optionID));
        returnGame.gameObject.SetActive(true);
    }

    public void SetOptionIDAndDisplayButtons(Options optionID)
    {
        startGame.gameObject.SetActive(true);
        startGame.onClick.AddListener(() => GameButtonPressed((int)optionID));
        returnGame.gameObject.SetActive(true);
    }

    public void DisplayButtons()
    {
        startGame.gameObject.SetActive(true);
        returnGame.gameObject.SetActive(true);
    }

    public void HideButtons()
    {
        startGame.gameObject.SetActive(false);
        startGame.onClick.RemoveAllListeners();
        returnGame.gameObject.SetActive(false);
    }

    public void GameButtonPressed(int option = -1)
    {
        buttonIDEvent.Invoke(option);
        HideButtons();
    }

}
