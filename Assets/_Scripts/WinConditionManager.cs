using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class WinConditionManager : MonoBehaviour
{

    [SerializeField]
    RectTransform uiCanvas;

    [SerializeField]
    AssetReference winPopUp;

    [SerializeField]
    AssetReference losePopUp;

    AsyncOperationHandle _currentHandle;

    Action _loseAction;
    LoseScreen _loseScreen;


    private void Awake()
    {
        _loseAction = OnLose;
    }

    private  void OnEnable()
    {
        BoardEventManager.OnWin += OnWin;
        BoardEventManager.OnDefeat += _loseAction;
    }

    private void OnDisable()
    {
        BoardEventManager.OnWin -= OnWin;
        BoardEventManager.OnDefeat -= _loseAction;
    }
    public void OnWin()
    {
        winPopUp.LoadAssetAsync<GameObject>().Completed += handle =>
        {
            var popUp = Instantiate(handle.Result, uiCanvas);
            popUp.GetComponent<WinScreen>().OnWin();

            if (_currentHandle.IsValid())
            {
                Addressables.Release(handle);
                winPopUp.ReleaseAsset();
            }

            _currentHandle = handle;
        };
    }

    public void OnLose()
    {
        losePopUp.LoadAssetAsync<GameObject>().Completed += handle =>
        {
            var popUp = Instantiate(handle.Result, uiCanvas);
            _loseScreen = popUp.GetComponent<LoseScreen>();
            _loseScreen.OnDefeat();


            BoardEventManager.OnDefeat -= _loseAction;
            _loseAction = OnLoseCached;
            BoardEventManager.OnDefeat += _loseAction;


            if (_currentHandle.IsValid())
            {
                Addressables.Release(handle);
                losePopUp.ReleaseAsset();
            }

            _currentHandle = handle;
        };
    }

    public void OnLoseCached()
    {
        _loseScreen.OnDefeat();
    }
}
