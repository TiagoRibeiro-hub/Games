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

    private static void KingMovesVertical(Board board, PiecesColor color, Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        // KING MOVE VERTICAL UP
        if (originalMoveFrom.Letter == originalMoveTo.Letter - 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {

        }

        // KING MOVE VERTICAL DOWN
        if (originalMoveFrom.Letter == originalMoveTo.Letter + 1 &&
            (originalMoveFrom.Number == originalMoveTo.Number - 1 || originalMoveFrom.Number == originalMoveTo.Number + 1))
        {

        }
    }

    private void KingMovesHorizontal(Board board, PiecesColor color, Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        // KING MOVE HORIZONTAL UP & DOWN
        if (originalMoveFrom.Number == originalMoveTo.Number &&
            (originalMoveFrom.Letter > originalMoveTo.Letter || originalMoveFrom.Letter < originalMoveTo.Letter))
        {
            // HORIZONTAL UP
            if (originalMoveFrom.Letter > originalMoveTo.Letter)
            {
                // DIAGONAL UP RIGHT
                RightDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalUp");
                if (board.IsCheck.IsCheck == false)
                {
                    // DIAGONAL UP LEFT
                    LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalUp");
                }
            }
            // HORIZONTAL DOWN
            if (originalMoveFrom.Letter < originalMoveTo.Letter)
            {
                // DIAGONAL DOWN RIGHT
                RightDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalDown");
                if (board.IsCheck.IsCheck == false)
                {
                    // DIAGONAL DOWN LEFT
                    LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalDown");
                }
            }
        }
        // KING MOVE HORIZONTAL RIGHT & LEFT
        if (originalMoveFrom.Letter == originalMoveTo.Letter &&
            (originalMoveFrom.Number > originalMoveTo.Number || originalMoveFrom.Number < originalMoveTo.Number))
        {
            // HORIZONTAL RIGHT
            if (originalMoveFrom.Number < originalMoveTo.Number)
            {
                // DIAGONAL UP RIGHT
                RightDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalRight");
                if (board.IsCheck.IsCheck == false)
                {
                    // DIAGONAL UP LEFT
                    LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalLeft");
                }
            }
            // HORIZONTAL LEFT
            if (originalMoveFrom.Number > originalMoveTo.Number)
            {
                // DIAGONAL DOWN RIGHT
                RightDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalRight");
                if (board.IsCheck.IsCheck == false)
                {
                    // DIAGONAL DOWN LEFT
                    LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, "HorizontalLeft");
                }
            }
        }
    }

    private static void RightDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color, string direction)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        if (direction.Contains("HorizontalUp"))
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
        }
        if(direction.Contains("HorizontalDown"))
        {
            moveToChange.Letter = originalMoveTo.Letter + 1; 
        }
        if (direction.Contains("HorizontalRight"))
        {
            moveToChange.Number = originalMoveTo.Number + 1;
        }
        if (direction.Contains("HorizontalLeft"))
        {
            moveToChange.Number = originalMoveTo.Number - 1;
        }
        moveToChange.Number = originalMoveFrom.Number + 1;
        _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false);
    }

    private static void LeftDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color, string direction)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        if (direction.Contains("HorizontalUp"))
        {
            moveToChange.Letter = originalMoveTo.Letter - 1;
        }
        if (direction.Contains("HorizontalDown"))
        {
            moveToChange.Letter = originalMoveTo.Letter + 1;
        }
        if (direction.Contains("HorizontalRight"))
        {
            moveToChange.Number = originalMoveTo.Number + 1;
        }
        if (direction.Contains("HorizontalLeft"))
        {
            moveToChange.Number = originalMoveTo.Number - 1;
        }
        moveToChange.Number = originalMoveFrom.Number - 1;
        _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false);
    }


}

