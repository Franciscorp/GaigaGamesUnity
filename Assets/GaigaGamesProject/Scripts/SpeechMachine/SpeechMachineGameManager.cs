using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilsSpeechMachine;

public class SpeechMachineGameManager : MonoBehaviour
{
    Dictionary<string, bool> SpeechElementsState;

    public SpeechMachineElementInPositionEvent elementIsInSlot;

    public SpeechMachineDialogueEvent dialogueEvent;


    void Awake()
    {
        SpeechElementsState = new Dictionary<string, bool>();
        // Each Enum is inserted into dictionary has false
        foreach (var name in Enum.GetNames(typeof(SpeechElements)))
        {
            SpeechElementsState.Add(name, false);
        }

        // Note: This notifies all the elements listening, so its performance it is not the best, but given such small cases, should be fine
        var speechElementsList = GameObject.FindGameObjectsWithTag("SpeechElement");
        foreach (var speechElement in speechElementsList)
        {
            elementIsInSlot.AddListener(speechElement.GetComponent<DragDrop>().ElementInSlot);
        }
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
            dialogueEvent.Invoke(DialogueEventType.WrongAnswer, SpeechElements.None);
        }

        //Debug.Log("Presenting list of elements done: ");

        foreach (var element in SpeechElementsState)
        {
            //Debug.Log(element.Key + " = " + element.Value);
        }

    }


}
