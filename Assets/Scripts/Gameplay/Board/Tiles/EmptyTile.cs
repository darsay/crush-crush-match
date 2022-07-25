using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : ABoardElement
{
    public EmptyTile(BoardManager boardClient, Vector2Int Position) : base(boardClient, Position)
    {
        gravityAffected = false;
        BoardModel = boardClient;

    }

    public override string ToString()
    {
        return "Empty";
    }

    public override bool CheckIfMatchable() => false;
}
