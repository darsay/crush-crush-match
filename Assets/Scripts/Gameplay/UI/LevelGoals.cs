using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGoals : MonoBehaviour
{
   [SerializeField] List<LevelGoal> goals = new List<LevelGoal>();

    int goalsCount;

    private void Start()
    {
        goalsCount = goals.Count;
    }

    public void ObjectiveComplete()
    {
        goalsCount--;

        if(goalsCount == 0)
        {
            BoardEventManager.NotifyOnWin();
        }
    }


}
