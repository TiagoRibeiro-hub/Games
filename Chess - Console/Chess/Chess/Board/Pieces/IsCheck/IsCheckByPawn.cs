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
                HorizontaLMovementUp(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

            }
            // HORIZONTAL DOWN
            if (originalMoveFrom.Letter < originalMoveTo.Letter)
            {
                HorizontaLMovementDown(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
            }
        }
        // KING MOVE HORIZONTAL RIGHT & LEFT
        if (originalMoveFrom.Letter == originalMoveTo.Letter &&
            (originalMoveFrom.Number > originalMoveTo.Number || originalMoveFrom.Number < originalMoveTo.Number))
        {
            //// HORIZONTAL RIGHT
            //if (originalMoveFrom.Number < originalMoveTo.Number)
            //{
            //    // DIAGONAL UP RIGHT
            //    HorizontaLMovementUp(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, HorizontalRight);
            //    if (board.IsCheck.IsCheck == false)
            //    {
            //        // DIAGONAL UP LEFT
            //        LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, HorizontalRight);
            //    }
            //}
            //// HORIZONTAL LEFT
            //if (originalMoveFrom.Number > originalMoveTo.Number)
            //{
            //    // DIAGONAL DOWN RIGHT
            //    HorizontaLMovementUp(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, HorizontalLeft);
            //    if (board.IsCheck.IsCheck == false)
            //    {
            //        // DIAGONAL DOWN LEFT
            //        LeftDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color, HorizontalLeft);
            //    }
            //}
        }
    }


    private void HorizontaLMovementUp(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            WhiteHorizontalMovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        else
        {
            BlackHorizontalMovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }

    }
    private void HorizontaLMovementDown(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            WhiteHorizontalMovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        else
        {
            BlackHorizontalMovementCheckDiagonal(check, board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }
    private static void BlackHorizontalMovementCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        
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
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false);
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            if (board.IsCheck.IsCheck == true)
            {
                break;
            }
        }
    }
    private static void WhiteHorizontalMovementCheckDiagonal(Check check, Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        
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
            _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false);
            check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
            if (board.IsCheck.IsCheck == true)
            {
                break;
            }
        }
    }

























    //private void DiagonalMovement(Board board, Board originalMoveTo, Board moveToChange, PiecesColor color, string direction)
    //{
    //    if (direction.Contains(HorizontalRight))
    //    {
    //        CheckDiagonalRight(originalMoveTo, moveToChange, color);
    //    }
    //    if (direction.Contains(HorizontalLeft))
    //    {
    //        CheckDiagonalRight(originalMoveTo, moveToChange, color);
    //    }

    //    _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Pawn, color, false, false);
    //}
    //private static void CheckDiagonalRight(Board originalMoveTo, Board moveToChange, PiecesColor color)
    //{
    //    WichPlayerPlaysCheck(originalMoveTo, moveToChange, color);
    //    moveToChange.Number = originalMoveTo.Number + 1;
    //}
    //private static void CheckDiagonalLeft(Board originalMoveTo, Board moveToChange, PiecesColor color)
    //{
    //    WichPlayerPlaysCheck(originalMoveTo, moveToChange, color);
    //    moveToChange.Number = originalMoveTo.Number - 1;
    //}
    //private static void WichPlayerPlaysCheck(Board originalMoveTo, Board moveToChange, PiecesColor color)
    //{
    //    if (color.ToString().Contains("." + PiecesColor.W))
    //    {
    //        // WHITE PLAYER CHECK DIAGONAL UP RIGHT
    //        moveToChange.Letter = originalMoveTo.Letter - 1;
    //    }
    //    else
    //    {
    //        // BLACK PLAYER CHECK DIAGONAL DOWN RIGHT
    //        moveToChange.Letter = originalMoveTo.Letter + 1;
    //    }
    //}



}

