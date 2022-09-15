using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinScreen : MonoBehaviour
{
    CanvasGroup canvasGroup;
    bool _hasWon;

    [SerializeField] RectTransform winPopUp;

    int _reward;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();   
        _reward = ServiceLocator.GetService<RemoteConfigGameService>().GetInt("TokensPerWin");
    }

    private void OnEnable()
    {
        BoardEventManager.OnWin += OnWin;
    }

    private void OnDisable()
    {
        BoardEventManager.OnWin -= OnWin;
    }

    void OnWin()
    {
        var data = ServiceLocator.GetService<GameProgressionService>().GameData;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 0.5f);
        BoardEventManager.NotifyOnInputLocked(true);

        var parameters = new Dictionary<string, object>();
        parameters.Add("userLevel", ServiceLocator.GetService<GameProgressionService>().GameData.LevelToPlay);
        ServiceLocator.GetService<AnalyticsGameService>().SendEvent("levelWin", parameters);

        GameDataEventManager.NotifyOnCoinsChange(data.Tokens + _reward);

        

        if (data.LevelToPlay == data.LevelUnlocked)
        {
            GameDataEventManager.NotifyOnLevelUnlockedIdChange(data.LevelUnlocked + 1);          
        }

        winPopUp.DOScale(1, 0.5f).SetEase(Ease.OutElastic);

        ServiceLocator.GetService<GameProgressionService>().SaveGameData();
    }
}
