using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BoardController
{
    BoardModel _boardModel;

    Level _level;

    public BoardController(int x, int y)
    {
        _boardModel = new BoardModel(x, y);
    }


    public void InitBoard(Level level)
    {
        _level = level;

        if (level.IsBoardPreDefined)
        {
            GenerateDefinedBoard();
        }
        else
        {
            GenerateRandomBoard();
        }



        SetAllNeighbours();

        CheckIfValid();

        PrintBoard();
    }

    private void GenerateDefinedBoard()
    {
        int counter = 0;
        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {

            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                _boardModel.Board[j, i] = GetTileByType(_level.Board[counter++], j, i);
                BoardEventManager.NotifyOnTileCreated(new Vector2Int(j, i), _boardModel.Board[j, i]);
            }

        }
    }

    private void GenerateRandomBoard()
    {
        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                _boardModel.Board[j, i] = GenerateRandomEmoji(j, i);
                BoardEventManager.NotifyOnTileCreated(new Vector2Int(j, i), _boardModel.Board[j, i]);
            }
        }
    }

    void PrintBoard()
    {
        string s = "";
        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                s += (_boardModel.Board[j, i]).ToString() + "\t";
            }

            s += "\n";
        }

        Debug.Log(s);
    }

    public void UpdateBoardAfterBreak()
    {
        SetAllNeighbours();

        PrintBoard();

        for (int i = _boardModel.Board.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                _boardModel.Board[j, i].Fall(_boardModel.Board);
                SetAllNeighbours();
            }
        }

        GenerateEmojiRow();

        CheckIfValid();

        //BoardEventManager.NotifyOnInputLocked(false);

        PrintBoard();
    }

    private void GenerateEmojiRow()
    {
        bool isTopRowEmpty = true;
        while (isTopRowEmpty)
        {
            isTopRowEmpty = false;
            for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
            {
                if (_boardModel.Board[i, 0] is EmptyTile)
                {
                    isTopRowEmpty = true;

                    var newEmoji = GenerateRandomEmoji(i, 0);
                    _boardModel.Board[i, 0] = newEmoji;

                    BoardEventManager.NotifyOnTileCreated(new Vector2Int(i, 0), newEmoji);

                    _boardModel.Board[i, 0].SetNeighbours(_boardModel.Board);
                    _boardModel.Board[i, 0].Fall(_boardModel.Board);

                    SetNeighboursInColumnAndSurrounds(i);
                }
            }
        }

    }

    void SetAllNeighbours()
    {
        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                _boardModel.Board[j, i].SetNeighbours(_boardModel.Board);
            }
        }
    }

    void SetNeighboursInColumnAndSurrounds(int columnId)
    {
        int start = columnId == 0 ? columnId : columnId - 1;
        int end = columnId == _boardModel.Board.GetLength(0) - 1 ? columnId : columnId + 1;

        for (int i = start; i <= end; i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                _boardModel.Board[i, j].SetNeighbours(_boardModel.Board);
            }
        }
    }

    public ABoardElement GenerateRandomEmoji(int x, int y)
    {
        var emojisNum = _level.EmojiColorsToSpawn.Length;
        var r = Random.Range(0, emojisNum);
        var emojiOut = new EmojiBlock(_level.EmojiColorsToSpawn[r], new Vector2Int(x, y));

        return emojiOut;
    }

    public ABoardElement GetTileByType(TilesTypes type, int x, int y)
    {
        int typeId = (int)type;

        if (type == TilesTypes.Rand)
        {
            return GenerateRandomEmoji(x, y);
        }

        if (typeId <= 5)
        {
            return new EmojiBlock((EmojiColor)typeId, new Vector2Int(x, y));
        }

        switch (type)
        {
            case TilesTypes.Bomb:
                return new BombBooster(new Vector2Int(x, y));

            case TilesTypes.Vertical:
                return new LineBooster(new Vector2Int(x, y), true);

            case TilesTypes.Horizontal:
                return new LineBooster(new Vector2Int(x, y), false);

            case TilesTypes.Heart:
                return new BasicHeart(new Vector2Int(x, y));

            case TilesTypes.BaloonHeart:
                return new BaloonHeart(new Vector2Int(x, y));

            case TilesTypes.RedHeart:
                return new RedHeart(new Vector2Int(x, y));

            case TilesTypes.YellowHeart:
                return new YellowHeart(new Vector2Int(x, y));

            case TilesTypes.GreenHeart:
                return new GreenHeart(new Vector2Int(x, y));

            case TilesTypes.PurpleHeart:
                return new PurpleHeart(new Vector2Int(x, y));

            case TilesTypes.BlueHeart:
                return new BlueHeart(new Vector2Int(x, y));

            case TilesTypes.Empty:
                return new EmptyTile(new Vector2Int(x, y));
        }

        return null;
    }

    public void BreakCell(int x, int y)
    {
        var boardTouched = _boardModel.Board[x, y];

        if (boardTouched != null)
        {
            boardTouched.Break(_boardModel.Board);
        }

        ResetChecked();
    }

    void CheckIfValid()
    {
        ResetChecked();

        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                if (_boardModel.Board[j, i].CheckIfMatchable()) return;
            }
        }

        ShuffleBoard();

        PrintBoard();

    }


    void ShuffleBoard()
    {
        List<ABoardElement> elements = new List<ABoardElement>();
        List<Vector2Int> positionsToShuffle = new List<Vector2Int>();

        List<ABoardElement> stillTiles = new List<ABoardElement>();

        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                if (_boardModel.Board[j, i] is EmojiBlock)
                {
                    positionsToShuffle.Add(_boardModel.Board[j, i].Position);
                    elements.Add(_boardModel.Board[j, i]);
                }
                else
                {
                    stillTiles.Add(_boardModel.Board[j, i]);
                }

            }
        }

        foreach (var pos in positionsToShuffle)
        {
            var r = Random.Range(0, elements.Count);

            BoardEventManager.NotifyOnTileSwap(elements[r].Position, pos);

            elements.Find(element => element.Position == pos).Position = elements[r].Position;

            _boardModel.Board[pos.x, pos.y] = elements[r];
            _boardModel.Board[pos.x, pos.y].Position = pos;

            elements.RemoveAt(r);
        }

        foreach (var tile in stillTiles)
        {
            var tilePos = tile.Position;

            _boardModel.Board[tilePos.x, tilePos.y] = tile;
        }

        SetAllNeighbours();

        PrintBoard();

        CheckIfValid();
    }

    void ResetChecked()
    {
        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _boardModel.Board.GetLength(0); j++)
            {
                _boardModel.Board[j, i].IsCheckedToDestroy = false;
            }
        }
    }

    public void ProcessActiveBooster(ActiveBooster booster, int x, int y)
    {
        var boardTouched = _boardModel.Board[x, y];

        switch (booster)
        {
            case ActiveBooster.CellBreaker:
                boardTouched.Remove(_boardModel.Board, true);
                BoardEventManager.NotifyOnBoardUpdated();
                break;

            case ActiveBooster.LineBreaker:
                RemoveLine(y);
                break;

            case ActiveBooster.ColumnBreaker:
                RemoveColumn(x);
                break;

            case ActiveBooster.Shuffle:
                ShuffleBoard();
                break;
        }

        BoardEventManager.NotifyOnActiveBoosterUsed(booster);


    }

    public void RemoveLine(int y)
    {
        for (int i = 0; i < _boardModel.Board.GetLength(0); i++)
        {
            var cell = _boardModel.Board[i, y];

            if (cell != null)
            {
                cell.Remove(_boardModel.Board, false);
            }
        }

        BoardEventManager.NotifyOnBoardUpdated();
    }

    public void RemoveColumn(int x)
    {
        for (int i = 0; i < _boardModel.Board.GetLength(1); i++)
        {
            var cell = _boardModel.Board[x, i];

            if (cell != null)
            {
                cell.Remove(_boardModel.Board, false);
            }
        }

        BoardEventManager.NotifyOnBoardUpdated();
    }

    public void DissapearBooster(Vector2Int pos)
    {
        _boardModel.Board[pos.x, pos.y].Dissappear(_boardModel.Board);
    }
}
