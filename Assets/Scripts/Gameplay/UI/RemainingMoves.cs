using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingMoves : MonoBehaviour
{
    [SerializeField] int moves;
    [SerializeField] TextMeshProUGUI movesText;

    private void Start()
    {
        movesText.text = $"Moves: {moves}";

        BoardEventManager.OnBoardUpdated += UpdateMoves; 
    }

    void UpdateMoves()
    {
        --moves;

        if (moves == 0)
        {
            BoardEventManager.NotifyOnDefeat();
            return;
        }

        movesText.text = $"Moves: {moves}";

        


    }



}
