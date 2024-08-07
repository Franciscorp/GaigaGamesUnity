using System;
using System.Collections.Generic;
using UnityEngine;
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
        int randomDialogue = UnityEngine.Random.Range(0, possibleDialogues.Count);
        return possibleDialogues[randomDialogue].GetDialoguesToArray(dialogueDataStructure.language);
    }
}