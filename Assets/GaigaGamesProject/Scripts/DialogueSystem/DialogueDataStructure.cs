using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

[System.Serializable]
public class DialogueDataStructure
{
    public Language language;
    public SpeechMachineDialogueStructure speechMachineDialogues;

    public DialogueDataStructure()
    {
        this.language = Language.English;
    }
}

[System.Serializable]
public class Dialogue
{
    public string key; // Unique identifier for the dialogue
    public string englishText;
    public string portugueseText;
}
