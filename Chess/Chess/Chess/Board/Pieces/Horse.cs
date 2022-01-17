namespace Chess;
#nullable disable
public class Horse : Pieces
{
    private static void HorseMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Horse + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    public void HorsePossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if ((moveFrom.Letter - 2 == moveTo.Letter && (moveFrom.Number - 1 == moveTo.Number || moveFrom.Number + 1 == moveTo.Number)) ||
            (moveFrom.Letter + 2 == moveTo.Letter && (moveFrom.Number - 1 == moveTo.Number || moveFrom.Number + 1 == moveTo.Number)) ||
            ((moveFrom.Letter - 1 == moveTo.Letter || moveFrom.Letter + 1 == moveTo.Letter) &&
            (moveFrom.Number + 2 == moveTo.Number || moveFrom.Number - 2 == moveTo.Number)))
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
                HorseMovement(board, game, moveTo, i, j, color);
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}

