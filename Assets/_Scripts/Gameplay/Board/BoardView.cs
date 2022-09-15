using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum ActiveBooster
{
    None,
    CellBreaker,
    LineBreaker,
    ColumnBreaker,
    Shuffle,
    
}

public class BoardView : MonoBehaviour
{
    [SerializeField] TilesResource tilesResource;
    [SerializeField] LevelLoader levelLoader;
    public List<ABoardElementView> Board;

    public int LengthX = 9;
    public int LengthY = 9;


    GameplayInputController _inputController;

    BoardController _boardController;

    public ActiveBooster CurrentActiveBooster { get; set; }

    bool lockInput = false;

    private void Awake()
    {
        Board = new List<ABoardElementView>();

        _inputController = new GameplayInputController();
        _inputController.Touch.TouchPress.canceled += ctx => OnBoardTouched(ctx);

        _boardController = new BoardController(LengthX, LengthY);

    }

    private void OnEnable()
    {
        BoardEventManager.OnTileMove += MovePiece;
        BoardEventManager.OnTileSwap += OnTileSwap;
        BoardEventManager.OnTileDestroyed += BreakPiece;
        BoardEventManager.OnTileCreated += OnTileCreated;
        BoardEventManager.OnBoardUpdated += _boardController.UpdateBoardAfterBreak;
        BoardEventManager.OnBoosterGenerated += OnBoosterGenerated;
        BoardEventManager.OnActiveBoosterSet += SetCurrentActiveBooster;
        BoardEventManager.OnInputLocked += OnInputLocked;
        BoardEventManager.OnTileDissapearWithNoEffect += DissapearTile;

        _inputController.Enable();
    }

    

    private void OnDisable()
    {
        BoardEventManager.OnTileMove -= MovePiece;
        BoardEventManager.OnTileSwap -= OnTileSwap;
        BoardEventManager.OnTileDestroyed -= BreakPiece;
        BoardEventManager.OnTileCreated -= OnTileCreated;
        BoardEventManager.OnBoardUpdated -= _boardController.UpdateBoardAfterBreak;
        BoardEventManager.OnBoosterGenerated -= OnBoosterGenerated;
        BoardEventManager.OnActiveBoosterSet -= SetCurrentActiveBooster;
        BoardEventManager.OnInputLocked -= OnInputLocked;
        BoardEventManager.OnTileDissapearWithNoEffect -= DissapearTile;

        _inputController.Disable();
    }

    private void Start()
    {
        _boardController.InitBoard(levelLoader.Level);
    }



    public ABoardElementView GetBoardElementViewAt(Vector2Int position)
    {
        return Board.Find(element => element.Position == position);
    }

    private void MovePiece(Vector2Int pos, Vector2Int target)
    {
        var element = GetBoardElementViewAt(pos);

        element.Position = target;

        element.MoveAnimation(target, AnimData.FallTime);
    }

    private void OnTileSwap(Vector2Int pos, Vector2Int target)
    {
        var elementToMove = GetBoardElementViewAt(pos);
        var elementToSwap = GetBoardElementViewAt(target);

        elementToMove.Position = target;
        elementToSwap.Position = pos;

        elementToMove.MoveAnimation(target, AnimData.FallTime);
    }

    public void BreakPiece(Vector2Int pos, bool broken, ABoardElement[,] board)
    {
        var element = GetBoardElementViewAt(pos);

        if (element == null)
        {
            print("Tile to break is null");
            return;
        }
        

        if (!broken)
        {
            element.TouchAnimation(AnimData.BreakTime);
            return;
        }

        Board.Remove(element);

        element.BreakAnimation(AnimData.BreakTime);
    }

    public void OnTileCreated(Vector2Int pos, ABoardElement tile)
    {
        Vector3Int initialPos = new Vector3Int(pos.x, -pos.y + LengthY, 0);

        if (tile is EmptyTile) return;

        ABoardElementView newTile = Instantiate(tilesResource.GetTile(tile), initialPos, Quaternion.identity, transform)
            .GetComponent<ABoardElementView>();

        newTile.Position = pos;

        Board.Add(newTile);

        newTile.MoveAnimation(pos, AnimData.FallTime);
    }

    public void OnBoosterGenerated(Booster booster, Vector2Int pos)
    {
        Vector3Int worldPos = new Vector3Int(pos.x, -pos.y, 0);

        ABoardElementView newBooster = Instantiate(tilesResource.GetTile(booster), worldPos, Quaternion.identity, transform)
            .GetComponent<ABoardElementView>();

        Board.Add(newBooster);

        newBooster.Position = pos;
    }

    void OnBoardTouched(InputAction.CallbackContext ctx)
    {
        if (lockInput) return;

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(_inputController.Touch.TouchPosition.ReadValue<Vector2>());
        touchWorldPosition.z = 0f;

        int x = (int)Mathf.Round(touchWorldPosition.x);
        int y = (int)Mathf.Round(touchWorldPosition.y);

        if (x < 0 || x > 8 || y > 0f || y < -8)
        {
            if(CurrentActiveBooster != ActiveBooster.None)
            {
                BoardEventManager.NotifyOnActiveBoosterSet(ActiveBooster.None);
            }

            return;
        }

        if(CurrentActiveBooster!=ActiveBooster.None)
        {
            _boardController.ProcessActiveBooster(CurrentActiveBooster, Math.Abs(x), Math.Abs(y));
            BoardEventManager.NotifyOnActiveBoosterSet(ActiveBooster.None);
        }
        else
        {
            _boardController.BreakCell(Math.Abs(x), Math.Abs(y));
        }        

    }

    void OnInputLocked(bool isLocked) => lockInput = isLocked;

    public void SetCurrentActiveBooster(ActiveBooster booster)
    {
        CurrentActiveBooster = booster;
    }

    void DissapearTile(Vector2Int pos)
    {
        var element = GetBoardElementViewAt(pos);

        if (element == null) return;

        Board.Remove(element);

        element.DestroyTile();
    }
}
