﻿
using System.Collections.Generic;

public  class UtilsDialogues{

    public const string PLAYER_NAME_TAG = "[PLAYER_NAME]";

    public enum IdentifyStutterDialogues
    {
        GameIntroduction = 0,
        GameIntroduction2 = 1,
        Question1 = 2
    }

    // Dictionary to map dialogue names to keys
    private static readonly Dictionary<IdentifyStutterDialogues, string> dialogueKeys = new Dictionary<IdentifyStutterDialogues, string>
    {
        { IdentifyStutterDialogues.GameIntroduction, "IdentifyStutterGameIntroduction" },
        { IdentifyStutterDialogues.GameIntroduction2, "IdentifyStutterGameIntroduction2" },
        { IdentifyStutterDialogues.Question1, "IdentifyStutterQuestion1" }
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
