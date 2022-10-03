using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.Advertisements;
public class AdsGameService : IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener,
    IService
{
    private string _adsGameId;
    private string _adUnitId;

    private bool _isAdLoaded;

    public bool IsAdReady => _isAdLoaded;
    public bool IsInitialized => _initializationTask == TaskStatus.RanToCompletion;

    private TaskStatus _initializationTask = TaskStatus.Created;
    private TaskStatus _showTaskStatus = TaskStatus.Created;

    private Action<bool> _onDone = null;

    public AdsGameService(string adsGameId, string adUnitId)
    {
        _adsGameId = adsGameId;
        _adUnitId = adUnitId;
    }

    public async Task<bool> Initialize(bool testMode = false, Action<bool> onDone = null)
    {
        _initializationTask = TaskStatus.Running;
        Advertisement.Initialize(_adsGameId, testMode, this);

        while (_initializationTask == TaskStatus.Running)
        {
            await Task.Delay(500);
        }

        return IsInitialized;
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
        _initializationTask = TaskStatus.RanToCompletion;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        _initializationTask = TaskStatus.Faulted;
    }

    private void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        _isAdLoaded = true;
        Debug.Log("Ad Loaded: " + adUnitId);
    }

    public async Task<bool> ShowAd()
    {

        if (_showTaskStatus == TaskStatus.Running)
            return false;

        if (!IsInitialized)
            return false;

        if (!IsAdReady)
            return false;

        _showTaskStatus = TaskStatus.Running;

        
        Advertisement.Show(_adUnitId, this);
#if UNITY_EDITOR
        await Task.Delay(2000);
        OnUnityAdsShowComplete(_adUnitId, UnityAdsShowCompletionState.COMPLETED);
#endif

        while(_showTaskStatus == TaskStatus.Running)
        {
            await Task.Delay(500);
        }


        return _showTaskStatus == TaskStatus.RanToCompletion;
    }

    private async void DelayedDebugWatch()
    {
        await Task.Delay(2000);
        OnUnityAdsShowComplete(_adUnitId, UnityAdsShowCompletionState.COMPLETED);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Unity Ads Rewarded Ad:" + showCompletionState.ToString());
        Advertisement.Load(_adUnitId, this);
        _showTaskStatus = showCompletionState == UnityAdsShowCompletionState.COMPLETED ? TaskStatus.RanToCompletion : TaskStatus.Faulted;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        Advertisement.Load(_adUnitId, this);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        Advertisement.Load(_adUnitId, this);

        _showTaskStatus = TaskStatus.Faulted;
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log("Started watching an ad");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("User clicked in the ad");
    }

    public void Clear()
    {
    }
}

