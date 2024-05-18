using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class DialogueStructure
{
    private Language language;
    private SpeechMachineDialogueStructure speechMachineDialogues;

}

[System.Serializable]
public class Dialogue
{
    public string key; // Unique identifier for the dialogue
    public string englishText;
    public string portugueseText;
}
