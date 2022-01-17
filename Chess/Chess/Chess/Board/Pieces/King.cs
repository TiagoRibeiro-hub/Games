namespace Chess;

public class King : Pieces
{
    private static void KingMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Horse + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    public void KingPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if ((moveFrom.Letter == moveTo.Letter || moveFrom.Letter - 1 == moveTo.Letter || moveFrom.Letter + 1 == moveTo.Letter) &&
            (moveFrom.Number == moveTo.Number || moveFrom.Number + 1 == moveTo.Number || moveFrom.Number - 1 == moveTo.Number))
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
                KingMovement(board, game, moveTo, i, j, color);

            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
        }
    }