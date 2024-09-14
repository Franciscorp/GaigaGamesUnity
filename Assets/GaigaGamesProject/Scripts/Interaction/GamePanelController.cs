using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Awake()
    {
        buttonIDEvent = new GamePanelButtonID();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (startGame != null)
        {
            // Add the onClick listener and associate it with the method
            startGame.onClick.AddListener(() => SendButtonPressedID((int)Options.SpeechMachine));  
        }
        else
        {
            Debug.LogError("Button is not assigned.");
        }

        HideButtons();
    }

    public void SetOptionIDAndDisplayButtons(int optionID)
    {
        Debug.Log("SetOptionIDAndDisplayButtons = " + optionID);
        startGame.gameObject.SetActive(true);
        startGame.onClick.AddListener(() => SendButtonPressedID(optionID));
        returnGame.gameObject.SetActive(true);

        if (optionID == 1)
        {
            // TODO devia ter solução para ingles
            startGame.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Ajudar Artur";
        }else if (optionID == 2)
        {
            // TODO devia ter solução para ingles
            startGame.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Ajudar Avô";
        }
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

    public void SendButtonPressedID(int option = -1)
    {
        Debug.Log("SendButtonPressedID = " + option);
        buttonIDEvent.Invoke(option);
        HideButtons();
    }

}
