using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Utils;

public class SpeechMachineGameManager : MonoBehaviour
{
    Dictionary<string, bool> SpeechElementsState;

    public SpeechMachineElementInPositionEvent elementIsInSlot;


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
        }

    }


}
