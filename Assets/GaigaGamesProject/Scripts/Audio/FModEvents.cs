﻿using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;


public class FModEvents : MonoBehaviour
{
    [field: Header("User Interface")]
    [field: SerializeField] public EventReference introductionMusic { get; private set; }
    [field: SerializeField] public EventReference mainGameMusic { get; private set; }
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }
    [field: SerializeField] public EventReference buttonClick { get; private set; }
    [field: SerializeField] public EventReference typingSFX { get; private set; }

    [field: Header("SpeechMachine")]
    [field: SerializeField] public EventReference SpeechMachineMusic { get; private set; }
    [field: SerializeField] public EventReference presentSuggestionSFX { get; private set; }
    [field: SerializeField] public EventReference rightAnswerSFX { get; private set; }
    [field: SerializeField] public EventReference wrongAnswerSFX { get; private set; }
    [field: SerializeField] public EventReference gameOverSFX { get; private set; }
    [field: SerializeField] public EventReference breathingSFX { get; private set; }

    [field: Header("IdentifyStutter")]

    [field: SerializeField] public EventReference IdentifyStutterMusic { get; private set; }
    [field: SerializeField] public EventReference stopTvCatSFX { get; private set; }
    [field: SerializeField] public EventReference rightAnswerCatSFX { get; private set; }
    [field: SerializeField] public EventReference wrongAnswerCatSFX { get; private set; }
    [field: SerializeField] public EventReference rightAnswerAltSFX { get; private set; }
    [field: SerializeField] public EventReference wrongAnswerAltSFX { get; private set; }
    [field: SerializeField] public EventReference newsIntroSFX { get; private set; }
    [field: SerializeField] public EventReference tvTalkingSFX { get; private set; }
    [field: SerializeField] public EventReference conclusionPartySFX { get; private set; }

    [field: Header("IdentifyStutter VoiceLines")]

    [field: SerializeField] public EventReference ISVoiceLineQuestion1 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion2 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion3 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion4 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion5 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion6 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion7 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion8 { get; private set; }
    [field: SerializeField] public EventReference ISVoiceLineQuestion9 { get; private set; }


    // contains all event instances in scene
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    public static FModEvents Instance { get; private set; }

    // Initiates singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one FMod Events in the scene");
        }
        Instance = this;
        Debug.Log("Fmod Events Created");

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }
}