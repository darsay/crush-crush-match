using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Booster : ABoardElement
{
    protected bool dontCombine;


    protected Booster(Vector2Int position) : base(position)
    {
        IsGravityAffected = true;
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

    public abstract void DoEffect(ABoardElement[,] board);

    public override bool CheckIfMatchable() => false;
}
