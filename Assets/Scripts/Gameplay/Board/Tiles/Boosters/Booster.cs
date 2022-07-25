using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Booster : ABoardElement, IDestroyable
{
    protected Booster(BoardManager boardClient, Vector2Int position) : base(boardClient, position)
    {
        gravityAffected = true;
    }

    public List<ABoardElement> GetBoosterNeighbours()
    {
        var outputList = new List<ABoardElement>();
        foreach(var n in Neighbours)
        {
            if(n is Booster)
            {
                outputList.Add(n);
            }
        }

        return outputList;
    }

    public abstract void Remove();
    public abstract bool Break();

    public abstract void DoEffect();

    public override bool CheckIfMatchable() => false;
}
