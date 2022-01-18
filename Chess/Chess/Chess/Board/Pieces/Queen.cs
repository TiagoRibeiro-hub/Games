namespace Chess;

public class Queen : Pieces
{
    private static void QuennMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Queen + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    public void QueenPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Tower tower = new(); Bishop bishop = new Bishop();
        if (tower.AcceptedTowerMovements(moveFrom, moveTo) || bishop.AcceptedBishopMovements(moveFrom, moveTo))
        {
            if (board.Matrix[moveTo.Letter, moveTo.Number].Contains(color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                game = CheckMate(board.Matrix, game, moveTo.Letter, moveTo.Number);
                QuennMovement(board, game, moveTo, i, j, color);
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}
