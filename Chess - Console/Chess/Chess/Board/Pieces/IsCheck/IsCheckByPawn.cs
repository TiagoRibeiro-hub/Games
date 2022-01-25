namespace Chess;
public class IsCheckByPawn
{
    private readonly Check check = new();

    public void IsCheckByPawnMethod(Board board, Board moveFrom, Board moveTo, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();

        // KING MOVES HORIZONTAL
        KingMovesHorizontalAndVertical(board, color, originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);

        // KING MOVES VERTICAL
        KingMovesDiagonal(board, color, originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
    }
    private void KingMovesHorizontalAndVertical(Board board, PiecesColor color, Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        // KING MOVE VERTICAL UP & DOWN
        if (originalMoveFrom.Number == originalMoveTo.Number &&
            (originalMoveFrom.Letter > originalMoveTo.Letter || originalMoveFrom.Letter < originalMoveTo.Letter))
        {
            MovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        // KING MOVE HORIZONTAL RIGHT & LEFT
        if (originalMoveFrom.Letter == originalMoveTo.Letter &&
           (originalMoveFrom.Number > originalMoveTo.Number || originalMoveFrom.Number < originalMoveTo.Number))
        {
            MovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }

    private void KingMovesDiagonal(Board board, PiecesColor color, Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        // KING MOVE DIAGONAL UP RIGHT & LEFT
        if (originalMoveFrom.Letter == originalMoveTo.Letter - 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {
            MovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }

        // KING MOVE DIAGONAL DOWN RIGHT & LEFT
        if (originalMoveFrom.Letter == originalMoveTo.Letter + 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {
            MovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }

    private void MovementCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            WhiteMovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        else
        {
            BlackMovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }
    private static void BlackMovementCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        for (int i = 0; i < 2; i++)
        {
            moveToChange.Letter = originalMoveTo.Letter + 1;
            if (i == 0)
            {
                // CHECK DIAGONAL RIGHT
                moveToChange.Number = originalMoveFrom.Number + 1;
                board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalDownRight;
            }
            if (i == 1)
            {
                // CHECK DIAGONAL LEFT
                moveToChange.Number = originalMoveFrom.Number - 1;
                board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalDownLeft;
            }
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false, false, true, false);
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        }
    }
    private static void WhiteMovementCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        for (int i = 0; i < 2; i++)
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
            if (i == 0)
            {
                // CHECK DIAGONAL RIGHT
                moveToChange.Number = originalMoveFrom.Number + 1;
                board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalUpRight;

            }
            if (i == 1)
            {
                // CHECK DIAGONAL LEFT
                moveToChange.Number = originalMoveFrom.Number - 1;
                board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalUpLeft;
            }
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false, false, true, false);
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        }
    }

}

