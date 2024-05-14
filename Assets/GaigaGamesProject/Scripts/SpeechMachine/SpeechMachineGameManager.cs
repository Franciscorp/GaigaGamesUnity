using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechMachineGameManager : MonoBehaviour
{
    public enum SpeechElements
    {
        Brain,
        Diaphgram,
        Mouth,
        Teeth,
        Tongue,
        VoiceBox,
        Lungs
    }

    Dictionary<string, bool> SpeechElementsState;


    void Awake()
    {
        SpeechElementsState = new Dictionary<string, bool>();
        // Each Enum is inserted into dictionary has false
        foreach (var name in Enum.GetNames(typeof(SpeechElements)))
        {
            SpeechElementsState.Add(name, false);
        }
    }




}
