namespace Chess;

public class IsCheckByTower
{
    public async Task IsCheckByTowerMethod(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom, Board originalMoveTo)
    {
        if (originalMoveFrom.Number > originalMoveTo.Number && originalMoveFrom.Letter == originalMoveTo.Letter)
        {
            // MOVE TO THE LEFT
            moveTo.Number = originalMoveFrom.Number - 1; // one left
            // tower up
            var towerUp = IsCheckTowerUp(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveTo);
            // tower down
            var towerDown = IsCheckTowerDown(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveTo);
            // tower left
            tower.LeftAndUpDirection(board, game, moveFrom, moveTo, i, j, color, true, false, false, true);
            if (board.IsCheck == false)
            {
                bool towerUpRes = await towerUp;
                if (board.IsCheck == false)
                {
                    bool towerDownRes = await towerDown;
                }
            }
        }
        if (originalMoveFrom.Number < originalMoveTo.Number && originalMoveFrom.Letter == originalMoveTo.Letter)
        {
            // MOVE TO THE RIGHT
            moveTo.Number = originalMoveFrom.Number + 1;// one right
            // tower up
            var towerUp = IsCheckTowerUp(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveTo);
            // tower down
            var towerDown = IsCheckTowerDown(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveTo);
            // tower right
            tower.RightAndDownDirection(board, game, moveFrom, moveTo, i, j, color, true, false, false, true);

            if (board.IsCheck == false)
            {
                bool towerUpRes = await towerUp;
                if (board.IsCheck == false)
                {
                    bool towerDownRes = await towerDown;
                }
            }
        }
        if (originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter > originalMoveTo.Letter)
        {
            // MOVE UP
            moveTo.Letter = originalMoveFrom.Letter - 1; // one up
            // tower right
            var towerRight = IsCheckTowerRight(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveFrom);
            // tower left
            var towerLeft = IsCheckTowerLeft(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveFrom);
            // tower up
            tower.LeftAndUpDirection(board, game, moveFrom, moveTo, i, j, color, false, true, false, true);

            if (board.IsCheck == false)
            {
                bool towerRightRes = await towerRight;
                if (board.IsCheck == false)
                {
                    bool towerLeftRes = await towerLeft;
                }
            }
        }
        if (originalMoveFrom.Number == originalMoveTo.Number && originalMoveFrom.Letter < originalMoveTo.Letter)
        {
            // MOVE DOWN
            moveTo.Letter = originalMoveFrom.Letter + 1; // one down
            // tower right
            var towerRight = IsCheckTowerRight(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveFrom);
            // tower left
            var towerLeft = IsCheckTowerLeft(board, game, moveFrom, moveTo, i, j, color, tower, originalMoveFrom);
            // tower right
            tower.RightAndDownDirection(board, game, moveFrom, moveTo, i, j, color, false, true, false, true);

            if (board.IsCheck == false)
            {
                bool towerRightRes = await towerRight;
                if (board.IsCheck == false)
                {
                    bool towerLeftRes = await towerLeft;
                }
            }
        }
    }
    private Task<bool> IsCheckTowerLeft(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom)
    {
        moveTo.Number = originalMoveFrom.Number - 1;
        tower.LeftAndUpDirection(board, game, moveFrom, moveTo, i, j, color, true, false, false, true);
        return Task.FromResult(board.IsCheck);

    }

    private Task<bool> IsCheckTowerRight(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Board originalMoveFrom)
    {
        moveTo.Number = originalMoveFrom.Number + 1;
        tower.RightAndDownDirection(board, game, moveFrom, moveTo, i, j, color, true, false, false, true);
        return Task.FromResult(board.IsCheck);

    }

    private Task<bool> IsCheckTowerUp(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Board originalMoveTo)
    {
        moveTo.Letter = originalMoveTo.Letter - 1;// one up
        tower.LeftAndUpDirection(board, game, moveFrom, moveTo, i, j, color, false, true, false, true);
        return Task.FromResult(board.IsCheck);

    }

    private Task<bool> IsCheckTowerDown(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Tower tower, Board originalMoveTo)
    {
        moveTo.Letter = originalMoveTo.Letter + 1;// one down
        tower.RightAndDownDirection(board, game, moveFrom, moveTo, i, j, color, false, true, false, true);
        return Task.FromResult(board.IsCheck);
    }

}