namespace Chess;
#nullable disable
public class Tower : Pieces
{
    public bool AcceptedTowerMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter == moveTo.Letter && (moveFrom.Number > moveTo.Number || moveFrom.Number < moveTo.Number)) ||
               (moveFrom.Number == moveTo.Number && (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter));
    }
    private static bool MoveNotAllowed(string[,] board, int moveToLetter, int moveToNumber, PiecesColor color)
    {
        if (board[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // Move To is the Same Color 
            return true;
        }
        return false;
    }
    private static void TowerMovement(Board board, Game game, int moveToLetter, int moveToNumber, int i, int j, PiecesColor color, bool queen)
    {
        Board moveTo = new() { Letter = moveToLetter, Number = moveToNumber };
        Funcs.CapuredList(board, color, moveTo);
        if (queen == false)
        {
            board.Matrix[moveToLetter, moveToNumber] = PiecesForm.Tower + color.ToString();
        }
        if (queen)
        {
            board.Matrix[moveToLetter, moveToNumber] = PiecesForm.Queen + color.ToString();
        }
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    private static void SetMovement(Board board, int moveToLetter, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen)
    {

        if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
        {
            // Move To is not the Same Color
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
            {
                // Move To is the King           
                game = CheckMate(board.Matrix, game, moveToLetter, moveToNumber);
            }
            TowerMovement(board, game, moveToLetter, moveToNumber, i, j, color, queen);
            game.ResultPlayedBoard = true;
        }
        else
        {
            // Move To has piece the same color on the line
            game.ResultPlayedBoard = false;
        }
    }
    private static bool HorizontalMovement(Board board, Board moveTo, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            board.IsCheck = false;
            if (board.Matrix[moveTo.Letter, moveToNumber].Contains(PiecesForm.Tower))
            {
                if (MoveNotAllowed(board.Matrix, moveTo.Letter, moveToNumber, color) == false)
                {
                    // Move To is not the Same Color - check by tower
                    board.IsCheck = true;
                    res = true;
                }
            }
            else
            {
                res = false;
            }
        }
        else
        {
            if (board.Matrix[moveTo.Letter, moveToNumber] != PiecesForm.Empty)
            {
                SetMovement(board, moveTo.Letter, moveToNumber, color, game, i, j, queen);
            }
            else if (moveToNumber == moveTo.Number)
            {
                TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color, queen);
            }
            else
            {
                res = false;
            }
        }
        return res;
    }
    private static bool VerticalMovement(Board board, Board moveTo, int moveToLetter, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            board.IsCheck = false;
            if (board.Matrix[moveToLetter, moveTo.Number] == PiecesForm.Tower)
            {
                if (MoveNotAllowed(board.Matrix, moveToLetter, moveTo.Number, color) == false)
                {
                    // Move To is not the Same Color - check by tower
                    board.IsCheck = true;
                    res = true;
                }
            }
            else
            {
                res = false;
            }
        }
        else
        {
            if (board.Matrix[moveToLetter, moveTo.Number] != PiecesForm.Empty)
            {
                SetMovement(board, moveToLetter, moveTo.Number, color, game, i, j, queen);
            }
            else if (moveToLetter == moveTo.Letter)
            {
                TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color, queen);
            }
            else
            {
                res = false;
            }
        }
        return res;
    }
    public void LeftAndUpDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool left, bool up, bool queen, bool king)
    {
        int x = 0, y = 0;
        if (king && left)
        {
            moveTo.Number = 1;
        }
        if (king && up)
        {
            moveTo.Letter = 1;
        }
        if (left)
        {
            x = moveFrom.Number;
            y = moveTo.Number;
        }
        if (up)
        {
            x = moveFrom.Letter;
            y = moveTo.Letter;
        }
        for (int t = x; t >= y; t--)
        {
            int z = t - 1;
            if (z == 0)
            {
                z += 1;
            }
            bool res = HorizontalMovement(board, moveTo, z, color, game, i, j, queen, king);
            if (res)
            {
                break;
            }
            else
            {
                continue;
            }
        }
    }
    public void RightAndDownDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool right, bool down, bool queen, bool king)
    {
        int x = 0, y = 0;
        if (king && right)
        {
            moveTo.Number = 8;
        }
        if (king && down)
        {
            moveTo.Letter = 8;
        }
        if (right)
        {
            x = moveFrom.Number;
            y = moveTo.Number;
        }
        if (down)
        {
            x = moveFrom.Letter;
            y = moveTo.Letter;
        }
        for (int t = x; t <= y; t++)
        {
            int z = t + 1;
            if (z == 9)
            {
                z -= 1;
            }
            bool res = VerticalMovement(board, moveTo, z, color, game, i, j, queen, king);
            if (res)
            {
                break;
            }
            else
            {
                continue;
            }
        }
    }
    public void TowerPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen)
    {
        if (AcceptedTowerMovements(moveFrom, moveTo))
        {
            if (moveFrom.Letter == moveTo.Letter)
            {
                // HORIZONTAL MOVEMENT
                if (moveFrom.Number < moveTo.Number)
                {
                    // Right Direction
                    RightAndDownDirection(board, game, moveFrom, moveTo, i, j, color, true, false, queen, false);
                }
                else
                {
                    // Left Direction
                    LeftAndUpDirection(board, game, moveFrom, moveTo, i, j, color, true, false, queen, false);
                }
            }
            else if (moveFrom.Number == moveTo.Number)
            {
                // VERTICAL MOVEMENT
                if (moveFrom.Letter < moveTo.Letter)
                {
                    // Down Direction
                    RightAndDownDirection(board, game, moveFrom, moveTo, i, j, color, false, true, queen, false);
                }
                else
                {
                    // Up Direction
                    LeftAndUpDirection(board, game, moveFrom, moveTo, i, j, color, false, true, queen, false);
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}


