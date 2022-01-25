namespace Chess;

public class IsCheckByKnight
{
    private readonly Check check = new();
    public void IsCheckByHorseMethod(Board board, Board moveFrom, Board moveTo, PiecesColor color)
    {
        Board originalMoveFrom = moveFrom; Board moveFromChange = new();
        Board originalMoveTo = moveTo; Board moveToChange = new();
        // UP RIGHT
        UpRight(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

        // DOWN RIGHT
        DownRight(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

        // UP LEFT
        UpLeft(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);

        // DOWN LEFT
        DownLeft(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
    }

    public void UpRight(Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalUpRight;
        // letter -1 number +2
        moveToChange.Letter = originalMoveTo.Letter - 1;
        moveToChange.Number = originalMoveTo.Number + 2;

        for (int i = 8; i > 0; i--)
        {
            for (int j = 1; j < board.Matrix.GetLength(1); j++)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Rook, color, false, false, false, false, true);

                    // letter -2 number +1
                    moveToChange.Letter = originalMoveTo.Letter - 2;
                    moveToChange.Number = originalMoveTo.Number + 1;
                    break;
                }
            }
        }
    }
    public void UpLeft(Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalUpLeft;
        // letter -1 number -2
        moveToChange.Letter = originalMoveTo.Letter - 1;
        moveToChange.Number = originalMoveTo.Number - 2;

        for (int i = 8; i > 0; i--)
        {
            for (int j = 8; j > 0; j--)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Rook, color, false, false, false, false, true);

                    // letter -2 number -1
                    moveToChange.Letter = originalMoveTo.Letter - 2;
                    moveToChange.Number = originalMoveTo.Number - 1;
                    break;
                }
            }
        }
    }


    public void DownRight(Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalDownRight;
        // letter + 1 number + 2
        moveToChange.Letter = originalMoveTo.Letter + 1;
        moveToChange.Number = originalMoveTo.Number + 2;

        for (int i = 1; i < board.Matrix.GetLength(0); i++)
        {
            for (int j = 1; j < board.Matrix.GetLength(1); j++)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Rook, color, false, false, false, false, true);

                    // letter + 2 number + 1
                    moveToChange.Letter = originalMoveTo.Letter + 2;
                    moveToChange.Number = originalMoveTo.Number + 1;
                    break;
                }
            }
        }
    }
    public void DownLeft(Board board, Board originalMoveFrom, Board originalMoveTo, Board moveFromChange, Board moveToChange, PiecesColor color)
    {
        check.ResetMoves(originalMoveFrom, moveFromChange, originalMoveTo, moveToChange);
        board.IsCheck.SideToCheck = SideToCheckOpt.DiagonalDownLeft;
        // letter + 1 number -2
        moveToChange.Letter = originalMoveTo.Letter + 1;
        moveToChange.Number = originalMoveTo.Number - 2;

        for (int i = 1; i < board.Matrix.GetLength(0); i++)
        {
            for (int j = 8; j > 0; j--)
            {
                if (i == moveToChange.Letter && j == moveToChange.Number)
                {
                    _ = board.IsCheck.CheckPiece(board, moveToChange.Letter, moveToChange.Number, PiecesForm.Rook, color, false, false, false, false, true);

                    // letter + 2 number -1
                    moveToChange.Letter = originalMoveTo.Letter + 2;
                    moveToChange.Number = originalMoveTo.Number - 1;
                    break;
                }
            }
        }
    }


}