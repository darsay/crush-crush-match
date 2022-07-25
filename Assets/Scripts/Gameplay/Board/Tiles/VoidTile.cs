using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTile : ABoardElement
{
    public VoidTile(BoardManager boardClient,Vector2Int position) : base(boardClient,position)
    {
        gravityAffected = false;
    }

    public override bool CheckIfMatchable() => false;

}
