namespace Chess;

public class Bishop : Pieces
{
    public bool AcceptedBishopMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter) &&
                    (moveFrom.Number != moveTo.Number &&
                    (moveFrom.Letter + moveFrom.Number == moveTo.Letter + moveTo.Number || moveFrom.Letter - moveFrom.Number == moveTo.Letter - moveTo.Number));
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
    private static void BishopMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        Funcs.CapuredList(board, color, moveTo);
        if (queen == false)
        {
            board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Bishop + color.ToString();
        }
        if (queen)
        {
            board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Queen + color.ToString();
        }
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    private static void SetMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color, int moveToLetter, int moveToNumber, bool queen, bool king)
    {
        if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
        {
            // Move To is not the Same Color
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
            {
                // Move To is the King           
                game = CheckMate(board.Matrix, game, moveToLetter, moveToNumber);
            }
            BishopMovement(board, game, moveTo, i, j, color, queen, king);
            game.ResultPlayedBoard = true;
        }
        else
        {
            // Move To is the Same Color 
            game.ResultPlayedBoard = false;
        }

    }
    private static bool DiagonalSideMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color, int moveToLetter, int moveToNumber, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            board.IsCheck = false;
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Bishop))
            {
                if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
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
            if (board.Matrix[moveToLetter, moveToNumber] != PiecesForm.Empty)
            {
                SetMovement(board, game, moveTo, i, j, color, moveToLetter, moveToNumber, queen, king);
            }
            else if (moveToLetter == moveTo.Letter && moveToNumber == moveTo.Number)
            {
                BishopMovement(board, game, moveTo, i, j, color, queen, king);
            }
            else
            {
                res = false;
            }
        }
        return res;
    }
    public void DiagonalRightSide(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, string upOrDown, bool queen, bool king)
    {
        if (king)
        {
            moveTo.Number = 8;
        }
        int moveToLetter = moveFrom.Letter;
        for (int b = moveFrom.Number; b <= moveTo.Number; b++)
        {
            if (upOrDown == "up")
            {
                moveToLetter -= 1;
            }
            if (upOrDown == "down")
            {
                moveToLetter += 1;
            }
            int moveToNumber = b + 1;
            if (moveToNumber == 9)
            {
                moveToNumber -= 1;
            }
            bool res = DiagonalSideMovement(board, game, moveTo, i, j, color, moveToLetter, moveToNumber, queen, king);
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
    public void DiagonaLeftSide(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, string upOrDown, bool queen, bool king)
    {
        if (king)
        {
            moveTo.Number = 1;
        }
        int moveToLetter = moveFrom.Letter;
        for (int b = moveFrom.Number; b >= moveTo.Number; b--)
        {
            if (upOrDown == "up")
            {
                moveToLetter -= 1;
            }
            if (upOrDown == "down")
            {
                moveToLetter += 1;
            }
            int moveToNumber = b - 1;
            if (moveToNumber == 0)
            {
                moveToNumber += 1;
            }
            bool res = DiagonalSideMovement(board, game, moveTo, i, j, color, moveToLetter, moveToNumber, queen, king);
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
    public void BishopPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen)
    {

        if (AcceptedBishopMovements(moveFrom, moveTo))
        {
            
            if (moveFrom.Letter > moveTo.Letter)
            {
                // DIAGONAL UP
                if (moveFrom.Number < moveTo.Number)
                {
                    // RIGHT SIDE
                    DiagonalRightSide(board, game, moveFrom, moveTo, i, j, color, "up", queen, false);
                }
                if (moveFrom.Number > moveTo.Number)
                {
                    // LEFT SIDE
                    DiagonaLeftSide(board, game, moveFrom, moveTo, i, j, color, "up", queen, false);
                }
            }
            if (moveFrom.Letter < moveTo.Letter)
            {
                // DIAGONAL DOWN
                if (moveFrom.Number < moveTo.Number)
                {
                    // RIGHT SIDE
                    DiagonalRightSide(board, game, moveFrom, moveTo, i, j, color, "down", queen, false);
                }
                if (moveFrom.Number > moveTo.Number)
                {
                    // LEFT SIDE
                    DiagonaLeftSide(board, game, moveFrom, moveTo, i, j, color, "down", queen, false);
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }

}