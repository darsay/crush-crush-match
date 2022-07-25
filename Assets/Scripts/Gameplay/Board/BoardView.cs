using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] TilesResource tilesResource;
    public GameObject [,] Board;
    public ABoardElementView[,] boardElementsView;

    public int LengthX = 9;
    public int LengthY = 9;

    Transform parentBoard;


    private void Awake()
    {
        Board = new GameObject[LengthX, LengthY];
        boardElementsView = new ABoardElementView[LengthX, LengthY];

        parentBoard = transform.parent;

        BoardEventManager.OnTileAnim += FallPiece;
        BoardEventManager.OnTileDestroyed += BreakPiece;

    }


    public void DrawBoard(ABoardElement[,] board)
    {
        ResetBoard(board);

        for (int row = LengthX-1; row >=0; row--)
        {
            for (int col = 0; col < LengthY; col++)
            {
                GameObject tile = (GameObject)Instantiate(tilesResource.GetTile(board[col, row]), transform);

                tile.transform.position = new Vector2(col, -row);

                Board[col, row] = tile;
                boardElementsView[col, row] = tile.GetComponent<ABoardElementView>();

            }
        }
    }

    public void ResetBoard(ABoardElement[,] board)
    {
        if (Board == null) return;

        for (int row = LengthX - 1; row >= 0; row--)
        {
            for (int col = 0; col < LengthY; col++)
            {
                Destroy(Board[col, row]);
                boardElementsView[col, row] = null;
            }
        }
    }

    public void FallPiece(Vector2Int pos, Vector2Int target)
    {
        boardElementsView[pos.x, pos.y].FallAnimation(target, AnimData.FallTime);
    }

    public void BreakPiece(Vector2Int pos)
    {
        boardElementsView[pos.x, pos.y].BreakAnimation(AnimData.BreakTime);
    }
}
