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
    private static void SetMovement(Board board, Game game, int i, int j, PiecesColor color, Board moveTo)
    {
        string lastPositionLetter = Funcs.ChangeIntToLetter(moveTo.Letter);
        if (color.ToString() == PiecesColor.W.ToString())
        {
            board.IsCheck.LastKingPositionWhite = lastPositionLetter + moveTo.Number;
        }
        else
        {
            board.IsCheck.LastKingPositionBlack = lastPositionLetter + moveTo.Number;
        }
        KingMovement(board, game, moveTo, i, j, color);
        game.ResultPlayedBoard = true;
    }
    public bool IsCheck(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        board.IsCheck.IsCheck = false;
        // CHECK BY PAWN
        IsCheckByPawn isCheckByPawn = new();
        isCheckByPawn.IsCheckByPawnMethod(board, game, moveFrom, moveTo, color);
        
        if (board.IsCheck.IsCheck == false)
        {
            // CHECK BY BISHOP
            IsCheckByBishop isCheckByBishop = new();
            isCheckByBishop.IsCheckByBishopMethod(board, game, moveFrom, moveTo, color);
            if (board.IsCheck.IsCheck == false)
            {
                // CHECK BY TOWER
                IsCheckByTower isCheckByTower = new();
                isCheckByTower.IsCheckByTowerMethod(board, game, moveFrom, moveTo, i, j, color);
            }
            if(board.IsCheck.IsCheck == false)
            {
                // CHECK BY HORSE
                IsCheckByHorse isCheckByHorse = new();
                isCheckByHorse.IsCheckByTowerMethod(board, game, moveFrom, moveTo, i, j, color);
            }
        }
        if (board.IsCheck.IsCheck)
        {
            // is check by...
            ShowConsole.IsCheckBy(board);
            game.ResultPlayedBoard = false;
            board.IsCheck.IsCheck = false;
            return true;
        }
        return false;
    }

    public void KingPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Tower tower = new(); Bishop bishop = new();
        if (tower.AcceptedTowerMovements(moveFrom, moveTo) || bishop.AcceptedBishopMovements(moveFrom, moveTo))
        {
            bool res = IsCheck(board, game, moveFrom, moveTo, i, j, color);
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


}
