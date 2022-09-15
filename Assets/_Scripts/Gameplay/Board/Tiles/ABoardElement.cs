using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABoardElement
{
    public int GraphicId;
    public bool IsCheckedToDestroy { get; set; }

    public Vector2Int Position;

    public List<ABoardElement> Neighbours = new List<ABoardElement>();

    public ABoardElement BottomNeighbour { get; set; }

    public bool IsGravityAffected { get; set; }

    public abstract bool CheckIfMatchable();

    public Vector2[] animationToDo; 


    public ABoardElement(Vector2Int position)
    {
        Position = position;
    }

    public void Fall(ABoardElement[,] board)
    {
        if (!IsGravityAffected) return;
        if (Position.y == board.GetLength(1)) return;
        if(BottomNeighbour is null) return;
        if (BottomNeighbour is not EmptyTile) return;

        var previousPos = Position;

        int offset = 0;

        while (BottomNeighbour is EmptyTile && Position.y < board.GetLength(1)-1)
        {
            if(BottomNeighbour is VoidTile)
            {
                offset++;
                continue;
            }
            else
            {
                offset = 0;
            }

            board[Position.x, Position.y] = new EmptyTile(Position);
            Position = new Vector2Int(Position.x, Position.y + 1 + offset);
            board[Position.x, Position.y] = this;
            SetNeighbours(board);
        }

        BoardEventManager.NotifyOnTileMove(previousPos, Position);

    }

    

    public void SetNeighbours(ABoardElement[,] board) { 
        Neighbours.Clear();

        if(Position.x > 0)
        {
            Neighbours.Add(board[Position.x - 1, Position.y]);
        }

        if (Position.x < board.GetLength(0) -1)
        {
            Neighbours.Add(board[Position.x + 1, Position.y]);
        }

        if (Position.y > 0)
        {
            Neighbours.Add(board[Position.x, Position.y - 1]);
        }

        if (Position.y < board.GetLength(1) - 1)
        {
            Neighbours.Add(board[Position.x, Position.y + 1]);
            BottomNeighbour = board[Position.x, Position.y + 1];
        }
    }

    public void Dissappear(ABoardElement[,] board)
    {
        board[Position.x, Position.y] = new EmptyTile(Position);
        BoardEventManager.NotifyOnTileDissapearWithNoEffect(Position);

    }

    public abstract void Remove(ABoardElement[,] board, bool isMatched);

    public abstract bool Break(ABoardElement[,] board);
}
