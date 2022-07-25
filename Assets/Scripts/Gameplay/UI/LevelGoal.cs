using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private int goal;

    [SerializeField] private EmojiColor color;

    LevelGoals goals;

    [SerializeField] TextMeshProUGUI goalProgressText;

    private void Awake()
    {
        goals = GetComponentInParent<LevelGoals>();

        goalProgressText.text = goal.ToString();
    }


    private void OnEnable()
    {
        BoardEventManager.OnEmojisMatched += UpdateObjecive;
    }

    void UpdateObjecive(int amount, EmojiColor matchColor)
    {
        if (matchColor != color) return;
        if (goal <= 0) return;

        goal -= amount;

        if (goal <= 0)
        {
            goal = 0;
            goals.ObjectiveComplete();
        }

        goalProgressText.text = goal.ToString();
    }
}
