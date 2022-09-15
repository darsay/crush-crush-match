using UnityEngine;

public class BombBooster : Booster
{
    public BombBooster(Vector2Int position) : base(position)
    {
        GraphicId = 2;
    }

    public override bool Break(ABoardElement[,] board)
    {

        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

        DoEffect(board);

        BoardEventManager.NotifyOnMoveDone();
        BoardEventManager.NotifyOnBoardUpdated();

        return true;
    }

    public override void Remove(ABoardElement[,] board, bool isMatched)
    {
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
        dontCombine = true;
        DoEffect(board);
        
    }

    public override void DoEffect(ABoardElement[,] board)
    {
        int prioritaryBoosterType = 0;

        if (!dontCombine)
        {
            for (int i = 0; i < Neighbours.Count; i++)
            {
                if (Neighbours[i] is BombBooster)
                {
                    var pos = Neighbours[i].Position;
                    prioritaryBoosterType = 2;
                    board[pos.x, pos.y].Dissappear(board);
                    continue;
                }

                if (Neighbours[i] is LineBooster)
                {
                    var pos = Neighbours[i].Position;

                    if (prioritaryBoosterType < 2) prioritaryBoosterType = 1;

                    board[pos.x, pos.y].Dissappear(board);
                }


            }

            if (prioritaryBoosterType == 2)
            {
                board[Position.x, Position.y] = new MegaBomb(Position);
                BoardEventManager.NotifyOnBoosterGenerated(board[Position.x, Position.y] as Booster, Position);
                board[Position.x, Position.y].Break(board);
                BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
                board[Position.x, Position.y] = new EmptyTile(Position);
                return;
            }

            if (prioritaryBoosterType == 1)
            {
                board[Position.x, Position.y] = new AxisBomb(Position);
                BoardEventManager.NotifyOnBoosterGenerated(board[Position.x, Position.y] as Booster, Position);
                board[Position.x, Position.y].Break(board);
                BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
                board[Position.x, Position.y] = new EmptyTile(Position);
                return;
            }
        }

        

        int startY = Position.y == 0 ? 0 : Position.y - 1;
        int endY = Position.y == board.GetLength(0) - 1 ? Position.y : Position.y + 1;

        int startX = Position.x == 0 ? 0 : Position.x - 1;
        int endX = Position.x == board.GetLength(0) - 1 ? Position.x : Position.x + 1;

        board[Position.x, Position.y] = new EmptyTile(Position);

        for (int i = startY; i <= endY; i++)
        {
            for (int j = startX; j <= endX; j++)
            {
                var tile = board[j, i];

                if (tile != null)
                {
                    tile.Remove(board, false);
                }
            }
        }

    }

    public override string ToString()
    {
        return "Bomb";
    }
}
