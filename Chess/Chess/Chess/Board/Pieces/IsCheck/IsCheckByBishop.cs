namespace Chess;

public class IsCheckByBishop
{
    public void IsCheckByBishopMethod(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, Bishop bishop, Board originalMoveFrom)
    {
        //if (board.IsCheck == false)
        //{
        //    //bishop left up
        //    moveFrom.Letter = originalMoveFrom.Letter + 1;
        //    moveFrom.Number = originalMoveFrom.Number - 1;

        //    moveTo.Number = originalMoveFrom.Number - 1;
        //    moveTo.Letter = originalMoveFrom.Letter - 1;
        //    bishop.DiagonaLeftSide(board, game, moveFrom, moveTo, i, j, color, "up", false, true);
        //}
        //if (board.IsCheck == false)
        //{
        //    //bishop left down
        //    moveTo.Number = originalMoveFrom.Number - 1;
        //    bishop.DiagonaLeftSide(board, game, moveFrom, moveTo, i, j, color, "down", false, true);
        //}
        //if (board.IsCheck == false)
        //{
        //    //bishop right up
        //    bishop.DiagonalRightSide(board, game, moveFrom, moveTo, i, j, color, "up", false, true);
        //}
        //if (board.IsCheck == false)
        //{
        //    //bishop right down
        //    bishop.DiagonalRightSide(board, game, moveFrom, moveTo, i, j, color, "down", false, true);
        //}
    }

}