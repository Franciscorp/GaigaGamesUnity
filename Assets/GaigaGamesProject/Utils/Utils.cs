using UnityEngine.Events;
using static Utils;

public class Utils
{
    // SAVES FILE NAMES 
    //public const string SaveDirectory = "Saves";
    public const string SettingsSaveFilePath = "SettingsSave";
    //public const string GameDataSaveFilePath = SaveDirectory + "/" + "GameDataSaveFilePath";

    // TIMERS CONSTANTS
    public const float LoadingScreenMinimumTime = 0.5f;


    // SPEECH MACHINE UTILS

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

}

[System.Serializable]
public class SpeechMachineGameEvent : UnityEvent<SpeechElements, SpeechElements>
{
}

[System.Serializable]
public class SpeechMachineElementInPositionEvent : UnityEvent<SpeechElements>
{
}