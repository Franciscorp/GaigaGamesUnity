using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using static BaseSpeechMachineDialogueGenerator;


[System.Serializable]
public class DialogueDataStructure
{
    public Language language;
    public SpeechMachineDialogueStructure speechMachineDialogues;

    public DialogueDataStructure()
    {
        this.language = Language.English;
        //speechMachineDialogues = GenerateSpeechMachineDialogue();
    }


    public void GenerateData()
    {
        speechMachineDialogues = GenerateSpeechMachineDialogue();
    }


    public bool IsThereDialoguesAvaiable()
    {
        if (speechMachineDialogues != null)
            return true;
        else
        {
            Debug.LogError("[DialogueDataStructure] - No Dialogues Avaiable in DataMisalignedException Structure");
            return false;
        }
    }
}

// NOTE: this is the correct format for json, public without gets and setters
[System.Serializable]
public class Dialogue
{
    public string key; // Unique identifier for the dialogue
    public bool wasDisplayed;
    public List<string> englishText;
    public List<string> portugueseText;

    public Dialogue(string key, List<string> englishText, List<string> portugueseText)
    {
        this.key = key;
        this.wasDisplayed = false;
        this.englishText = englishText;
        this.portugueseText = portugueseText;
    }

    public Dialogue(string key, string englishText, string portugueseText)
    {
        this.key = key;
        this.wasDisplayed = false;
        this.englishText = new List<string> {englishText};
        this.portugueseText = new List<string> { portugueseText };
    }

    public string[] GetDialoguesToArray(Language language)
    {
        if (language == Language.English)
            return englishText.ToArray();
        else
            return portugueseText.ToArray();
    }
}
