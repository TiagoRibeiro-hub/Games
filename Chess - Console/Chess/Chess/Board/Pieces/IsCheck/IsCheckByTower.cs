namespace Chess;

public class IsCheckByTower
{
    public async void IsCheckByTowerMethod(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower)
    {
        Board originalMoveFrom = moveFrom; Board originalMoveTo = moveTo;
        Board moveToChange = new();
        if (originalMoveFrom.Number > originalMoveTo.Number && originalMoveFrom.Letter == originalMoveTo.Letter)
        {
            // MOVE TO THE LEFT
            moveToChange.Number = originalMoveFrom.Number - 1; // one left
            moveToChange.Letter = originalMoveFrom.Letter;
            // tower left
            tower.LeftDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);
            if (board.IsCheck.IsCheck == false)
            {
                // tower up
                moveToChange.Number = originalMoveFrom.Number - 1; // one left
                IsCheckTowerUp(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveTo);
                if (board.IsCheck.IsCheck == false)
                {
                    // tower down
                    moveToChange.Number = originalMoveFrom.Number - 1; // one left
                    IsCheckTowerDown(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveTo);
                }
            }
        }
        else if (originalMoveFrom.Number < originalMoveTo.Number && originalMoveFrom.Letter == originalMoveTo.Letter)
        {
            // MOVE TO THE RIGHT
            moveToChange.Number = originalMoveFrom.Number + 1;// one right
            moveToChange.Letter = originalMoveFrom.Letter;
            // tower right
            tower.RightDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);

            if (board.IsCheck.IsCheck == false)
            {
                // tower up
                moveToChange.Number = originalMoveFrom.Number + 1;// one right
                IsCheckTowerUp(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveTo);
                if (board.IsCheck.IsCheck == false)
                {
                    // tower down
                    moveToChange.Number = originalMoveFrom.Number + 1;// one right
                    IsCheckTowerDown(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveTo);
                }
            }
        }
        else if (originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter > originalMoveTo.Letter)
        {
            // MOVE UP
            moveToChange.Letter = originalMoveFrom.Letter - 1; // one up
            moveToChange.Number = originalMoveFrom.Number;

            // tower up
            tower.UpDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);

            if (board.IsCheck.IsCheck == false)
            {
                // tower right
                moveToChange.Letter = originalMoveFrom.Letter + 1; // one down
                IsCheckTowerRight(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveFrom);
                if (board.IsCheck.IsCheck == false)
                {
                    // tower left
                    moveToChange.Letter = originalMoveFrom.Letter + 1; // one down
                    IsCheckTowerLeft(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveFrom);
                }
            }
        }
        else if (originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter < originalMoveTo.Letter)
        {
            // MOVE DOWN
            moveToChange.Letter = originalMoveFrom.Letter + 1; // one down
            moveToChange.Number = originalMoveFrom.Number;

            // tower down
            tower.DownDirection(board, game, moveFrom, moveToChange, i, j, color, false, true);

            if (board.IsCheck.IsCheck == false)
            {
                // tower right
                moveToChange.Letter = originalMoveFrom.Letter + 1; // one down
                IsCheckTowerRight(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveFrom);
                if (board.IsCheck.IsCheck == false)
                {
                    // tower left
                    moveToChange.Letter = originalMoveFrom.Letter + 1; // one down
                    IsCheckTowerLeft(board, game, moveFrom, moveToChange, i, j, color, tower, originalMoveFrom);
                }
            }
        }
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