
using System.Collections.Generic;

public  class UtilsDialogues{

    public const string PLAYER_NAME_TAG = "[PLAYER_NAME]";
    public const string WRONG_ANSWERS_TAG = "[WRONG_ANSWERS]";
    public const string UNIDENTIFIED_DIALOGUE = "unidentifiedDialogue";

    public enum MainGameDialogues
    {
        UnidentifiedDialogue = 0,
        MainGameStory1 = 1,
        MainGameStory2 = 2,
        MainGameStory3 = 3,
        MainGameStory4 = 4,
        GrandmaStory1 = 5,
        GrandmaAltStory1 = 6,
        GrandpaAltStory1 = 7,
        SpeechMachineStory1 = 8,
        IdentifyStutterStory1 = 9,
        GrandmaAltStory2 = 10,
        SpeechMachineAlt2 = 11,

    }

    // Dictionary to map dialogue names to keys
    private static readonly Dictionary<MainGameDialogues, string> mainGameDialogueKeys = new Dictionary<MainGameDialogues, string>
    {
        { MainGameDialogues.UnidentifiedDialogue, "UnidentifiedDialogue" },
        { MainGameDialogues.MainGameStory1, "MainGameStory1" },
        { MainGameDialogues.MainGameStory2, "MainGameStory2" },
        { MainGameDialogues.MainGameStory3, "MainGameStory3" },
        { MainGameDialogues.MainGameStory4, "MainGameStory4" },
        { MainGameDialogues.GrandmaStory1, "MainGameGrandma1" },
        { MainGameDialogues.GrandmaAltStory1, "MainGameGrandmaAltSpeechMachine1" },
        { MainGameDialogues.GrandpaAltStory1, "MainGameGrandpaAltSpeechMachine1" },
        { MainGameDialogues.SpeechMachineStory1, "MainGameSpeechMachineStartGameInteract1" },
        { MainGameDialogues.IdentifyStutterStory1, "MainGameIdentifyStutterStartGameInteract1" },
        { MainGameDialogues.GrandmaAltStory2, "MainGameGrandmaAlt2" },
        { MainGameDialogues.SpeechMachineAlt2, "MainGameSpeechMachineAlt2" },
    };

    public enum VoiceLines
    {
        DefaultVoiceLine = 0,
        ISVoiceLineQuestion1 = 1,
        ISVoiceLineQuestion2 = 2,
        ISVoiceLineQuestion3 = 3,
        ISVoiceLineQuestion4 = 4,
        ISVoiceLineQuestion5 = 5,
        ISVoiceLineQuestion6 = 6,
        ISVoiceLineQuestion7 = 7,
        ISVoiceLineQuestion8 = 8,
        ISVoiceLineQuestion9 = 9
    }

    // Static function to get the key associated with a dialogue name
    public static string GetMainGameDialogueKey(MainGameDialogues dialogueName)
    {
        if (mainGameDialogueKeys.TryGetValue(dialogueName, out string key))
        {
            return key;
        }
        else
        {
            // Handle case where the dialogue name is not found
            return null; // or return a default key, throw an exception, etc.
        }
    }

    // Function to find the key based on the value
    public static MainGameDialogues GetMainGameDialogueByValueID(string value)
    {
        foreach (var pair in mainGameDialogueKeys)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }

        // Return null if no matching key is found
        return MainGameDialogues.UnidentifiedDialogue;
    }

    public enum IdentifyStutterDialogues
    {
        GameIntroduction = 0,
        GameIntroduction2 = 1,
        Question1 = 2,
        Question2 = 3,
        Question3 = 4,
        Question4 = 5,
        Question5 = 6,
        Question6 = 7,
        Question7 = 8,
        Question8 = 9,
        Question9 = 10,
        UnidentifiedDialogue = 11,
        Conclusion = 12,
        GoodConclusion = 13,
        BadConclusion = 14,
        BestConclusion = 15
    }


    // Dictionary to map dialogue names to keys
    private static readonly Dictionary<IdentifyStutterDialogues, string> dialogueKeys = new Dictionary<IdentifyStutterDialogues, string>
    {
        { IdentifyStutterDialogues.GameIntroduction, "IdentifyStutterGameIntroduction" },
        { IdentifyStutterDialogues.GameIntroduction2, "IdentifyStutterGameIntroduction2" },
        { IdentifyStutterDialogues.Question1, "IdentifyStutterQuestion1" },
        { IdentifyStutterDialogues.Question2, "IdentifyStutterQuestion2" },
        { IdentifyStutterDialogues.Question3, "IdentifyStutterQuestion3" },
        { IdentifyStutterDialogues.Question4, "IdentifyStutterQuestion4" },
        { IdentifyStutterDialogues.Question5, "IdentifyStutterQuestion5" },
        { IdentifyStutterDialogues.Question6, "IdentifyStutterQuestion6" },
        { IdentifyStutterDialogues.Question7, "IdentifyStutterQuestion7" },
        { IdentifyStutterDialogues.Question8, "IdentifyStutterQuestion8" },
        { IdentifyStutterDialogues.Question9, "IdentifyStutterQuestion9" },
        { IdentifyStutterDialogues.UnidentifiedDialogue, "IdentifyStutterUnidentifiedDialogue" },
        { IdentifyStutterDialogues.Conclusion, "IdentifyStutterConclusion" },
        { IdentifyStutterDialogues.GoodConclusion, "IdentifyStutterGoodConclusion" },
        { IdentifyStutterDialogues.BadConclusion, "IdentifyStutterBadConclusion" },
        { IdentifyStutterDialogues.BestConclusion, "IdentifyStutterBestConclusion" },
    };

    // Static function to get the key associated with a dialogue name
    public static string GetDialogueKey(IdentifyStutterDialogues dialogueName)
    {
        if (dialogueKeys.TryGetValue(dialogueName, out string key))
        {
            return key;
        }
        else
        {
            // Handle case where the dialogue name is not found
            return null; // or return a default key, throw an exception, etc.
        }
    }

    // Function to find the key based on the value
    public static IdentifyStutterDialogues GetDialogueByValueID(string value)
    {
        foreach (var pair in dialogueKeys)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }

        // Return null if no matching key is found
        return IdentifyStutterDialogues.UnidentifiedDialogue;
    }

}
