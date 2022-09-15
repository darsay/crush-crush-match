using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class IconLoaderService : IService
{

    private readonly string baseName = "icon";
    private AsyncOperationHandle _currentHandle;


    public void LoadIcon(int id, Action<Sprite> displayIcon)
    {
        string fullName = baseName;

        if(id < 10)
        {
            fullName += $"0{id.ToString()}"; 
        }
        else
        {
            fullName += id.ToString();
        }

        Addressables.LoadAssetAsync<Sprite>(fullName).Completed += handle =>
        {

            displayIcon.Invoke(handle.Result);
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
