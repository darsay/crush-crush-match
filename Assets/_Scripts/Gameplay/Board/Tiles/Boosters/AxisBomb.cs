using System.Collections;
using UnityEngine;


public class AxisBomb : Booster
{

    public AxisBomb(Vector2Int position) : base(position)
    {
        GraphicId = 4;
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
        int initx = Position.x > 0 ? Position.x - 1 : 0;
        int endx = Position.x < board.GetLength(1)-1 ? Position.x + 1 : board.GetLength(1) - 1;


        for (int i = initx; i <= endx; i++)
        {
            for (int j = 0; j <board.GetLength(1); j++)
            {
                board[i, j].Remove(board, false);
            }
        }

    }

    void HorizontalBreak(ABoardElement[,] board)
    {
        int inity = Position.y > 0 ? Position.y - 1 : 0;
        int endy = Position.y < board.GetLength(0)-1 ? Position.y + 1 : board.GetLength(0) - 1;


        for (int i = inity; i <= endy; i++)
        {
            for (int j = 0; j < board.GetLength(0); j++)
            {
                board[j, i].Remove(board, false);
            }
        }
    }

    public override string ToString()
    {
        return "AxisB";
    }

    public override void DoEffect(ABoardElement[,] board)
    {
        board[Position.x, Position.y] = new EmptyTile(Position);

        VerticalBreak(board);
        HorizontalBreak(board);
    }
}