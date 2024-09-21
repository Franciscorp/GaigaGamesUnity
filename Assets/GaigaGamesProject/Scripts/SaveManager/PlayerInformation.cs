

using UnityEngine;
using static Utils;

public class PlayerInformation
{
    private string username { get; set; }
    private Gender gender { get; set; }
    public GameStages currentGameStage { get; set; }
    public IdentifyStutterInformation identifyStuterGameInfo { get; set; }
    

    public PlayerInformation()
    {
        LoadData();
    }

    // Loads information async, stops lag
    // TODO this is bad, because it was loading multiple times without necessity
    public async void LoadData()
    {
        username = LoadCharacterName();
        gender = GetGender();
        // Loads information if exists
        identifyStuterGameInfo = new IdentifyStutterInformation();
        currentGameStage = LoadCurrentGameStage();
    }

    public void CreateBasePlayerInfo()
    {
        SetCurrentGameStage(GameStages.SpeechMachineDone);
        SetCharacterName("Jogador");
        SetGender(Gender.Female);
    }

    public void ResetMiniGamesInformation()
    {
        identifyStuterGameInfo.ResetIdentifyStutter();
    }

    public void SetCharacterName(string name)
    {
        name = CapitalizeEachWord(name);
        username = name;
        PlayerPrefs.SetString(Utils.UsernamePlayerPrefsKeyword, name);
        PlayerPrefs.Save();
        Debug.Log("Character Name saved");
    }

    public string GetUsername()
    {
        return username;
    }

    public string LoadCharacterName()
    {
        string characterName = PlayerPrefs.GetString(Utils.UsernamePlayerPrefsKeyword, "Jogador");
        username = characterName;
        //Debug.Log(characterName);
        return characterName;
    }

    public GameStages LoadCurrentGameStage()
    {
        return (GameStages)PlayerPrefs.GetInt(Utils.GameStagePrefsKeyword, 0);
    }

    public GameStages GetCurrentGameStage()
    {
        return currentGameStage;
    }

    public void SetCurrentGameStage(GameStages gameStage)
    {
        Debug.Log("SetCurrentGameStage = " + gameStage);
        PlayerPrefs.SetInt(Utils.GameStagePrefsKeyword, (int)gameStage);
        PlayerPrefs.Save();
        currentGameStage = gameStage;
        Debug.Log("Saved CurrentGameStage = " + gameStage);
    }

    public void SetGender(Gender gender)
    {
        PlayerPrefs.SetInt(Utils.GenderPlayerPrefsKeyword, (int)gender);
        PlayerPrefs.Save();
        Debug.Log("Gender saved");
    }

    public Gender GetGender()
    {
        return (Gender)PlayerPrefs.GetInt(Utils.GenderPlayerPrefsKeyword);
    }

}