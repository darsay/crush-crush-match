using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardEventManager
{
    public static event Action<Vector2Int> OnTileDestroyed = delegate(Vector2Int pos) { };
    public static event Action OnBoardUpdated = delegate () { };
    public static event Action<Vector2Int, Vector2Int> OnTileAnim = delegate (Vector2Int pos, Vector2Int target) { };
    public static event Action<int, EmojiColor> OnEmojisMatched = delegate (int amount, EmojiColor color) { };

    public static event Action OnWin = delegate () { };
    public static event Action OnDefeat = delegate () { };

    public static void NotifyOnTileDestroyed(Vector2Int position)
    {
        OnTileDestroyed(position);
    }

    public static void NotifyOnBoardUpdated()
    {
        OnBoardUpdated();
    }

    public static void NotifyOnTileAnim(Vector2Int pos, Vector2Int target)
    {
        OnTileAnim(pos, target);
    }

    public static void NotifyOnEmojisMatched(int amount, EmojiColor color)
    {
        OnEmojisMatched(amount, color);
    }

    public static void NotifyOnWin()
    {
        OnWin();
    }

    public static void NotifyOnDefeat()
    {
        OnDefeat();
    }
}
