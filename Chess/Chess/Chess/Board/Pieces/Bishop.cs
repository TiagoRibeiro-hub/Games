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
            if (board.Matrix[moveTo.Letter, moveTo.Number].Contains(color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                game = CheckMate(board.Matrix, game, moveTo.Letter, moveTo.Number);
                BishopMovement(board, game, moveTo, i, j, color);
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }


}