using UnityEngine;
using UnityEngine.Events;

public class Utils
{
    // SAVES FILE NAMES 
    public const string SaveDirectory = "Saves";
    public const string SettingsSaveFilePath = "SettingsSave";
    public const string DialoguesSaveFilePath = "DialoguesSave";
    public const string PlayerInformationSaveFilePath = "PlayerSave";

    // TIMERS CONSTANTS
    public const float LoadingScreenMinimumTime = 0.5f;
    public const float EventTimeout = 0.1f;
    public const float TimeUntilSuggestion = 20f;

    public const string UsernameDialogueKeyword = "$username$";



    public enum Language
    {
        Portuguese = 0,
        English = 1
    }

    // TODO make sure it is called in the app starting
    // TODO tmp in single game
    public static void SetScreenAlwaysOn()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}



