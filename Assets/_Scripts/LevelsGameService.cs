using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelsGameService : IService
{
    public int LevelsCount;

    public Level LevelToPlay;

    string _levelBaseName = "Level";

    string _path = Application.dataPath + "/Levels";


    private AsyncOperationHandle _currentHandle;

    public void Initialize()
    {
        DirectoryInfo directory = new DirectoryInfo(_path);

        LevelsCount  = directory.GetFiles().Length/2;

        GameDataEventManager.OnLevelToPlayChange += SetLevel;
    }

    public void SetLevel(int i)
    {
        ServiceLocator.GetService<GameProgressionService>().GameData.LevelToPlay = i;

        string fullName = _levelBaseName;

        if (i < 10)
        {
            fullName += $"00{i.ToString()}";
        }
        else if (i < 100)
        {
            fullName += $"0{i.ToString()}";
        }
        else
        {
            fullName += i.ToString();
        }

        Addressables.LoadAssetAsync<Level>(fullName).Completed += handle =>
        {
            LevelToPlay = handle.Result;

            if (_currentHandle.IsValid())
            {
                Addressables.Release(handle);
            }

            _currentHandle = handle;
        };
    }

    public void Clear()
    {

    }

}
