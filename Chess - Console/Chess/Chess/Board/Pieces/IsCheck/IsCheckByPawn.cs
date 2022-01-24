namespace Chess;
public class IsCheckByPawn
{
    private readonly Check check = new();

    public void IsCheckByPawnMethod(Board board, Game game, Board moveFrom, Board moveTo, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();

        // KING MOVES HORIZONTAL
        KingMovesHorizontal(board, color, originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);

        // KING MOVES VERTICAL
        KingMovesVertical(board, color, originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
    }

    private void KingMovesVertical(Board board, PiecesColor color, Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        // KING MOVE VERTICAL UP
        if (originalMoveFrom.Letter == originalMoveTo.Letter - 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {
            UpDownDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }

        // KING MOVE VERTICAL DOWN
        if (originalMoveFrom.Letter == originalMoveTo.Letter + 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {
            UpDownDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }  
    private void KingMovesHorizontal(Board board, PiecesColor color, Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        // KING MOVE HORIZONTAL UP & DOWN
        if (originalMoveFrom.Number == originalMoveTo.Number &&
            (originalMoveFrom.Letter > originalMoveTo.Letter || originalMoveFrom.Letter < originalMoveTo.Letter))
        {
            HorizontaLMovementUpDown(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        // KING MOVE HORIZONTAL RIGHT & LEFT
        if (originalMoveFrom.Letter == originalMoveTo.Letter &&
            (originalMoveFrom.Number > originalMoveTo.Number || originalMoveFrom.Number < originalMoveTo.Number))
        {
            UpDownDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }
    
    private void HorizontaLMovementUpDown(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            WhiteMovementHorizontalCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        else
        {
            BlackMovementHorizontalCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }
    private static void BlackMovementHorizontalCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        for (int i = 0; i < 2; i++)
        {
            moveToChange.Letter = originalMoveTo.Letter + 1;
            if (i == 0)
            {
                // CHECK DIAGONAL RIGHT
                moveToChange.Number = originalMoveFrom.Number + 1;
            }
            if (i == 1)
            {
                // CHECK DIAGONAL LEFT
                moveToChange.Number = originalMoveFrom.Number - 1;
            }
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false, false);
            if (board.IsCheck.IsCheck == true)
            {
                break;
            }
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        }
    }
    private static void WhiteMovementHorizontalCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        for (int i = 0; i < 2; i++)
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
            if (i == 0)
            {
                // CHECK DIAGONAL RIGHT
                moveToChange.Number = originalMoveFrom.Number + 1;
            }
            if (i == 1)
            {
                // CHECK DIAGONAL LEFT
                moveToChange.Number = originalMoveFrom.Number - 1;
            }
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false, false);
            if (board.IsCheck.IsCheck == true)
            {
                break;
            }
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        }
    }


    // Bishop
    private void UpDownDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        UpDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        if (board.IsCheck.IsCheck == false)
        {
            DownDiagonalCheck(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }  
    private static void UpDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        for (int i = 0; i < 2; i++)
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
            if (i == 0)
            {
                // CHECK DIAGONAL RIGHT
                moveToChange.Number = originalMoveTo.Number + 1;
            }
            if (i == 1)
            {
                // CHECK DIAGONAL LEFT
                moveToChange.Number = originalMoveTo.Number - 1;
            }
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false, false);
            if (board.IsCheck.IsCheck == true)
            {
                break;
            }
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        }
    }
    private static void DownDiagonalCheck(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        for (int i = 0; i < 2; i++)
        {
            moveToChange.Letter = originalMoveTo.Letter + 1;
            if (i == 0)
            {
                // CHECK DIAGONAL RIGHT
                moveToChange.Number = originalMoveTo.Number + 1;
            }
            if (i == 1)
            {
                // CHECK DIAGONAL LEFT
                moveToChange.Number = originalMoveTo.Number - 1;
            }
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false, false);
            if (board.IsCheck.IsCheck == true)
            {
                break;
            }
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        }
    }


}

