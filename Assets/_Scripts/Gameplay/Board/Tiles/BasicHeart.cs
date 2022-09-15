using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHeart : Blocker
{
    protected int hitsToBreak;

    public BasicHeart(Vector2Int position) : base(position)
    {
        IsGravityAffected = true;

        BoardEventManager.OnTileMatched += CheckIfNeighbourBroken;
        hitsToBreak = 1;

        GraphicId = 0;
    }

    protected void CheckIfNeighbourBroken(Vector2Int cellBroken, ABoardElement[,] board)
    {    
        foreach (var n in Neighbours)
        {
            if (n is not EmojiBlock) continue;

            if (cellBroken == n.Position) Remove(board, false);
        }
    }

    public override bool Break(ABoardElement[,] board) => false;

    public override bool CheckIfMatchable() => false;

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        hitsToBreak--;

        if (hitsToBreak > 0) return;

        board[Position.x, Position.y] = new EmptyTile(Position);
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

        BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.Heart);

        BoardEventManager.OnTileMatched -= CheckIfNeighbourBroken;
    }

    public override string ToString()
    {
        return "Heart";
    }
}
