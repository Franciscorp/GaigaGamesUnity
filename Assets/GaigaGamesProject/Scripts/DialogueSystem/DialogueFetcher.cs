using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.SmartFormat.Utilities;
using static UtilsSpeechMachine;


public class DialogueFetcher : MonoBehaviour {

    public static string[] GetRequestedRandomDialogueFromList(DialogueDataStructure dialogueDataStructure, 
        DialogueEventType currentDialogueTypeEvent, Scene currentScene)
    {
        switch (currentDialogueTypeEvent)
        {
            case DialogueEventType.None:
                return new string[] { "Error, None type of error found" };

            case DialogueEventType.Intro:
                return GetRandomDialogueFromList(dialogueDataStructure, GetIntroFromCurrentScene(dialogueDataStructure, currentScene));

            case DialogueEventType.AskName:
                return GetRandomDialogueFromList(dialogueDataStructure, dialogueDataStructure.introductionDialogues.askName);
            
            case DialogueEventType.NameEntered:
                return GetNameEnteredForIntroduction(dialogueDataStructure);

            case DialogueEventType.AskGender:
                return GetRandomDialogueFromList(dialogueDataStructure, dialogueDataStructure.introductionDialogues.askGender);

            case DialogueEventType.GenderEntered:
                return GetAdequateGenderAnswerForIntroduction(dialogueDataStructure);

            case DialogueEventType.Conclusion:
                return GetRandomDialogueFromList(dialogueDataStructure, GetConclusionFromCurrentScene(dialogueDataStructure, currentScene));

            default:
                return new string[] { "Default value with no speech element, This is an error" };
        }

    }

    private static List<Dialogue> GetIntroFromCurrentScene(DialogueDataStructure dialogueDataStructure, Scene currentScene)
    {
        switch (currentScene)
        {
            case Scene.Introduction:
                return dialogueDataStructure.introductionDialogues.introduction;

            case Scene.SpeechMachine:
                return dialogueDataStructure.speechMachineDialogues.introduction;
           
            default:
                return new List<Dialogue>() { new Dialogue("Error", "There was an error in Dialogue fetcher", "Ocorreu um erro no Dialogue Fetcher") };

        }
    }

    private static string[] GetNameEnteredForIntroduction(DialogueDataStructure dialogueDataStructure)
    {
        string[] nameEnteredString = GetRandomDialogueFromList(dialogueDataStructure, dialogueDataStructure.introductionDialogues.nameEntered);

        nameEnteredString = ChangeDialogueKeywords(nameEnteredString);

        return nameEnteredString;
    }

    private static string[] GetAdequateGenderAnswerForIntroduction(DialogueDataStructure dialogueDataStructure)
    {
        PlayerInformation playerInformation = new PlayerInformation();
        Utils.Gender gender = playerInformation.GetGender();

        if (gender == Utils.Gender.Male)
            return GetRandomDialogueFromList(dialogueDataStructure, dialogueDataStructure.introductionDialogues.maleGenderEntered);
        //if (gender == Utils.Gender.Female)
        else 
            return GetRandomDialogueFromList(dialogueDataStructure, dialogueDataStructure.introductionDialogues.femaleGenderEntered);
    }

    private static List<Dialogue> GetConclusionFromCurrentScene(DialogueDataStructure dialogueDataStructure, Scene currentScene)
    {
        switch (currentScene)
        {
            //case Scene.Introduction:
                //return dialogueDataStructure.introductionDialogues.conclusion;

            case Scene.SpeechMachine:
                return dialogueDataStructure.speechMachineDialogues.conclusion;

            default:
                return new List<Dialogue>() { new Dialogue("Error", "There was an error in Dialogue fetcher", "Ocorreu um erro no Dialogue Fetcher") };
        }
    }


    private static string[] GetRandomDialogueFromList(DialogueDataStructure dialogueDataStructure, List<Dialogue> possibleDialogues)
    {
        if (possibleDialogues == null || possibleDialogues.Count == 0)
        {
            Debug.LogError("Dialogue Fetcher - List of possible Dialogues is Empty, check DialogueStructure");
            return null;
        }

        int randomDialogue = UnityEngine.Random.Range(0, possibleDialogues.Count);
        return possibleDialogues[randomDialogue].GetDialoguesToArray(dialogueDataStructure.language);
    }

    private static string[] ChangeDialogueKeywords(string[] originalDialogue)
    {
        // TODO not efficient. but fuck it
        PlayerInformation playerInformation = new PlayerInformation();
        string username = playerInformation.GetCharacterName();

        // Replace the keyword in each string
        for (int i = 0; i < originalDialogue.Count(); i++)
        {
            originalDialogue[i] = originalDialogue[i].Replace(Utils.UsernameDialogueKeyword, username);
        }

        return originalDialogue;
    }

}