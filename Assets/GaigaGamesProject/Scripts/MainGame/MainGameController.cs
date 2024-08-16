using UnityEngine;
using static Utils;

public class MainGameController : MonoBehaviour
{
    private PlayerInformation playerInformation;

    public GameObject Player;
    public GameObject spriteBoy;
    public GameObject spriteGirl;

    private void Awake()
    {
        playerInformation = new PlayerInformation();
    }

    private void Start()
    {
        ShowCorrectGenderSprite();
    }

    private void ShowCorrectGenderSprite()
    {
        var sprites = Player.GetComponents<SpriteRenderer>();

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
