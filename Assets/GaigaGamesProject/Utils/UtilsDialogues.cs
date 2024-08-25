
using System.Collections.Generic;

public  class UtilsDialogues{

    public const string PLAYER_NAME_TAG = "[PLAYER_NAME]";

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
        Question9 = 10
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
        { IdentifyStutterDialogues.Question9, "IdentifyStutterQuestion9" }
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

}
