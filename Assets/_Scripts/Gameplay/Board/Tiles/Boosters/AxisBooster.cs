using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisBooster : Booster
{
    public AxisBooster(Vector2Int position) : base(position) {
        GraphicId = 3;
    }
    public override bool Break(ABoardElement[,] board)
    {
        DoEffect(board);

        return true;
    }

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        Break(board);
    }

    void VerticalBreak(ABoardElement[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            board[Position.x, i].Remove(board, false);
        }
    }

    void HorizontalBreak(ABoardElement[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (i== Position.x) continue;
            board[i, Position.y].Remove(board, false);
        }
    }

    public override string ToString()
    {
        return "+";
    }

    public override void DoEffect(ABoardElement[,] board)
    {
        board[Position.x, Position.y] = new EmptyTile(Position);

        VerticalBreak(board);
        HorizontalBreak(board);
    }
}
