namespace Chess;
#nullable disable
public class IsCheckByRook
{
    private readonly Check check = new();
    private readonly Rook tower = new();

    public void IsCheckByTowerMethod(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();

        // MOVE UP && MOVE DOWN
        if ((originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter > originalMoveTo.Letter) ||
            (originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter < originalMoveTo.Letter))
        {
            // tower right
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerRight(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);

            // tower left
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerLeft(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);
        }
        // MOVE TO THE LEFT && MOVE TO THE RIGHT
        if ((originalMoveFrom.Letter == originalMoveTo.Letter && originalMoveFrom.Number > originalMoveTo.Number) ||
            (originalMoveFrom.Letter == originalMoveTo.Letter && originalMoveFrom.Number < originalMoveTo.Number))
        {
            // tower up
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerUp(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);

            // tower down
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerDown(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);
        }
        // MOVE DIAGONAL DOWN LEFT OR RIGHT
        if (originalMoveFrom.Letter == originalMoveTo.Letter + 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {
            // tower up
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerUp(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);

            // tower down
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerDown(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);

            // tower right
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerRight(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);

            // tower left
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerLeft(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);

        }
        // MOVE DIAGONAL UP LEFT OR RIGHT
        if (originalMoveFrom.Letter == originalMoveTo.Letter - 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {
            // tower up
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerUp(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);

            // tower down
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerDown(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);

            // tower right
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerRight(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);

            // tower left
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerLeft(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);
        }
    }

    private void IsCheckTowerLeft(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Rook tower, Board originalMoveFrom)
    {
        moveToChange.Number = originalMoveFrom.Number - 1;
        board.IsCheck.SideToCheck = SideToCheckOpt.Left;
        tower.LeftDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }
    private void IsCheckTowerRight(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Rook tower, Board originalMoveFrom)
    {
        moveToChange.Number = originalMoveFrom.Number + 1;
        board.IsCheck.SideToCheck = SideToCheckOpt.Right;
        tower.RightDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }
    private void IsCheckTowerUp(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Rook tower, Board originalMoveFrom)
    {
        moveToChange.Letter = originalMoveFrom.Letter - 1;// one up
        board.IsCheck.SideToCheck = SideToCheckOpt.Up;
        tower.UpDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }
    private void IsCheckTowerDown(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Rook tower, Board originalMoveFrom)
    {
        moveToChange.Letter = originalMoveFrom.Letter + 1;// one down
        board.IsCheck.SideToCheck = SideToCheckOpt.Down;
        tower.DownDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }

}