using UnityEngine;
using static Utils;

public class ConfigurePlayerInformation : MonoBehaviour
{
    public GameObject spriteBoy;
    public GameObject spriteGirl;

    private PlayerInformation playerInformation;

    void Awake()
    {
        playerInformation = new PlayerInformation();
    }

    private void Start()
    {
        ShowCorrectGenderSprite();
    }

    private void ShowCorrectGenderSprite()
    {
        if (playerInformation.GetGender() == Gender.Male)
        {
            spriteBoy.SetActive(true);
            spriteGirl.SetActive(false);
        }
        else
        {
            spriteBoy.SetActive(false);
            spriteGirl.SetActive(true);
        }
    }
}
