namespace Chess;

public class King : Pieces
{
    private static void KingMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.King + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    public void KingPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Tower tower = new(); Bishop bishop = new();
        if (tower.AcceptedTowerMovements(moveFrom, moveTo) || bishop.AcceptedBishopMovements(moveFrom, moveTo))
        {
            bool res = IsCheck(board, game, moveFrom, moveTo, i, j, color, tower, bishop);
            if (res == false)
            {
                SetMovement(board, game, i, j, color, moveTo);
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }

    public bool IsCheck(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Bishop bishop)
    {
        board.IsCheck = new();
        // CHECK BY TOWER
        IsCheckByTower isCheckByTower = new(); IsCheckByBishop isCheckByBishop = new();
        isCheckByTower.IsCheckByTowerMethod(board, game, moveFrom, moveTo, i, j, color, tower);
        if (board.IsCheck.IsCheck == false)
        {
            return false;
        }


        // is check by...
        Console.WriteLine("Is Check by: " + board.IsCheck.ByPiece.ToUpper());
        game.ResultPlayedBoard = false;
        return true;
    }

    private static void SetMovement(Board board, Game game, int i, int j, PiecesColor color, Board originalMoveTo)
    {
        KingMovement(board, game, originalMoveTo, i, j, color);
        game.ResultPlayedBoard = true;
    }
}
