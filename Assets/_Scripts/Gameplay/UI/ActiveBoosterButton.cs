using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBoosterButton : MonoBehaviour
{

    [SerializeField] ActiveBooster booster;
    [SerializeField] TextMeshProUGUI usesLeftText;
    [SerializeField] string description;
    [SerializeField] ActiveBoosterTooltip tooltip;

    Button _button;
    int _usesLeft;
    Image _boosterImage;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _boosterImage = GetComponent<Image>();
    }

    private void SetUsesLeft()
    {
        var gameData = ServiceLocator.GetService<GameProgressionService>().GameData;
        switch (booster)
        {
            case ActiveBooster.CellBreaker:
                _usesLeft = gameData.CellEraser;
                break;

            case ActiveBooster.ColumnBreaker:
                _usesLeft = gameData.ColumnEraser;
                break;

            case ActiveBooster.LineBreaker:
                _usesLeft = gameData.RowEraser;
                break;

            case ActiveBooster.Shuffle:
                _usesLeft = gameData.ShuffleBooster;
                break;
        }
        
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnPressed);
        BoardEventManager.OnActiveBoosterUsed += OnUsed;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnPressed);
        BoardEventManager.OnActiveBoosterUsed -= OnUsed;

    }

    private void Start()
    {

        SetUsesLeft();
        usesLeftText.text = _usesLeft.ToString();
    }

    void OnPressed()
    {
        if (_usesLeft <= 0) return;

        BoardEventManager.NotifyOnActiveBoosterSet(booster);
        tooltip.Show(description, _boosterImage.sprite);
    }

    void OnUsed(ActiveBooster booster)
    {
        if (booster != this.booster) return;

        --_usesLeft;
        NotifyUsesLeft();
        usesLeftText.text = _usesLeft.ToString();
    }

    private void NotifyUsesLeft()
    {
        switch (booster)
        {
            case ActiveBooster.CellBreaker:
                GameDataEventManager.NotifyOnCellEraserChange(_usesLeft);
                break;

            case ActiveBooster.ColumnBreaker:
                GameDataEventManager.NotifyOnColumnEraserChange(_usesLeft);
                break;

            case ActiveBooster.LineBreaker:
                GameDataEventManager.NotifyOnRowEraserChange(_usesLeft);
                break;

            case ActiveBooster.Shuffle:
                GameDataEventManager.NotifyOnShuffleChange(_usesLeft);
                break;
        }
    }
}
