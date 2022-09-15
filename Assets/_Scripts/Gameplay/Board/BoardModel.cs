public class BoardModel
{
    public ABoardElement[,] Board;

    public BoardModel(int lengthX, int lengthY)
    {
        Board = new ABoardElement[lengthX, lengthY];
    }

    public BoardModel(ABoardElement[,] board)
    {
        Board = board;
    }
}
