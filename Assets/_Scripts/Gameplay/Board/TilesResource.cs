using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesResource : MonoBehaviour
{
    public GameObject[] BaseEmoji;

    public GameObject[] Boosters;

    public Sprite[] ObjectiveSprites;

    public GameObject[] Blockers;

    public GameObject empty;


    public GameObject GetTile(ABoardElement element)
    {

        if (element is EmojiBlock) return BaseEmoji[element.GraphicId];

        if (element is Booster) return Boosters[element.GraphicId];

        if(element is Blocker) return Blockers[element.GraphicId];

        if (element is EmptyTile) return empty;

        return null;
    }

    public Sprite GetObjectiveSprite(ObjectiveType type)
    {
        return ObjectiveSprites[(int)type];
    }

}
