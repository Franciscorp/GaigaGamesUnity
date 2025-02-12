using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

// Audio Manager
// https://www.youtube.com/watch?v=rcBHIOjZDpk

public class AudioManager : MonoBehaviour
{
    private const float MinAudioValue = 0f;
    private const float MaxAudioValue = 1f;

    // contains all event instances in scene
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance uniqueEventInstance;
    private EventInstance uniqueVoiceLineEventInstance;


    public static AudioManager Instance { get; private set; }

    // Initiates singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        Instance = this;
        Debug.Log("Audio Manager Created");

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    // LOGIC
    public void PlayOneShot(EventReference sound)
    {
        Vector3 worldPos = Vector3.zero;
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlayVoiceLine(EventReference sound)
    {
        uniqueVoiceLineEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        uniqueVoiceLineEventInstance = CreateInstance(sound);
        uniqueVoiceLineEventInstance.start();
    }

    public void PlayUniqueOneShot(EventReference sound)
    {
        //Vector3 worldPos = Vector3.zero;
        //RuntimeManager.PlayOneShot(sound, worldPos);
        uniqueEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        uniqueEventInstance = CreateInstance(sound);
        uniqueEventInstance.start();
    }

    // responsible for Emitters that are local spatial audios
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.AddComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void StopAllAudios()
    {
        Debug.Log("StopAllAudios");
        CleanUp();
    }

    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            Debug.Log("Clean eventInstance = " + eventInstance.ToString());
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        // stop all the event emitter, becasuse if we don't they may hang around on other scenes
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("On Destroy");
        CleanUp();
    }

}
