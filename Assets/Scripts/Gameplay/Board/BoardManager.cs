using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public ABoardElement[,] Board;

    public int LengthX { get => Board.GetLength(0);}
    public int LengthY { get => Board.GetLength(1);}

    [SerializeField] BoardView boardView;

    GameplayInputController _inputController;

    bool lockInput = false;
    bool gameFinished = false;

    private void Awake()
    {
        Board = new ABoardElement[9, 9];

        _inputController = new GameplayInputController();

        _inputController.Touch.TouchPress.canceled += ctx => OnBoardTouched(ctx);
    }

    private void OnEnable()
    {
        BoardEventManager.OnBoardUpdated += OnUpdateBoardAfterBreak;
        BoardEventManager.OnDefeat += OnGameFinished;
        BoardEventManager.OnWin += OnGameFinished;

        _inputController.Enable();
    }

    private void OnDisable()
    {
        BoardEventManager.OnBoardUpdated -= OnUpdateBoardAfterBreak;
        BoardEventManager.OnDefeat -= OnGameFinished;
        BoardEventManager.OnWin -= OnGameFinished;

        _inputController.Disable();
    }

    private void Start()
    {
        InitBoard();
    }

    void InitBoard()
    {
        for(int i = 0; i < LengthY; i++)
        {
            for(int j = 0; j < LengthX; j++)
            {
                Board[j, i] = GenerateRandomEmoji(j, i);
            }
        }

        SetAllNeighbours();

        CheckIfValid();

        boardView.DrawBoard(Board);

        PrintBoard();
    }

    void PrintBoard()
    {
        string s = "";
        for (int i = 0; i < LengthX; i++)
        {
            for (int j = 0; j < LengthY; j++)
            {
                s += (Board[j, i]).ToString() + "\t";
            }

            s += "\n";
        }

        print(s);  
    }

    void OnUpdateBoardAfterBreak()
    {
        StartCoroutine(UpdateBoardAfterBreak());
    }

    IEnumerator UpdateBoardAfterBreak()
    {
        SetAllNeighbours();

        PrintBoard();

        for (int i = LengthY-1; i >=0; i--)
        {
            for (int j = 0; j < LengthX; j++)
            {
                Board[j, i].Fall();
                SetAllNeighbours();
            }
        }

        yield return new WaitForSeconds(AnimData.FallTime + 0.1f);
              

        GenerateEmojiRow();

        CheckIfValid();

        boardView.DrawBoard(Board);
        lockInput = false;

        PrintBoard();
    }

    private void GenerateEmojiRow()
    {
        bool isTopRowEmpty = true;
        while (isTopRowEmpty)
        {
            isTopRowEmpty = false;
            for (int i = 0; i < LengthX; i++)
            {
                if (Board[i, 0] is EmptyTile)
                {
                    isTopRowEmpty = true;
                    Board[i, 0] = GenerateRandomEmoji(i, 0);
                    Board[i, 0].SetNeighbours();
                    Board[i, 0].Fall();
                    SetNeighboursInColumnAndSurrounds(i);
                }
            }
        }
        
    }

    void SetAllNeighbours()
    {
        for (int i = 0; i < LengthY; i++)
        {
            for (int j = 0; j < LengthX; j++)
            {
                Board[j, i].SetNeighbours();
            }
        }
    }

    void SetNeighboursInColumnAndSurrounds(int columnId)
    {
        int start = columnId == 0 ? columnId : columnId-1;
        int end = columnId == LengthX-1 ? columnId : columnId+1;

        for (int i = start; i <=end; i++)
        {
            for (int j = 0; j < LengthX; j++)
            {
                Board[i, j].SetNeighbours();
            }
        }
    }

    public ABoardElement GenerateRandomEmoji(int x, int y)
    {
        var emojis = Enum.GetValues(typeof(EmojiColor)).Length;
        var r = Random.Range(0,5);
        var emojiOut = new EmojiBlock((EmojiColor)r, this, new Vector2Int(x, y));
        return new EmojiBlock((EmojiColor)r, this, new Vector2Int(x, y));
    }

    IEnumerator BreakCell(int x, int y)
    {
        var boardTouched = Board[x, y] as IDestroyable;

        if (boardTouched != null)
        {
            lockInput = boardTouched.Break();
        }

        yield return new WaitForSeconds(AnimData.BreakTime + 0.5f);

        ResetChecked();
    }

    void CheckIfValid()
    {
        ResetChecked();

        for (int i = 0; i < LengthY; i++)
        {
            for (int j = 0; j < LengthX; j++)
            {
                if(Board[j, i].CheckIfMatchable()) return;
            }
        }

        ShuffleBoard();      

        PrintBoard();

        CheckIfValid();
    }


    void ShuffleBoard()
    {
        List<ABoardElement> elements = new List<ABoardElement>();

        for (int i = 0; i < LengthY; i++)
        {
            for (int j = 0; j < LengthX; j++)
            {
                elements.Add(Board[j, i]);
            }
        }

        Board = new ABoardElement[9, 9];

        for (int i = 0; i < LengthY; i++)
        {
            for (int j = 0; j < LengthX; j++)
            {
                var r = Random.Range(0, elements.Count - 1);
                Board[j, i] = elements[r];
                Board[j, i].Position = new Vector2Int(j, i);
                elements.RemoveAt(r);
            }
        }

        SetAllNeighbours();
    }

    void ResetChecked()
    {
        for (int i = 0; i < LengthY; i++)
        {
            for (int j = 0; j < LengthX; j++)
            {
                Board[j, i].IsCheckedToDestroy = false;
            }
        }
    }

    void OnGameFinished()
    {
        gameFinished = true;
    }

    void OnBoardTouched(InputAction.CallbackContext ctx)
    {
        if (lockInput || gameFinished) return;

        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(_inputController.Touch.TouchPosition.ReadValue<Vector2>());
        touchWorldPosition.z = 0f;

        int x = (int)Mathf.Round(touchWorldPosition.x);
        int y = (int)Mathf.Round(touchWorldPosition.y);

        if (x < 0 || x > 8) return;

        if (y > 0f || y < -8) return;

        StartCoroutine(BreakCell(Math.Abs(x), Math.Abs(y)));

    }
}
