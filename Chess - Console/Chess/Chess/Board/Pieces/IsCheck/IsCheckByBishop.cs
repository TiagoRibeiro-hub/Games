namespace Chess;

public class IsCheckByBishop
{
    private readonly Check check = new();
    public void IsCheckByBishopMethod(Board board, Game game, Board moveFrom, Board moveTo, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();
        if (originalMoveFrom.Letter >= 5 && (originalMoveFrom.Number == 4 || originalMoveFrom.Number == 5))
        {
            // FIRST DOWN THEN UP
            DownRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            UpRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            UpLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

        }
        else if (originalMoveFrom.Letter <= 4 && (originalMoveFrom.Number == 4 || originalMoveFrom.Number == 5))
        {
            // FIRST UO THEN DOWN
            UpRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            UpLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

        }
        else if (originalMoveFrom.Number <= 3 && (originalMoveFrom.Letter == 4 || originalMoveFrom.Letter == 5))
        {
            // FIRST UP & DOWN LEFT THEN RIGHT
            UpLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            UpRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

        }
        else if (originalMoveFrom.Number >= 6 && (originalMoveFrom.Letter == 4 || originalMoveFrom.Letter == 5))
        {
            // FIRST UP & DOWN RIGHT THEN LEFT
            UpRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownRightDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            UpLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            DownLeftDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }

    public void DownRightDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        bool flag = false;
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        // CHECK DIAGONAL DOWN RIGHT
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalDownRight;
        moveToChange.Letter = originalMoveTo.Letter + 1;
        moveToChange.Number = originalMoveTo.Number + 1;

        for (int i = 1; i < board.Matrix.GetLength(0); i++)
        {
            if (flag)
            {
                break;
            }
            for (int j = 1; j < board.Matrix.GetLength(1); j++)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    bool stopSearch = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Bishop, color, false, false, true, false, false);
                    if (stopSearch || moveToChange.Number == 8)
                    {
                        flag = true;
                        break;
                    }
                    moveToChange.Letter += 1;
                    moveToChange.Number += 1;
                    break;
                }
            }
        }
    }
    public void DownLeftDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        bool flag = false;
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        // CHECK DIAGONAL DOWN LEFT
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalDownLeft;
        moveToChange.Letter = originalMoveTo.Letter + 1;
        moveToChange.Number = originalMoveTo.Number - 1;

        for (int i = 1; i < board.Matrix.GetLength(0); i++)
        {
            if (flag)
            {
                break;
            }
            for (int j = 8; j > 0; j--)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    bool stopSearch = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Bishop, color, false, false, true, false, false);
                    if (stopSearch || moveToChange.Number == 1)
                    {
                        flag = true;
                        break;
                    }
                    moveToChange.Letter += 1;
                    moveToChange.Number -= 1;
                    break;
                }
            }
        }
    }

    public void UpRightDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        bool flag = false;
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        // CHECK DIAGONAL UP RIGHT
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalUpRight;
        moveToChange.Letter = originalMoveTo.Letter - 1;
        moveToChange.Number = originalMoveTo.Number + 1;

        for (int i = 8; i > 0; i--)
        {
            if (flag)
            {
                break;
            }
            for (int j = 1; j < board.Matrix.GetLength(1); j++)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    bool stopSearch = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Bishop, color, false, false, true, false, false);
                    if (stopSearch || moveToChange.Number == 8)
                    {
                        flag = true;
                        break;
                    }
                    moveToChange.Letter -= 1;
                    moveToChange.Number += 1;
                    break;
                }
            }
        }
    }
    public void UpLeftDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        bool flag = false;
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        // CHECK DIAGONAL UP LEFT
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalUpLeft;
        moveToChange.Letter = originalMoveTo.Letter - 1;
        moveToChange.Number = originalMoveTo.Number - 1;

        for (int i = 8; i > 0; i--)
        {
            if (flag)
            {
                break;
            }
            for (int j = 8; j > 0; j--)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    bool stopSearch = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Bishop, color, false, false, true, false, false);
                    if (stopSearch || moveToChange.Number == 1)
                    {
                        flag = true;
                        break;
                    }
                    moveToChange.Letter -= 1;
                    moveToChange.Number -= 1;
                    break;
                }
            }
        }
    }


}