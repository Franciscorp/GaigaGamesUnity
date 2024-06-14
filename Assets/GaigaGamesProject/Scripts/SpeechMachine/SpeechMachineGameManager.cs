using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using static UtilsSpeechMachine;

public class SpeechMachineGameManager : MonoBehaviour
{
    private GameObject dialogueManager;
    private Coroutine suggestionCoroutine;
    private Coroutine introEventCoroutine;
    private Dictionary<string, bool> SpeechElementsState;

    // Events
    public SpeechMachineElementInPositionEvent elementIsInSlot;
    public SpeechMachineDialogueEvent dialogueEvent;


    private void Awake()
    {
        SpeechElementsState = new Dictionary<string, bool>();
        // Each Enum is inserted into dictionary has false
        foreach (var name in Enum.GetNames(typeof(SpeechElements)))
        {
            SpeechElementsState.Add(name, false);
        }

        // Element = "None" is already set to done
        SpeechElementsState["None"] = true;

        // Note: This notifies all the elements listening, so its performance it is not the best, but given such small cases, should be fine
        var speechElementsList = GameObject.FindGameObjectsWithTag("SpeechElement");
        foreach (var speechElement in speechElementsList)
        {
            elementIsInSlot.AddListener(speechElement.GetComponent<DragDrop>().ElementInSlot);
        }

        dialogueManager = GameObject.FindGameObjectsWithTag("DialogueManager").FirstOrDefault();
        if (dialogueManager != null)
            dialogueManager.GetComponent<DialogueManager>().OnGameIsOverDialogueCompleted.AddListener(GameCompleted);
    }

    private void Start()
    {
        introEventCoroutine = StartCoroutine(TryUntilInvokeIntro());
        StartCountdownForSuggestion();
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.SpeechMachineMusic);
    }


    public void PresentSuggestionToPlayer()
    {
        var speechElement = GetPlayableSpeechElement();
        Debug.Log("[PresentSuggestionToPlayer] = " + speechElement);
        dialogueEvent.Invoke(DialogueEventType.Suggestion, speechElement);
    }

    // TODO stops giving suggestions for ever, will need reactivating in the future
    public void StopSuggestionsToThePlayer()
    {
        StopCoroutine(suggestionCoroutine);
    }

    public void GameCompleted()
    {
        Debug.Log("SpeechMachine Controller - GameCompleted()");
        AudioManager.Instance.PlayOneShot(FModEvents.Instance.gameOverSFX);
        StartCoroutine(WaitForJingleToEnd());
        //Application.Quit();
    }

    // TODO TEMP
    IEnumerator WaitForJingleToEnd()
    {
        yield return new WaitForSeconds(4.8f);
        Debug.Log("WaitForJingleToEnd");
        //SceneLoader.Load(SceneLoader.Scene.GamesMenu);
        Application.Quit();
    }

    // working, checks if event avaiable until being sent
    IEnumerator TryUntilInvokeIntro()
    {
        // Waits for a few seconds until a suggestion appears
        yield return new WaitForSeconds(Utils.EventTimeout);

        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        // if so, it doesn't show anything
        if (dialogueEvent != null)
        {
            dialogueEvent.Invoke(DialogueEventType.Intro, SpeechElements.None);
            StopCoroutine(introEventCoroutine);
        }

        TryUntilInvokeIntro();
    }


    private SpeechElements GetPlayableSpeechElement()
    {
        // Filter out the speech elements that have not been played
        var playableSpeechElements = SpeechElementsState.Where(k => !k.Value).ToList();

        // Ensure there are playable speech elements available
        if (playableSpeechElements.Count == 0)
        {
            return SpeechElements.None; // or handle this case as needed
        }

        // Note: Random suggestion will not be used
        // Select a random speech element from the filtered list
        //int randomIndex = UnityEngine.Random.Range(0, playableSpeechElements.Count);
        //var speechElementSuggested = playableSpeechElements[randomIndex];

        // next suggestion by order
        var speechElementSuggested = playableSpeechElements.FirstOrDefault();

        if (Enum.TryParse(speechElementSuggested.Key, out SpeechElements speechElementEnum))
        {
            // Convert the enum to its underlying integer value
            int enumInt = (int)speechElementEnum;

            // Output the integer value
            Console.WriteLine($"The integer value of {speechElementSuggested.Key} is {enumInt}.");
        }
        else
        {
            // Handle the case where the string is not a valid enum name
            Console.WriteLine("Invalid enum name: " + speechElementSuggested.Key);
        }

        return speechElementEnum;
    }

    // Checks if courotine already exists, if not starts
    public void StartCountdownForSuggestion()
    {
        if (suggestionCoroutine != null)
        {
            StopCoroutine(suggestionCoroutine);
        }
        suggestionCoroutine = StartCoroutine(CountdownForSuggestion());
    }


    IEnumerator CountdownForSuggestion()
    {
        // Waits for a few seconds until a suggestion appears
        yield return new WaitForSeconds(Utils.TimeUntilSuggestion);
        
        // sends dialogue event and dialogue manager checks if other dialogue is present on the dialogue manager, 
        // if so, it doesn't show anything
        PresentSuggestionToPlayer();

        //Debug.Log("Sent suggestion");
        StartCountdownForSuggestion();
    }

    public void ElementInSlotDetected(SpeechElements speechElementSlotID, SpeechElements speechElementID)
    {
        if (speechElementSlotID == speechElementID)
        {
            Debug.Log("Speech Element was detected and it is equal!");
            elementIsInSlot.Invoke(speechElementID);
            SpeechElementsState[Enum.GetName(typeof(SpeechElements), speechElementID)] = true;
            // on catching element, sends a dialogue event
            // Right Element, righ answer
            dialogueEvent.Invoke(DialogueEventType.RightAnswer, speechElementID);
        }
        else
        {
            dialogueEvent.Invoke(DialogueEventType.WrongAnswer, speechElementID);
        }

        //Debug.Log("Presenting list of elements done: ");

        int numberOfElementsDone = SpeechElementsState.Where(x => x.Value == true).Count();
        
        if (numberOfElementsDone == UtilsSpeechMachine.NumberOfSpeechElements)
        {
            dialogueEvent.Invoke(DialogueEventType.Conclusion, SpeechElements.None);
            Debug.Log("All speech elements done");
        }

    }


}
