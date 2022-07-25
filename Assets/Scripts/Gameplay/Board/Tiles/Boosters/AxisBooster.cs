using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisBooster : Booster
{


    public AxisBooster(BoardManager boardClient, Vector2Int position) : base(boardClient, position) {}
    public override bool Break()
    {
        DoEffect();

        BoardEventManager.NotifyOnBoardUpdated();

        return true;
    }

    public override void Remove()
    {
        Break();
    }

    void VerticalBreak()
    {
        for (int i = 0; i < BoardModel.LengthY; i++)
        {
            if (BoardModel.Board[Position.x, i] is not IDestroyable) continue;

            (BoardModel.Board[Position.x, i] as IDestroyable).Remove();
        }
    }

    void HorizontalBreak()
    {
        for (int i = 0; i < BoardModel.LengthX; i++)
        {
            if (BoardModel.Board[i, Position.y] is not IDestroyable) continue;

            (BoardModel.Board[i, Position.y] as IDestroyable).Remove();
        }
    }

    public override string ToString()
    {
        return "+";
    }

    public override void DoEffect()
    {
        BoardModel.Board[Position.x, Position.y] = new EmptyTile(BoardModel, Position);

        VerticalBreak();
        HorizontalBreak();
    }
}
