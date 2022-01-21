namespace Chess;

public class IsCheckByTower
{
    public void IsCheckByTowerMethod(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Tower tower = new();
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();

        // MOVE UP && MOVE DOWN
        if ((originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter > originalMoveTo.Letter) ||
            (originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter < originalMoveTo.Letter))
        {
            // tower right
            ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerRight(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);

            if (board.IsCheck.IsCheck == false)
            {
                // tower left
                ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
                IsCheckTowerLeft(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveFrom);
            }
        }
        // MOVE TO THE LEFT && MOVE TO THE RIGHT
        if ((originalMoveFrom.Letter == originalMoveTo.Letter && originalMoveFrom.Number > originalMoveTo.Number) ||
            (originalMoveFrom.Letter == originalMoveTo.Letter && originalMoveFrom.Number < originalMoveTo.Number))
        {
            // tower up
            ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            IsCheckTowerUp(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);

            if (board.IsCheck.IsCheck == false)
            {
                // tower down
                ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
                IsCheckTowerDown(board, game, moveFromChange, moveToChange, i, j, color, tower, originalMoveTo);
            }
        }
    }

    private static void ResetMoves(Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        moveFromChange.Letter = originalMoveFrom.Letter;
        moveFromChange.Number = originalMoveFrom.Number;
        moveToChange.Letter = originalMoveTo.Letter;
        moveToChange.Number = originalMoveTo.Number;
    }

    private void IsCheckTowerLeft(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom)
    {
        moveToChange.Number = originalMoveFrom.Number - 1;
        tower.LeftDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }
    private void IsCheckTowerRight(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom)
    {
        moveToChange.Number = originalMoveFrom.Number + 1;
        tower.RightDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }
    private void IsCheckTowerUp(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom)
    {
        moveToChange.Letter = originalMoveFrom.Letter - 1;// one up
        tower.UpDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }
    private void IsCheckTowerDown(Board board, Game game, Board moveFrom, Board moveToChange, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom)
    {
        moveToChange.Letter = originalMoveFrom.Letter + 1;// one down
        tower.DownDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
    }

}