using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelObjectives : MonoBehaviour
{
    [SerializeField] List<LevelObjectiveView> objectives = new List<LevelObjectiveView>();
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] TilesResource tilesResource;

    int objectivesCount;

    private void OnEnable()
    {
        BoardEventManager.OnLevelLoaded += CreateObjectives;
        
    }

    private void OnDisable()
    {
        BoardEventManager.OnLevelLoaded -= CreateObjectives;
    }

    private void CreateObjectives(Level level)
    {
        var objectives = level.Objectives;

        foreach (var l in objectives)
        {
            var objectiveGO = Instantiate(objectivePrefab, transform);
            var objective = objectiveGO.GetComponent<LevelObjectiveView>();

            var sprite = tilesResource.GetObjectiveSprite(l.Type);
            objective.Init(l.Type, l.Goal, this, sprite);

            this.objectives.Add(objective);
        }

        objectivesCount = this.objectives.Count;
    }

    public void ObjectiveComplete()
    {
        objectivesCount--;

        BoardEventManager.NotifyOnObjectiveAchieved();

        if (objectivesCount == 0)
        {
            BoardEventManager.NotifyOnWin();
        }
    }

    public int GetObjectivesNum()
    {
        return objectives.Count;
    }

}
