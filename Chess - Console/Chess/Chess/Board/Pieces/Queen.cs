namespace Chess;

public class Queen : Pieces
{
    public void QueenPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Rook tower = new(); Bishop bishop = new();
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
