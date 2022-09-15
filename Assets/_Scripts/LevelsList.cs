using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsList : MonoBehaviour
{
    [SerializeField] RectTransform levelsPopUp;
    [SerializeField] RectTransform contentRt;
    [SerializeField] GameObject levelButtonPrefab;

    CanvasGroup _canvasGroup;

    int lastLevel;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        lastLevel = ServiceLocator.GetService<GameProgressionService>().GameData.LevelUnlocked-1;

    }

    private void Start()
    {
        InitLevelsList();
    }

    void InitLevelsList()
    {
        var lastLevel = ServiceLocator.GetService<GameProgressionService>().GameData.LevelUnlocked-1;

        for(int i = 0; i <= lastLevel; i++)
        {
            var level = Instantiate(levelButtonPrefab, contentRt).GetComponent<LevelLoaderButton>();
            level.SetLevel(i);
        } 
    }

    public void OpenLevelList()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1, 0.5f);

        levelsPopUp.DOScale(1, 0.5f).SetEase(Ease.OutElastic);
    }

    public void CloseLevelList()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, 0.5f);

        levelsPopUp.localScale = Vector2.zero;
    }
}
