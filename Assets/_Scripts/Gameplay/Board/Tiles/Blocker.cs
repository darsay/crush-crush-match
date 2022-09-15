using UnityEngine;

public class Blocker : ABoardElement
{
    public Blocker(Vector2Int position) : base(position) { }

    public override bool Break(ABoardElement[,] board) => false;

    public override bool CheckIfMatchable() => false;

    public override void Remove(ABoardElement[,] board, bool isMatched) { }
}
