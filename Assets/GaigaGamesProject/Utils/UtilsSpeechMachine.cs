using UnityEngine;
using UnityEngine.Events;
using static UtilsSpeechMachine;

public class UtilsSpeechMachine : MonoBehaviour
{
    public enum SpeechElements
    {
        None,
        Brain,
        Diaphgram,
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
        WrongAnswer,
        RightAnswer,
        Help, 
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
