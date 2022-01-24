namespace Chess;
#nullable disable
public class Tower : Pieces
{
    public bool AcceptedTowerMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter == moveTo.Letter && (moveFrom.Number > moveTo.Number || moveFrom.Number < moveTo.Number)) ||
               (moveFrom.Number == moveTo.Number && (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter));
    }
    private bool MoveNotAllowed(string[,] board, int moveToLetter, int moveToNumber, PiecesColor color)
    {
        if (board[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // Move To is the Same Color 
            return true;
        }
        return false;
    }
   
    public void IsKingInCheck(Board board, Game game, PiecesColor color, int moveToLetter, int moveToNumber)
    {
        //move from => last position
        //move to => +1 -1
        Board moveLastPositionKing;
        PiecesColor newColor;
        LastPositionKing(board, color, out moveLastPositionKing, out newColor);

        Board moveToChange = new();
        if (moveLastPositionKing.Number == moveToNumber && moveLastPositionKing.Letter < moveToLetter)
        {
            //KING IS UP
            // MOVE KING DOWN
            moveToChange.Letter = moveLastPositionKing.Letter + 1; // one down
            moveToChange.Number = moveLastPositionKing.Number;
            // tower down
            DownDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
        if (moveLastPositionKing.Number == moveToNumber && moveLastPositionKing.Letter > moveToLetter)
        {
            //KING IS DOWN
            // MOVE KING UP
            moveToChange.Letter = moveLastPositionKing.Letter - 1; // one up
            moveToChange.Number = moveLastPositionKing.Number;
            // tower up
            UpDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
        if (moveLastPositionKing.Letter == moveToLetter && moveLastPositionKing.Number > moveToNumber)
        {
            //KING IS RIGHT
            // MOVE KING LEFT
            moveToChange.Number = moveLastPositionKing.Number - 1; // one left
            moveToChange.Letter = moveLastPositionKing.Letter;
            // tower left
            LeftDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
        if (moveLastPositionKing.Letter == moveToLetter && moveLastPositionKing.Number < moveToNumber)
        {
            //KING IS LEFT
            // MOVE KING RIGHT
            moveToChange.Number = moveLastPositionKing.Number + 1;// one right
            moveToChange.Letter = moveLastPositionKing.Letter;
            // tower right
            RightDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
    }

    private void TowerMovement(Board board, Game game, int moveToLetter, int moveToNumber, int i, int j, PiecesColor color, bool queen)
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
        // SEE IF KING IS IN CHECK
        IsKingInCheck(board, game, color, moveToLetter, moveToNumber);
    }
    private void SetMovement(Board board, int moveToLetter, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen)
    {

        if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
        {
            // Move To is not the Same Color
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
            {
                // Move To is the King           
                CheckMate(board.Matrix, game, moveToLetter, moveToNumber);
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


    private bool HorizontalMovement(Board board, Board moveTo, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            res = board.IsCheck.CheckPiece(board, moveTo.Letter, moveToNumber, PiecesForm.Tower, color, res, true, false);
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
    private bool VerticalMovement(Board board, Board moveTo, int moveToLetter, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            res = board.IsCheck.CheckPiece(board, moveToLetter, moveTo.Number, PiecesForm.Tower, color, res, true, false);
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
        for (int t = moveFrom.Letter; t > moveToLetterLimit; t--)
        {
            int moveToLetter = t - 1;
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
        for (int t = moveFrom.Letter; t < moveToLetterLimit; t++)
        {
            int moveToLetter = t + 1;
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
        for (int t = moveFrom.Number; t < moveToNumberLimit; t++)
        {
            int moveToNumber = t + 1;
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
        for (int t = moveFrom.Number; t > moveToNumberLimit; t--)
        {
            int moveToNumber = t - 1;
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
                    RightDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
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


