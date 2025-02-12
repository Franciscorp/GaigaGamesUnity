using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using static UtilsSpeechMachine;
using static BaseSpeechMachineDialogueGenerator;
using static BaseIntroductionDialogueGenerator;
using static BaseMainGameDialogueGenerator;



[System.Serializable]
public class DialogueDataStructure
{
    public Language language;
    public IntroductionDialogueStructure introductionDialogues;
    public MainGameDialogueStructure mainGameDialogues;
    public SpeechMachineDialogueStructure speechMachineDialogues;


    public DialogueDataStructure()
    {
        this.language = Language.Portuguese;
        GenerateData();
    }


    public void GenerateData()
    {
        if (this.speechMachineDialogues == null)
            speechMachineDialogues = GenerateSpeechMachineDialogue();

        if (this.introductionDialogues == null)
            introductionDialogues = GenerateIntroductionDialogueStructure();

        if (this.mainGameDialogues == null)
            mainGameDialogues = GenerateMainGameDialogueStructure();
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
    public string npcNamePortuguese;
    public string npcNameEnglish;
    public Npc npc;
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

    public Dialogue(string key, Npc npc, List<string> englishText, List<string> portugueseText)
    {
        this.key = key;
        this.wasDisplayed = false;
        this.npcNamePortuguese = GetPortugueseTranslatedNpcList(npc);
        this.npc = npc;
        this.englishText = englishText;
        this.portugueseText = portugueseText;
    }

    public Dialogue(string key, Npc npc, string englishText, string portugueseText)
    {
        this.key = key;
        this.wasDisplayed = false;
        this.npcNamePortuguese = GetPortugueseTranslatedNpcList(npc);
        this.npc = npc;
        this.englishText = new List<string> { englishText };
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
