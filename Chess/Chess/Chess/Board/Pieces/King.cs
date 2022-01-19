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
            IsCheck(board, game, moveFrom, moveTo, i, j, color, tower, bishop);
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }

    public async void IsCheck(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Bishop bishop)
    {
        Board originalMoveFrom = moveFrom; Board originalMoveTo = moveTo;

        // CHECK BY TOWER
        IsCheckByTower isCheckByTower = new(); IsCheckByBishop isCheckByBishop = new();
        var isCheckByTowerTask = isCheckByTower.IsCheckByTowerMethod(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveFrom, originalMoveTo);
        if (isCheckByTowerTask.GetAwaiter().IsCompleted && board.IsCheck == false)
        {
            SetMovement(board, game, i, j, color, originalMoveTo);
        }

        if(board.IsCheck)
        {
            // check by...
            game.ResultPlayedBoard = false;
        }
    }

    private static void SetMovement(Board board, Game game, int i, int j, PiecesColor color, Board originalMoveTo)
    {
        KingMovement(board, game, originalMoveTo, i, j, color);
        game.ResultPlayedBoard = true;
    }
}
