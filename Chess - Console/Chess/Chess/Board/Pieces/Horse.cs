namespace Chess;
#nullable disable
public class Horse : Pieces
{
    private void HorseMovement(Board board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveTo.Letter, moveTo.Number] = PiecesForm.Horse + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;

        IsKingInCheck(board, color, moveTo);

        game.ResultPlayedBoard = true;
    }

    public void IsKingInCheck(Board board, PiecesColor color, Board moveTo)
    {
        Board moveLastPositionKing;
        PiecesColor newColor;
        LastPositionKing(board, color, out moveLastPositionKing, out newColor);
        
        Board originalMoveFrom = moveLastPositionKing;
        Board originalMoveTo = moveLastPositionKing;
        Board moveFromChange = new();
        Board moveToChange = new();
        IsCheckByHorse isCheckByHorse = new();

        if(moveLastPositionKing.Letter < moveTo.Letter && moveLastPositionKing.Number < moveTo.Number)
        {
            // KING IS UP LEFT - HORSE IS DOWN RIGHT
            isCheckByHorse.DownRight(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        if (moveLastPositionKing.Letter < moveTo.Letter && moveLastPositionKing.Number > moveTo.Number)
        {
            // KING IS UP RIGHT - HORSE IS DOWN LEFT
            isCheckByHorse.DownLeft(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        if (moveLastPositionKing.Letter > moveTo.Letter && moveLastPositionKing.Number > moveTo.Number)
        {
            // KING IS DOWN RIGHT - HORSE IS UP LEFT
            isCheckByHorse.UpLeft(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
        if (moveLastPositionKing.Letter > moveTo.Letter && moveLastPositionKing.Number < moveTo.Number)
        {
            // KING IS DOWN LEFT - HORSE IS UP RIGHT
            isCheckByHorse.UpRight(board, originalMoveFrom, originalMoveTo, moveFromChange, moveToChange, color);
        }
    }
    public void HorsePossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if ((moveFrom.Letter - 2 == moveTo.Letter && (moveFrom.Number - 1 == moveTo.Number || moveFrom.Number + 1 == moveTo.Number)) ||
            (moveFrom.Letter + 2 == moveTo.Letter && (moveFrom.Number - 1 == moveTo.Number || moveFrom.Number + 1 == moveTo.Number)) ||
            ((moveFrom.Letter - 1 == moveTo.Letter || moveFrom.Letter + 1 == moveTo.Letter) &&
            (moveFrom.Number + 2 == moveTo.Number || moveFrom.Number - 2 == moveTo.Number)))
        {
            if (board.Matrix[moveTo.Letter, moveTo.Number].Contains("."+color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                CheckMate(board.Matrix, game, moveTo.Letter, moveTo.Number);
                HorseMovement(board, game, moveTo, i, j, color);
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}

