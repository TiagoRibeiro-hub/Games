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
        Tower tower = new(); Bishop bishop = new();
        if (tower.AcceptedTowerMovements(moveFrom, moveTo))
        {
            tower.TowerPossibleMoves(board, game, moveFrom, moveTo, i, j, color, true);
        }
        else if (bishop.AcceptedBishopMovements(moveFrom, moveTo))
        {
            bishop.BishopPossibleMoves(board, game, moveFrom, moveTo, i, j, color, true);
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}
