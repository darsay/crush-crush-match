using System;
using System.Collections;
using UnityEngine;


public class MegaBomb : Booster
{

    public MegaBomb(Vector2Int position) : base(position)
    {
        GraphicId = 5;
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

    public override void DoEffect(ABoardElement[,] board)
    {
        int startY = Math.Clamp(Position.y - 2, 0, board.GetLength(1)-1);
        int endY = Math.Clamp(Position.y + 2, 0, board.GetLength(1)-1);

        int startX = Math.Clamp(Position.x - 2, 0, board.GetLength(0)-1);
        int endX = Math.Clamp(Position.x + 2, 0, board.GetLength(0)-1);

        board[Position.x, Position.y] = new EmptyTile(Position);

        for (int i = startY; i <= endY; i++)
        {
            for (int j = startX; j <= endX; j++)
            {
                var tile = board[j, i];

                if (tile != null)
                {
                    tile.Remove(board, false);
                }
            }
        }
    }

    public override string ToString()
    {
        return "MegaB";
    }

}