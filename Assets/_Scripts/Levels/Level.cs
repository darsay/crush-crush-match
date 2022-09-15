using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/RegularLevel", order = 1)]
[System.Serializable]
public class Level : ScriptableObject
{
    public int LevelId;

    public int MovesLimit;

    public LevelObjective[] Objectives;

    public EmojiColor[] EmojiColorsToSpawn;

    [HideInInspector] public bool IsBoardPreDefined;

    [HideInInspector] public TilesTypes[] Board = new TilesTypes[81];

}
