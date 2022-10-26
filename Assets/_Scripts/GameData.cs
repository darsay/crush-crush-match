using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData
{
    // PROGRESS STATS
    public int LevelUnlocked;

    public int LevelToPlay;


    // MAIN STATS
    public int Tokens;
    public int Lifes;


    // ACTIVE BOOSTERS
    public int CellEraser;
    public int ColumnEraser;
    public int RowEraser;
    public int ShuffleBooster;

    // CUSTOMIZATION
    public int PlayerIcon;
    public int CrushIcon;

    public List<int> IconsList = new List<int>();

    public List<int> ShopItems = new List<int>();

    public void SetStatsToListen()
    {
        GameDataEventManager.OnLevelUnlockedIdChange += SetLevelUnlocked;

        GameDataEventManager.OnCoinsChange += SetCoins;
        GameDataEventManager.OnLifesChange += SetLifes;

        GameDataEventManager.OnCellEraserChange += SetCellEraser;
        GameDataEventManager.OnRowEraserChange += SetRowEraser;
        GameDataEventManager.OnColumnEraserChange += SetColumnEraser;
        GameDataEventManager.OnShuffleChange += SetShuffle;

        GameDataEventManager.OnPlayerIconChange += SetPlayerIcon;
        GameDataEventManager.OnCrushIconChange += SetCrushIcon;
        GameDataEventManager.OnSkinAdded += AddIcon;

        GameDataEventManager.OnBuyMade += OnBuyMade;

    }

    void SetLevelUnlocked(int amount) => LevelUnlocked = amount;

    void SetCoins(int amount) => Tokens = amount;
    void SetLifes(int amount) => Lifes = amount;

    void SetCellEraser(int amount) => CellEraser = amount;
    void SetColumnEraser(int amount) => ColumnEraser = amount;
    void SetRowEraser(int amount) => RowEraser = amount;
    void SetShuffle(int amount) => ShuffleBooster = amount;

    void SetPlayerIcon(int icon) => PlayerIcon = icon;
    void SetCrushIcon(int icon) => CrushIcon = icon;

    public void AddIcon(int newIcon)
    {
        if (IconsList.Contains(newIcon)) return;

        IconsList.Add(newIcon);
    }

    void OnBuyMade(int id)
    {
        ShopItems.Add(id);
    }
}
