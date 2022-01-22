namespace Chess;
public class IsCheckByPawn
{
    private readonly Check check = new();
    private readonly Pawn pawn = new();
    public void IsCheckByPawnMethod(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();

        // DIAGONAL UP
        if (originalMoveFrom.Letter > originalMoveTo.Letter)
        {
            // RIGHT
            RightDiagonal(check, pawn, board, game, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            if (board.IsCheck.IsCheck == false)
            {
                // LEFT
                LeftDiagonal(check, pawn, board, game, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            }
        }
        // DIAGONAL DOWN
        if (originalMoveFrom.Letter < originalMoveTo.Letter)
        {
            // RIGHT
            RightDiagonal(check, pawn, board, game, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            if (board.IsCheck.IsCheck == false)
            {
                // LEFT
                LeftDiagonal(check, pawn, board, game, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            }
        }
    }

    private static void RightDiagonal(Check check, Pawn pawn, Board board, Game game, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        Move move = new();
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);

        moveToChange.Letter = originalMoveFrom.Letter - 1;
        moveToChange.Number = originalMoveFrom.Number + 1;
        pawn.DiagonalCapturedMovement(board, move, game, moveFromChange, moveToChange, 0, 0, color, true);

    }

    private static void LeftDiagonal(Check check, Pawn pawn, Board board, Game game, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        Move move = new();
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);

        moveToChange.Letter = originalMoveFrom.Letter - 1;
        moveToChange.Number = originalMoveFrom.Number - 1;
        pawn.DiagonalCapturedMovement(board, move, game, moveFromChange, moveToChange, 0, 0, color, true);

    }


}

