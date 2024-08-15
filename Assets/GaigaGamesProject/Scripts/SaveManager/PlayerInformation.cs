

using UnityEngine;
using static Utils;

public class PlayerInformation
{
    private string username { get; set; }
    private Gender gender { get; set; }

    public PlayerInformation()
    {
        username = GetCharacterName();
        gender = GetGender();
    }

    public void SetCharacterName(string name)
    {
        name = CapitalizeEachWord(name);
        PlayerPrefs.SetString(Utils.UsernamePlayerPrefsKeyword, name);
        PlayerPrefs.Save();
        Debug.Log("Character Name saved");
    }

    public string GetCharacterName()
    {
        string characterName = PlayerPrefs.GetString(Utils.UsernamePlayerPrefsKeyword);
        Debug.Log(characterName);
        return characterName;
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