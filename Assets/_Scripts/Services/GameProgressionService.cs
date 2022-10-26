using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class GameProgressionService : IService
{
    public GameData GameData;


    private IGameProgressionProvider _progressionProvider;

    CloudGameProgressionProvider _cloudProvider;
    FileGameProgressionProvider _fileProvider;

    bool _isCloud;


    public async Task Initialize(GameConfigService gameConfig, int isCloud)
    {
        _cloudProvider = new CloudGameProgressionProvider();
        _fileProvider = new FileGameProgressionProvider();

        await _cloudProvider.Initialize();

        await _fileProvider.Initialize();

        if (isCloud != 0)
        {
            _isCloud = true;

            _progressionProvider = _cloudProvider;
        }
        else
        {
            _isCloud = false;

            _progressionProvider = _fileProvider;
        }

        LoadGameData(gameConfig);

        GameDataEventManager.OnAplicationPaused += SaveWhenPaused;
    }

    public void LoadGameData(GameConfigService config)
    {
        string data = _progressionProvider.Load();

        if (string.IsNullOrEmpty(data))
        {
            CreateNewGameData(config);
            SaveGameData();
        }
        else
        {
            GameData = JsonUtility.FromJson<GameData>(data);
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

        GameData.ShopItems = new List<int>();
        GameData.IconsList = config.InitialSkins;

    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void SaveGameData()
    {
        if (_isCloud)
        {
            _cloudProvider.Save(JsonUtility.ToJson(GameData));

        }

        _fileProvider.Save(JsonUtility.ToJson(GameData));
    }

    public void Clear()
    {

    }

    void SaveWhenPaused(bool isPaused)
    {
        if (isPaused) SaveGameData();
    }

    public void ChangeProvider(bool isCloud)
    {
        if (isCloud)
        {
            _isCloud = true;

            _progressionProvider = _cloudProvider;
        }
        else
        {
            _isCloud = false;

            _progressionProvider = _fileProvider;
        }
    }
}
