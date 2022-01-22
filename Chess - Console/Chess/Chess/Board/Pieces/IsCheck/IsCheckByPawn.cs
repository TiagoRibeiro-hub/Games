namespace Chess;
public class IsCheckByPawn
{
    private readonly Check check = new();

    public void IsCheckByPawnMethod(Board board, Game game, Board moveFrom, Board moveTo, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();

        // DIAGONAL UP
        if (originalMoveFrom.Letter > originalMoveTo.Letter)
        {
            // RIGHT
            RightDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, true);
            if (board.IsCheck.IsCheck == false)
            {
                // LEFT
                LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, true);
            }
        }
        // DIAGONAL DOWN
        if (originalMoveFrom.Letter < originalMoveTo.Letter)
        {
            // RIGHT
            RightDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, false);
            if (board.IsCheck.IsCheck == false)
            {
                // LEFT
                LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, false);
            }
        }
    }
    // COLOR PROBLEM
    private static void RightDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color, bool up)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        if (up)
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
        }
        else
        {
            moveToChange.Letter = originalMoveTo.Letter + 1;
        }
        moveToChange.Number = originalMoveFrom.Number + 1;
        _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false);


    }

    private static void LeftDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color, bool up)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        if (up)
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
        }
        else
        {
            moveToChange.Letter = originalMoveTo.Letter + 1;
        }
        moveToChange.Number = originalMoveFrom.Number - 1;
        _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false);
    }


}

