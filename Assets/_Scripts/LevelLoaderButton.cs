using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLoaderButton : SceneLoaderButton
{
    public int LevelToLoad;

    [SerializeField] protected TextMeshProUGUI _text;


    virtual protected void Start()
    {
        _text.text = $"Level {LevelToLoad + 1}";

    }

    override protected void LoadScene()
    {
        GameDataEventManager.NotifyOnLevelToPlayChange(LevelToLoad);
        var parameters = new Dictionary<string, object>();
        parameters.Add("userLevel", LevelToLoad);
        ServiceLocator.GetService<AnalyticsGameService>().SendEvent("levelStart", parameters);
        base.LoadScene();
    }

    public void SetLevel(int l)
    {
        LevelToLoad = l;
        _text.text = $"Level {LevelToLoad + 1}";
    }
}
