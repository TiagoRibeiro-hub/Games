namespace Chess;

public class Bishop : Pieces
{
    private static void BishopMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Bishop + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    public bool AcceptedBishopMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter) &&
                    (moveFrom.Number != moveTo.Number &&
                    (moveFrom.Letter + moveFrom.Number == moveTo.Letter + moveTo.Number || moveFrom.Letter - moveFrom.Number == moveTo.Letter - moveTo.Number));
    }
    public void BishopPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {

        if (AcceptedBishopMovements(moveFrom, moveTo))
        {
            int count = 0;
            if (moveFrom.Letter > moveTo.Letter)
            {
                // DIAGONAL UP
                if (moveFrom.Number < moveTo.Number)
                {
                    // RIGHT SIDE
                    DiagonalRightSide(board, game, moveFrom, moveTo, i, j, color, count);
                }
                if (moveFrom.Number > moveTo.Number)
                {
                    // LEFT SIDE
                    DiagonaLeftSide(board, game, moveFrom, moveTo, i, j, color, count);
                }
            }
            if (moveFrom.Letter < moveTo.Letter)
            {
                // DIAGONAL DOWN
                if (moveFrom.Number < moveTo.Number)
                {
                    // RIGHT SIDE
                    DiagonalRightSide(board, game, moveFrom, moveTo, i, j, color, count, "down");
                }
                if (moveFrom.Number > moveTo.Number)
                {
                    // LEFT SIDE
                    DiagonaLeftSide(board, game, moveFrom, moveTo, i, j, color, count, "down");
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
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
    private static void SetMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color, int moveToLetter, int moveToNumber)
    {
        if(MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
        {
            // Move To is not the Same Color
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
            {
                // Move To is the King           
                game = CheckMate(board.Matrix, game, moveToLetter, moveToNumber);
            }
            BishopMovement(board, game, moveTo, i, j, color);
            game.ResultPlayedBoard = true;
        }
        else
        {
            // Move To is the Same Color 
            game.ResultPlayedBoard = false;
        }

    }

    private static bool DiagonalSideMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color, int moveToLetter, int moveToNumber)
    {
        bool res = true;
        if (board.Matrix[moveToLetter, moveToNumber] != PiecesForm.Empty)
        {
            SetMovement(board, game, moveTo, i, j, color, moveToLetter, moveToNumber);
        }
        else if (moveToLetter == moveTo.Letter && moveToNumber == moveTo.Number)
        {
            BishopMovement(board, game, moveTo, i, j, color);
        }
        else
        {
            res = false;
        }
        return res;
    }
    private static void DiagonalRightSide(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, int count, string upOrDown= "up")
    {
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
            bool res = DiagonalSideMovement(board, game, moveTo, i, j, color, moveToLetter, moveToNumber);
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

    private void DiagonaLeftSide(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, int count, string upOrDown = "up")
    {
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
            bool res = DiagonalSideMovement(board, game, moveTo, i, j, color, moveToLetter, moveToNumber);
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

}