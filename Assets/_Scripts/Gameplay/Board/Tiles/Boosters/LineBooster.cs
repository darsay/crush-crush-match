using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineBooster : Booster
{
    public bool IsVertical { get; set; }

    public LineBooster(Vector2Int position) : base(position)
    {
        GraphicId = Random.Range(0, 2);

        IsVertical = GraphicId == 1;
    }

    public LineBooster(Vector2Int position, bool isVertical) : base(position)
    {
        GraphicId = isVertical ? 1 : 0;

        IsVertical = isVertical;
    }

    public override bool Break(ABoardElement[,] board)
    {
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

        DoEffect(board);

        BoardEventManager.NotifyOnMoveDone();
        BoardEventManager.NotifyOnBoardUpdated();

        return true;
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
            board[i, Position.y].Remove(board, false);
        }
    }

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
        dontCombine = true;
        DoEffect(board);
    }

    public override string ToString()
    {
        if(IsVertical) return "|";

        return "-";

    }

    public override void DoEffect(ABoardElement[,] board)
    {
        int prioritaryBoosterType = 0;

        if (!dontCombine)
        {
            for (int i = 0; i < Neighbours.Count; i++)
            {
                if (Neighbours[i] is BombBooster)
                {
                    var pos = Neighbours[i].Position;
                    prioritaryBoosterType = 2;
                    board[pos.x, pos.y].Dissappear(board);
                    continue;
                }

                if (Neighbours[i] is LineBooster)
                {
                    var pos = Neighbours[i].Position;

                    if (prioritaryBoosterType < 2) prioritaryBoosterType = 1;

                    board[pos.x, pos.y].Dissappear(board);
                }


            }

            if (prioritaryBoosterType == 2)
            {
                board[Position.x, Position.y] = new AxisBomb(Position);
                BoardEventManager.NotifyOnBoosterGenerated(board[Position.x, Position.y] as Booster, Position);
                board[Position.x, Position.y].Break(board);
                BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
                board[Position.x, Position.y] = new EmptyTile(Position);
                return;
            }

            if (prioritaryBoosterType == 1)
            {
                board[Position.x, Position.y] = new AxisBooster(Position);
                BoardEventManager.NotifyOnBoosterGenerated(board[Position.x, Position.y] as Booster, Position);
                board[Position.x, Position.y].Break(board);
                BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
                board[Position.x, Position.y] = new EmptyTile(Position);
                return;
            }
        }

        
        board[Position.x, Position.y] = new EmptyTile(Position);

        if (IsVertical)
        {
            VerticalBreak(board);
        }
        else
        {
            HorizontalBreak(board);
        }




    }
}
