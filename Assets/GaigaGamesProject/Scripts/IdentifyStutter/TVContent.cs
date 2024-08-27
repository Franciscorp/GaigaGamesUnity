using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVContent : MonoBehaviour
{
    private SpriteRenderer currentSprite;
    public List<Sprite> contents;

    public enum TVContentID
    {
        ScreenOff = 0,
        News = 1,
        FriendTalking = 2,
        ReporterTalking = 3,
        BothTalking = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();

        if (contents != null) 
            currentSprite.sprite = contents[0];
    }

    public void UpdateContent(TVContentID contentID)
    {
        if (contents == null || contents.Count <= 0)
            return;

        if ((contents.Count - 1) < (int)contentID)
        {
            Debug.LogWarning("TV Content doesn't have choosen ID");
            return;
        }

        currentSprite.sprite = contents[(int)contentID];
    }

}
