using UnityEngine;

public class BaloonHeart : BasicHeart
{
    public BaloonHeart(Vector2Int position) : base(position)
    {
        IsGravityAffected = false;

        GraphicId = 1;
    }

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        hitsToBreak--;

        board[Position.x, Position.y] = new EmptyTile(Position);
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

        BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.HeartBaloon);

        BoardEventManager.OnTileMatched -= CheckIfNeighbourBroken;
    }
}
