using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : ABoardElement
{
    public EmptyTile(Vector2Int Position) : base(Position)
    {
        IsGravityAffected = false;

    }

    public override string ToString()
    {
        return "Empty";
    }

    public override bool CheckIfMatchable() => false;

    public override void Remove(ABoardElement[,] board, bool isMatched) { }

    public override bool Break(ABoardElement[,] board) { return false; }
}
