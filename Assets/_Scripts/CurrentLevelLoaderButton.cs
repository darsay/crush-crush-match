using UnityEngine;

public class CurrentLevelLoaderButton : LevelLoaderButton
{
    protected override void Start()
    {


        LevelToLoad = ServiceLocator.GetService<GameProgressionService>().GameData.LevelUnlocked;


        base.Start();

        if (LevelToLoad >= ServiceLocator.GetService<LevelsGameService>().LevelsCount)
        {
            _button.interactable = false;
            _text.text = $"More levels to come!";
        }
    }
}
