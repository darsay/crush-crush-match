using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EmojiColor
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Yellow = 3,
    Purple = 4,
}

public class EmojiBlock : ABoardElement
{
    public EmojiColor Color { get; set; }

    private ObjectiveType objectiveType;

    public EmojiBlock(EmojiColor color,
        Vector2Int position)
        : base(position)
    {
        Color = color;
        GraphicId = (int)color;

        objectiveType = (ObjectiveType)(int)color;
        

        IsGravityAffected = true;
        IsCheckedToDestroy = false;
    }

    public void OnTouched(ABoardElement[,] board)
    {
        Break(board);
    }

    public List<EmojiBlock> GetChainedEmojis()
    {
        List<EmojiBlock> closed = new List<EmojiBlock>();

        List<EmojiBlock> open = new List<EmojiBlock>();

        open.Add(this);


        while(open.Count > 0)
        {
            EmojiBlock current = open[0];
            open.RemoveAt(0);           
            closed.Add(current);
            
            
            if (current is not EmojiBlock) continue;

            foreach(var n in current.Neighbours)
            {
                var emoji = n as EmojiBlock;

                if (emoji == null) continue;

                if(emoji.Color == this.Color && !open.Contains(emoji)
                    && !closed.Contains(emoji))
                {
                   open.Add(emoji);
                }
            }
        }

        return closed.GetRange(1, closed.Count-1);
    }

    override public bool Break(ABoardElement[,] board)
    {
        var destroyableNeighbours = GetChainedEmojis();

        if (destroyableNeighbours.Count < 2)
        {
            Debug.Log("Not enough neighbours");
            BoardEventManager.NotifyOnTileDestroyed(Position, false, board);
            return false;
        }


        if (destroyableNeighbours.Count >= 4 && destroyableNeighbours.Count < 6)
        {
            BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

            Booster booster = new LineBooster(Position);
            board[Position.x, Position.y] = booster;
            BoardEventManager.NotifyOnObjectiveUpdated(1, objectiveType);
            BoardEventManager.NotifyOnBoosterGenerated(booster, Position);
            BoardEventManager.NotifyOnTileMatched(Position, board);

        }
        else if (destroyableNeighbours.Count >= 6)
        {
            BoardEventManager.NotifyOnTileDestroyed(Position, true, board);

            Booster booster = new BombBooster(Position);
            board[Position.x, Position.y] = booster;
            BoardEventManager.NotifyOnObjectiveUpdated(1, objectiveType);
            BoardEventManager.NotifyOnBoosterGenerated(booster, Position);
            BoardEventManager.NotifyOnTileMatched(Position, board);
        }
        else
        {
            destroyableNeighbours.Add(this);
        }

        destroyableNeighbours.ForEach(neighbour => neighbour.Remove(board, true));

        BoardEventManager.NotifyOnBoardUpdated();
        BoardEventManager.NotifyOnMoveDone();


        return true;
    }


    override public void Remove(ABoardElement[,] board, bool isMatched)
    {
        board[Position.x, Position.y] = new EmptyTile(Position);
        BoardEventManager.NotifyOnTileDestroyed(Position, true, board);
        BoardEventManager.NotifyOnObjectiveUpdated(1, objectiveType);

        if(isMatched)
            BoardEventManager.NotifyOnTileMatched(Position, board);
    }

    
    public override string ToString()
    {
        return Color.ToString();
    }

    public override bool CheckIfMatchable()
    {
        return GetChainedEmojis().Count >= 2;
    }
}
