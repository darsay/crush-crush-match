using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector] public Level Level;
    [SerializeField] Level defaultLevel;

    private void Awake()
    {
        Level = ServiceLocator.GetService<LevelsGameService>().LevelToPlay;
    }

    private void Start()
    {
        if (Level == null) Level = defaultLevel;


        BoardEventManager.NotifyOnLevelLoaded(Level);
    }

}
