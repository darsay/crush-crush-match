using UnityEngine;

public class ColorHeart : BasicHeart
{
    protected EmojiColor color;

    public ColorHeart(Vector2Int position) : base(position)
    {
    }

    public override void CheckIfNeighbourBroken(Vector2Int cellBroken, ABoardElement[,] board)
    {
        foreach (var n in Neighbours)
        {
            if (n is not EmojiBlock) continue;

            var emojiTile = n as EmojiBlock;

            if (cellBroken == emojiTile.Position && emojiTile.Color == color)
            {
                Remove(board, false);
            }
        }
    }

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        hitsToBreak--;

        board[Position.x, Position.y] = new EmptyTile(Position);
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

        NotifyByColor();

        BoardEventManager.OnTileMatched -= CheckIfNeighbourBroken;
    }

    void NotifyByColor()
    {
        switch (color)
        {
            case EmojiColor.Red:
                BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.RedBaloon);
                break;

            case EmojiColor.Green:
                BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.GreenBaloon);
                break;

            case EmojiColor.Blue:
                BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.BlueBaloon);
                break;

            case EmojiColor.Yellow:
                BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.YellowBaloon);
                break;

            case EmojiColor.Purple:
                BoardEventManager.NotifyOnObjectiveUpdated(1, ObjectiveType.PurpleBaloon);
                break;
        }
    }
}
