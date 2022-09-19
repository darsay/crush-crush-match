using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    AssetReference settingsPopUp;

    [SerializeField]
    RectTransform parentCanvas;

    AsyncOperationHandle _currentHandle;

    UnityAction _openAction; 
    Button _button;
    SettingsMenu _popUpInstance;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _openAction = OpenSettings;
        _button.onClick.AddListener(_openAction);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(_openAction);
    }

    public void OpenSettings()
    {
        settingsPopUp.LoadAssetAsync<GameObject>().Completed += handle =>
        {
            var popUp = Instantiate(handle.Result, parentCanvas);
            _popUpInstance = popUp.GetComponent<SettingsMenu>();
            _popUpInstance.OpenSettings();

            _button.onClick.RemoveListener(_openAction);
            _openAction = OpenSettingsCached;
            _button.onClick.AddListener(_openAction);

            if (_currentHandle.IsValid())
            {
                Addressables.Release(handle);
                settingsPopUp.ReleaseAsset();
            }

            _currentHandle = handle;
        };
    }

    void OpenSettingsCached()
    {
        _popUpInstance.OpenSettings();
    }
}
