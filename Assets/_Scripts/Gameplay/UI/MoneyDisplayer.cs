using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        text.text = $"{ServiceLocator.GetService<GameProgressionService>().GameData.Tokens}";
    }

    private void OnEnable()
    {
        GameDataEventManager.OnCoinsChange += OnCoinsChange;
    }

    private void OnDisable()
    {
        GameDataEventManager.OnCoinsChange -= OnCoinsChange;
    }

    void OnCoinsChange(int newAmount)
    {
        text.text = $"{newAmount}";
    }
}
