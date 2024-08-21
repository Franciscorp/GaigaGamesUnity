using System.Collections.Generic;
using System.Globalization;
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

    // PLAYER PREFS
    public const string UsernameDialogueKeyword = "$username$";
    public const string UsernamePlayerPrefsKeyword = "Username";
    public const string GenderPlayerPrefsKeyword = "Gender";

    public enum Npc
    {
        Narrator = 0,
        Boy = 1,
        Girl = 2,
        SpeechMachine = 3,
        Tobias = 4,
        Grandma = 5,
        Grandpa = 6, 
        Error = 7,
        Player = 8
    }

    public enum Language
    {
        Portuguese = 0,
        English = 1
    }

    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    // TODO make sure it is called in the app starting
    // TODO tmp in single game
    public static void SetScreenAlwaysOn()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Function to capitalize each word in the sentence.
    public static string CapitalizeEachWord(string sentence)
    {
        // Use TextInfo to capitalize each word in the sentence.
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(sentence.ToLower());
    }

    public static string GetPortugueseTranslatedNpcList(Npc currentNpc)
    {
        List<string> npcList = new List<string>
        {
            "Narrador",      // Narrator
            "Menino",        // Boy
            "Menina",        // Girl
            "Boneco", // SpeechMachine
            "Tobias",        // Tobias
            "Avó",           // Grandma
            "Avô",            // Grandpa
            "Erro",           // Erro
            "Jogador"         // Player
        };

        return npcList[(int)currentNpc];
    }
}



