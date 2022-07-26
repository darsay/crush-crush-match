using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LoseScreen : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField] CanvasGroup tryAgainPopCG;
    [SerializeField] CanvasGroup gameOverCG;
    [SerializeField] TextMeshProUGUI retryText;

    int _priceToContinue;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _priceToContinue = ServiceLocator.GetService<RemoteConfigGameService>().GetInt("TokensToRetry");
        retryText.text = $"+5 moves for {_priceToContinue} tokens";
    }


    public void OnDefeat()
    {
        BoardEventManager.NotifyOnInputLocked(true);
        canvasGroup.DOFade(1, 0.5f);
        canvasGroup.blocksRaycasts = true;
        tryAgainPopCG.blocksRaycasts = true;

        var parameters = new Dictionary<string, object>();
        parameters.Add("userLevel", ServiceLocator.GetService<GameProgressionService>().GameData.LevelToPlay);
        ServiceLocator.GetService<AnalyticsGameService>().SendEvent("levelLost", parameters);
    }

    public void Continue()
    {
        int currentCoins = ServiceLocator.GetService<GameProgressionService>().GameData.Tokens;
        if (currentCoins < 0) return;

        if (currentCoins < _priceToContinue) return;

        BoardEventManager.NotifyOnInputLocked(false);

        BoardEventManager.NotifyOnContinue(5);
        GameDataEventManager.NotifyOnCoinsChange(currentCoins - _priceToContinue);
        ServiceLocator.GetService<GameProgressionService>().SaveGameData();

        canvasGroup.DOFade(0, 0.5f);
        canvasGroup.blocksRaycasts = false;
        tryAgainPopCG.blocksRaycasts = false;
    }

    public void ContinueAfterAd()
    {

        BoardEventManager.NotifyOnInputLocked(false);

        BoardEventManager.NotifyOnContinue(5);
        ServiceLocator.GetService<GameProgressionService>().SaveGameData();

        canvasGroup.DOFade(0, 0.5f);
        canvasGroup.blocksRaycasts = false;
        tryAgainPopCG.blocksRaycasts = false;
    }

    public void GameOver()
    {
        tryAgainPopCG.DOFade(0, 0.5f);
        tryAgainPopCG.blocksRaycasts = false;

        gameOverCG.DOFade(1, 0.5f);
        gameOverCG.blocksRaycasts = true;
        ServiceLocator.GetService<GameProgressionService>().SaveGameData();
    }

    
}
