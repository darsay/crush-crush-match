using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingMoves : MonoBehaviour
{
    [SerializeField] int moves;
    [SerializeField] TextMeshProUGUI movesText;

    bool _hasWon;

    private void OnEnable()
    {
        BoardEventManager.OnLevelLoaded += InitMoves;
        BoardEventManager.OnMoveDone += UpdateMoves;
        BoardEventManager.OnWin += OnWin;
    }

    private void OnDisable()
    {
        BoardEventManager.OnMoveDone -= UpdateMoves;
        BoardEventManager.OnWin -= OnWin;
    }
    

    void InitMoves(Level level)
    {
        moves = level.MovesLimit;
        movesText.text = $"{moves}";
    }



    void UpdateMoves()
    {
        --moves;
        movesText.text = $"{moves}";

        if (moves == 0 && !_hasWon)
        {
            BoardEventManager.NotifyOnDefeat();
        }
    }

    void OnWin()
    {
        _hasWon = true;
    }

    public void SetMoves(int newMoves)
    {
        moves = newMoves;
        movesText.text = $"{moves}";
    }

}
