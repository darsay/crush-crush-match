using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class GameProgressionService : IService
{
    public GameData GameData;

    private static string path = Application.persistentDataPath + "/savefile.json";

    public void LoadGameData(GameConfigService config)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            CreateNewGameData(config);
            SaveGameData();
        }

        GameData.SetStatsToListen();
    }

    void CreateNewGameData(GameConfigService config)
    {
        GameData = new GameData();
        GameData.Tokens = config.InitialTokens;

        GameData.CellEraser = config.InitialCellEraser;
        GameData.RowEraser = config.InitialRowEraser;
        GameData.ColumnEraser = config.InitialColumnsEraser;
        GameData.ShuffleBooster = config.InitialShuffle;

        GameData.PlayerIcon = config.InitialPlayerIcon;
        GameData.CrushIcon = config.InitialCrushIcon;

        GameData.shopItems = new List<bool>() { true, true, true, true, true };
        GameData.IconsList = config.InitialSkins;

    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void SaveGameData()
    {
        string json = JsonUtility.ToJson(GameData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Clear()
    {

    }
}
