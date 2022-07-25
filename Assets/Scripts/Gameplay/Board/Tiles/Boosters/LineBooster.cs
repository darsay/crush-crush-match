using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineBooster : Booster
{
    public bool IsVertical { get; set; }

    public LineBooster(BoardManager boardClient, Vector2Int position) : base(boardClient, position)
    {
        GraphicId = Random.Range(0, 2);

        IsVertical = GraphicId == 1;
    }

    public override bool Break()
    {
        DoEffect();

        BoardEventManager.NotifyOnBoardUpdated();

        return true;
    }

    void VerticalBreak()
    {
        for(int i = 0; i < BoardModel.LengthY; i++)
        {
            if (BoardModel.Board[Position.x, i] is not IDestroyable) continue;

            (BoardModel.Board[Position.x, i] as IDestroyable).Remove();
        }
    }

    void HorizontalBreak()
    {
        for (int i = 0; i < BoardModel.LengthY; i++)
        {
            if (BoardModel.Board[i, Position.y] is not IDestroyable) continue;

            (BoardModel.Board[i, Position.y] as IDestroyable).Remove();
        }
    }

    public override void Remove()
    {
        DoEffect();
    }

    public override string ToString()
    {
        if(IsVertical) return "|";

        return "-";

    }

    public override void DoEffect()
    {
        int boosterNeighbours = 0;

        for (int i = 0; i < Neighbours.Count; i++)
        {
            if (Neighbours[i] is LineBooster)
            {
                var pos = Neighbours[i].Position;
                boosterNeighbours++;
                BoardModel.Board[pos.x, pos.y] = new EmptyTile(BoardModel, pos);
            }
        }

        if (boosterNeighbours > 0)
        {

            BoardModel.Board[Position.x, Position.y] = new AxisBooster(BoardModel, Position);
            (BoardModel.Board[Position.x, Position.y] as AxisBooster).Break();
        }

        BoardModel.Board[Position.x, Position.y] = new EmptyTile(BoardModel, Position);

        if (IsVertical)
        {
            VerticalBreak();
        }
        else
        {
            HorizontalBreak();
        }
    }
}
