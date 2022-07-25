using UnityEngine;

public class BombBooster : Booster
{
    public BombBooster(BoardManager boardClient, Vector2Int position) : base(boardClient, position)
    {
        GraphicId = 2;
    }

    public override bool Break()
    {
        DoEffect();

        BoardEventManager.NotifyOnBoardUpdated();
        BoardEventManager.NotifyOnTileDestroyed(Position);

        return true;
    }

    public override void Remove()
    {
        BoardEventManager.NotifyOnTileDestroyed(Position);
        DoEffect();
    }

    public override void DoEffect()
    {
        int startY = Position.y == 0 ? 0 : Position.y - 1;
        int endY = Position.y == BoardModel.LengthY - 1 ? Position.y : Position.y + 1;

        int startX = Position.x == 0 ? 0 : Position.x - 1;
        int endX = Position.x == BoardModel.LengthX - 1 ? Position.x : Position.x + 1;

        BoardModel.Board[Position.x, Position.y] = new EmptyTile(BoardModel, Position);

        for (int i = startY; i <= endY; i++)
        {
            for (int j = startX; j <= endX; j++)
            {
                var tile = BoardModel.Board[j, i] as IDestroyable;

                if (tile != null)
                {
                    tile.Remove();
                }
            }
        }
    }

    public override string ToString()
    {
        return "Bomb";
    }
}
