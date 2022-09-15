using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class LevelObjectiveView : MonoBehaviour
{
    [SerializeField] private int goal;

    [SerializeField] private ObjectiveType type;

    LevelObjectives goals;

    [SerializeField] TextMeshProUGUI goalProgressText;

    [SerializeField] private Image image;

    private void OnEnable()
    {
        BoardEventManager.OnObjectiveUpdated += UpdateObjective;
    }

    private void OnDisable()
    {
        BoardEventManager.OnObjectiveUpdated -= UpdateObjective;
    }

    void UpdateObjective(int amount, ObjectiveType matchType)
    {
        if (matchType != type) return;
        if (goal <= 0) return;

        goal -= amount;

        if (goal <= 0)
        {
            goal = 0;
            goals.ObjectiveComplete();
        }

        goalProgressText.text = goal.ToString();
    }

    public void Init(ObjectiveType type, int goal, LevelObjectives objectives, Sprite sprite)
    {
        goals = objectives;

        this.type = type;

        this.goal = goal;

        goalProgressText.text = goal.ToString();

        image.sprite = sprite;
    }
}
