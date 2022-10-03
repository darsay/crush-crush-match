using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataEventManager
{
    public static event Action<int> OnLevelUnlockedIdChange = delegate (int amount) { };
    public static event Action<int> OnLevelToPlayChange = delegate (int amount) { };

    public static event Action<int> OnLifesChange = delegate (int amount) { };
    public static event Action<int> OnCoinsChange = delegate (int amount) { };

    public static event Action<int> OnCellEraserChange = delegate (int amount) { };
    public static event Action<int> OnColumnEraserChange = delegate (int amount) { };
    public static event Action<int> OnRowEraserChange = delegate (int amount) { };
    public static event Action<int> OnShuffleChange = delegate (int amount) { };
    public static event Action<int> OnPlayerIconChange = delegate (int icon) { };
    public static event Action<int> OnCrushIconChange = delegate (int icon) { };

    public static event Action<int> OnSkinAdded = delegate (int icon) { };
    public static event Action<int> OnBuyMade = delegate (int id) { };

    public static event Action<bool> OnAplicationPaused = delegate (bool isPause) { };

    public static void NotifyOnLevelUnlockedIdChange(int amount) => OnLevelUnlockedIdChange(amount);
    public static void NotifyOnLevelToPlayChange(int amount) => OnLevelToPlayChange(amount);
    public static void NotifyOnLifesChange(int amount) => OnLifesChange(amount);
    public static void NotifyOnCoinsChange(int amount) => OnCoinsChange(amount);

    public static void NotifyOnCellEraserChange(int amount) => OnCellEraserChange(amount);
    public static void NotifyOnColumnEraserChange(int amount) => OnColumnEraserChange(amount);
    public static void NotifyOnRowEraserChange(int amount) => OnRowEraserChange(amount);
    public static void NotifyOnShuffleChange(int amount) => OnShuffleChange(amount);

    public static void NotifyOnPlayerIconChange(int icon) => OnPlayerIconChange(icon);
    public static void NotifyOnCrushIconChange(int icon) => OnCrushIconChange(icon);

    public static void NotifyOnSkinAdded(int icon) => OnSkinAdded(icon);
    public static void NotifyOnBuyMade(int id) => OnBuyMade(id);

    public static void NotifyOnAplicationPaused(bool isPaused) => OnAplicationPaused(isPaused);
    
}
