using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayer : MonoBehaviour
{
    TextMeshProUGUI _text;
    Button _button;

    private void Start()
    {
        var levelUnlocked = ServiceLocator.GetService<GameProgressionService>().GameData.LevelUnlocked;

        _text = GetComponentInChildren<TextMeshProUGUI>();

        _text.text = $"Level {levelUnlocked + 1}";

        _button = GetComponentInChildren<Button>();

        if (levelUnlocked >= ServiceLocator.GetService<LevelsGameService>().LevelsCount)
        {
            _button.interactable = false;
            _text.text = $"More levels to come!";
        }
    }
}
