using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmojiColor
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Yellow = 3,
    Purple = 4,
    Pink = 5,
    Dstry = 6,
}

public class EmojiBlock : ABoardElement, IDestroyable
{
    public EmojiColor Color { get; set; }

    public EmojiBlock(EmojiColor color, BoardManager boardClient,
        Vector2Int position)
        : base(boardClient, position)
    {
        Color = color;
        GraphicId = (int)color;
        

        gravityAffected = true;
        IsCheckedToDestroy = false;
    }

    public void OnTouched()
    {
        Break();
    }

    public List<EmojiBlock> GetChainedEmojis()
    {
        IsCheckedToDestroy = true;

        List<EmojiBlock> destroyables = new List<EmojiBlock>();

        foreach (ABoardElement tile in Neighbours)
        {
            
            if (tile is not EmojiBlock) continue;

            if (tile.IsCheckedToDestroy) continue;
            

            var emojiTile = tile as EmojiBlock;

            if (emojiTile.Color.Equals(Color)){

                if (!destroyables.Contains(emojiTile))
                {
                    destroyables.Add(emojiTile);
                    destroyables.AddRange(emojiTile.GetChainedEmojis());
                }               
            }

        }

        return destroyables;
    }

    public bool Break()
    {
        var destroyableNeighbours = GetChainedEmojis();

        if (destroyableNeighbours.Count < 2)
        {
            Debug.Log("Not enough neighbours");
            return false;
        }
            

        if (destroyableNeighbours.Count >=4 && destroyableNeighbours.Count < 6)
        {
            BoardModel.Board[Position.x, Position.y] = new LineBooster(BoardModel, Position);
            BoardEventManager.NotifyOnEmojisMatched(1, Color);
        }
        else if(destroyableNeighbours.Count >= 6)
        {
            BoardModel.Board[Position.x, Position.y] = new BombBooster(BoardModel, Position);
            BoardEventManager.NotifyOnEmojisMatched(1, Color);
        }
        else
        {
            destroyableNeighbours.Add(this);
        }
        
        destroyableNeighbours.ForEach(neighbour => neighbour.Remove());

        BoardEventManager.NotifyOnBoardUpdated();
        BoardEventManager.NotifyOnTileDestroyed(Position);

        return true;
    }


    public void Remove()
    {
        BoardModel.Board[Position.x, Position.y] = new EmptyTile(BoardModel, Position);
        BoardEventManager.NotifyOnTileDestroyed(Position);
        BoardEventManager.NotifyOnEmojisMatched(1, Color);
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
