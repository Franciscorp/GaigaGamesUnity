using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;


public class FModEvents : MonoBehaviour
{
    [field: Header("User Interface")]
    [field: SerializeField] public EventReference buttonClick { get; private set; }
    [field: SerializeField] public EventReference typingSFX { get; private set; }

    [field: Header("SpeechMachine")]
    [field: SerializeField] public EventReference SpeechMachineMusic { get; private set; }
    [field: SerializeField] public EventReference presentSuggestionSFX { get; private set; }
    [field: SerializeField] public EventReference rightAnswerSFX { get; private set; }
    [field: SerializeField] public EventReference wrongAnswerSFX { get; private set; }
    [field: SerializeField] public EventReference gameOverSFX { get; private set; }
    [field: SerializeField] public EventReference breathingSFX { get; private set; }

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
        Debug.Log("Audio Manager Created");

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }
}