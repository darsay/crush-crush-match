using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABoardElement
{

    public int GraphicId;
    public BoardManager BoardModel { get; set; }
    public bool IsCheckedToDestroy { get; set; }

    public Vector2Int Position;

    public List<ABoardElement> Neighbours = new List<ABoardElement>();

    public ABoardElement BottomNeighbour { get; set; }

    public bool gravityAffected { get; set; }

    public abstract bool CheckIfMatchable();

    public Vector2[] animationToDo; 


    public ABoardElement(BoardManager boardModel, Vector2Int position)
    {
        BoardModel = boardModel;
        Position = position;
    }

    public void Fall()
    {
        if (!gravityAffected) return;
        if (Position.y == BoardModel.Board.GetLength(1)) return;
        if(BottomNeighbour is null) return;
        if (BottomNeighbour is not EmptyTile) return;

        var previousPos = Position;

        int offset = 0;

        while (BottomNeighbour is EmptyTile && Position.y < BoardModel.Board.GetLength(1)-1)
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

            BoardModel.Board[Position.x, Position.y] = new EmptyTile(BoardModel, Position);
            Position = new Vector2Int(Position.x, Position.y + 1 + offset);
            BoardModel.Board[Position.x, Position.y] = this;
            SetNeighbours();
        }

        BoardEventManager.NotifyOnTileAnim(previousPos, Position);

    }

    

    public void SetNeighbours() { 
        Neighbours.Clear();

        if(Position.x > 0)
        {
            Neighbours.Add(BoardModel.Board[Position.x - 1, Position.y]);
        }

        if (Position.x < BoardModel.Board.GetLength(0) -1)
        {
            Neighbours.Add(BoardModel.Board[Position.x + 1, Position.y]);
        }

        if (Position.y > 0)
        {
            Neighbours.Add(BoardModel.Board[Position.x, Position.y - 1]);
        }

        if (Position.y < BoardModel.Board.GetLength(1) - 1)
        {
            Neighbours.Add(BoardModel.Board[Position.x, Position.y + 1]);
            BottomNeighbour = BoardModel.Board[Position.x, Position.y + 1];
        }
    }
}
