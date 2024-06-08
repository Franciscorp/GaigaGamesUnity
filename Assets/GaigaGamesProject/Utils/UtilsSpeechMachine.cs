using UnityEngine;
using UnityEngine.Events;
using static UtilsSpeechMachine;

public class UtilsSpeechMachine : MonoBehaviour
{

    public const string SpeechMachine = "SpeechMachine";
    public const string Key = "Key";
    public const string SuggestionKey = "SuggestionKey";
    public const string RightAnswerKey = "RightAnswerKey";
    public const string WrongAnswerKey = "WrongAnswerKey";
    public const int NumberOfSpeechElements = 7;

    public enum SpeechElements
    {
        None,
        Brain,
        Diaphragm,
        Mouth,
        Teeth,
        Tongue,
        VoiceBox,
        Lungs
    }

    public enum DialogueEventType
    {
        None,
        Intro,
        Help, 
        Suggestion,
        WrongAnswer,
        RightAnswer,
        Conclusion
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

[System.Serializable]
public class SpeechMachineDialogueEvent : UnityEvent<DialogueEventType , SpeechElements>
{
}
