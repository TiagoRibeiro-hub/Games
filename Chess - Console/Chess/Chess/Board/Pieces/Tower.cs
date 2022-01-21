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

    private static bool CheckPiece(Board board, int moveToLetter, int moveToNumber, PiecesColor color, bool stopSearch)
    {
        board.IsCheck.IsCheck = false;
        string newColor = Funcs.NewColor(color);
        if(board.Matrix[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // same color is protected
            stopSearch = true;
        }
        else if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Tower + newColor) || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Queen + newColor))
        {
            board.IsCheck.IsCheck = true;
            board.IsCheck.ByPiece = Funcs.IsCheckBy(board, moveToLetter, moveToNumber, color, PiecesForm.Tower);
            stopSearch = true;
            //if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
            //{
            //    // Move To is not the Same Color - check by tower
                
            //}
        }
        else
        {
            stopSearch = false;
        }
        return stopSearch;
    }


    private static bool HorizontalMovement(Board board, Board moveTo, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            res = CheckPiece(board, moveTo.Letter, moveToNumber, color, res);
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
            res = CheckPiece(board, moveToLetter, moveTo.Number, color, res);
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

    public void UpDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToLetterLimit;
        if (king)
        {
            moveToLetterLimit = 1;
        }
        else
        {
            moveToLetterLimit = moveTo.Letter;
        }
        for (int t = moveFrom.Letter; t >= moveToLetterLimit; t--)
        {
            int moveToLetter = t - 1;
            if (moveToLetter == 0)
            {
                moveToLetter += 1;
            }
            bool res = VerticalMovement(board, moveTo, moveToLetter, color, game, i, j, queen, king);
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
    public void DownDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToLetterLimit;
        if (king)
        {
            moveToLetterLimit = 8;
        }
        else
        {
            moveToLetterLimit = moveTo.Letter;
        }
        for (int t = moveFrom.Letter; t <= moveToLetterLimit; t++)
        {
            int moveToLetter = t + 1;
            if (moveToLetter == 9)
            {
                moveToLetter -= 1;
            }
            bool res = VerticalMovement(board, moveTo, moveToLetter, color, game, i, j, queen, king);

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

    public void RightDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToNumberLimit;
        if (king)
        {
            moveToNumberLimit = 8;
        }
        else
        {
            moveToNumberLimit = moveTo.Number;
        }
        for (int t = moveFrom.Number; t <= moveToNumberLimit; t++)
        {
            int moveToNumber = t + 1;
            if (moveToNumber == 9)
            {
                moveToNumber -= 1;
            }

            bool res = HorizontalMovement(board, moveTo, moveToNumber, color, game, i, j, queen, king);
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
    public void LeftDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToNumberLimit;
        if (king)
        {
            moveToNumberLimit = 1;
        }
        else
        {
            moveToNumberLimit = moveTo.Number;
        }
        for (int t = moveFrom.Number; t >= moveToNumberLimit; t--)
        {
            int moveToNumber = t - 1;
            if (moveToNumber == 0)
            {
                moveToNumber += 1;
            }
            bool res = HorizontalMovement(board, moveTo, moveToNumber, color, game, i, j, queen, king);
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
                    RightDirection(board, game, moveFrom, moveTo, i, j, color,  queen, false);
                }
                else
                {
                    // Left Direction
                    LeftDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
            }
            else if (moveFrom.Number == moveTo.Number)
            {
                // VERTICAL MOVEMENT
                if (moveFrom.Letter < moveTo.Letter)
                {
                    // Down Direction
                    DownDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
                else
                {
                    // Up Direction
                    UpDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}


