
using System.Collections.Generic;
using UnityEngine;
using static TVContent;

public class CatSpriteController : MonoBehaviour
{
    private SpriteRenderer currentSprite;
    public List<Sprite> contents;

    public enum CatSpriteID
    {
        NormalCat = 0,
        ConfusedCat = 1,
        HappyCat = 2,
        CatMeows = 3
    }


    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();

        if (contents != null)
            currentSprite.sprite = contents[0];
    }

    public void UpdateContent(CatSpriteID contentID)
    {
        if (contents == null || contents.Count <= 0)
            return;

        if ((contents.Count - 1) < (int)contentID)
        {
            Debug.LogWarning("Cat Sprite doesn't have choosen ID");
            return;
        }

        currentSprite.sprite = contents[(int)contentID];
    }
}
