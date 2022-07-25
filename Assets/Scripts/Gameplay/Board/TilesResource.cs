using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesResource : MonoBehaviour
{
    public GameObject[] BaseEmoji;

    public GameObject[] Boosters;


    public GameObject GetTile(ABoardElement element)
    {

        if (element is EmojiBlock) return BaseEmoji[element.GraphicId];

        if (element is Booster) return Boosters[element.GraphicId];

        return null;
    }
}
