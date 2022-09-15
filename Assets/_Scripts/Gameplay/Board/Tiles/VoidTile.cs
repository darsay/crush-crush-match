using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTile : ABoardElement
{
    public VoidTile(Vector2Int position) : base(position)
    {
        IsGravityAffected = false;
    }

    public override bool Break(ABoardElement[,] board)
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckIfMatchable() => false;

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        throw new System.NotImplementedException();
    }
}
