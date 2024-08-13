

using UnityEngine;

public class PlayerInformation
{
    private string username { get; set; }

    public PlayerInformation()
    {
        username = GetCharacterName();
    }

    public void SetCharacterName(string name)
    {
        PlayerPrefs.SetString("UserName", name);
        PlayerPrefs.Save();
        Debug.Log("Character Name saved");
    }

    public string GetCharacterName()
    {
        string characterName = PlayerPrefs.GetString("UserName");
        Debug.Log(characterName);
        return characterName;
    }

}