namespace Chess;
#nullable disable
public partial class Pieces
{
    public string Name { get; set; }
    public PiecesColor Color { get; set; }
    

#nullable enable
    private static PiecesColor ColorOption(string colorOpt)
    {
        PiecesColor color = new();
        if (colorOpt.Contains(PiecesColor.White.ToString()))
        {
            color = PiecesColor.W;
        }
        else
        {
            color = PiecesColor.B;
        }

        return color;
    }
    public Game PossibleMoves(string[,] board, Move move, Game game, Board moveFrom, Board moveTo, int i, int j, string colorOpt = "Black")
    {
        PiecesColor color = ColorOption(colorOpt);

        if (board[i, j].Contains(PiecesForm.Pawn + color))
        {
            // Move From is Pawn 
            Pawn pawn = new();
            pawn.PawnPossibleMoves(board, move, game, moveFrom, moveTo, i, j, color);
        }
        else if (board[i, j].Contains(PiecesForm.Tower + color))
        {
            // Move From is Tower 
            Tower tower = new();
            tower.TowerPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else if (board[i, j].Contains(PiecesForm.Horse + color))
        {
            // Move From is Horse 
            HorsePossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else if (board[i, j].Contains(PiecesForm.Bishop + color))
        {
            // Move From is Bishop 
            BishopPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else if (board[i, j].Contains(PiecesForm.Queen + color))
        {
            // Move From is Queen 
            QueenPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else if (board[i, j].Contains(PiecesForm.King + color))
        {
            // Move From is King 
            KingPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
        return game;
    }

    public static Game CheckMate(string[,] board, Game game, int moveToLetter, int moveToNumber)
    {
        if (board[moveToLetter, moveToNumber].Contains(PiecesForm.King))
        {
            // Move To is the King
            game.Messages = $"\n**** {game.WhoPlays} is the winner ****\n"; 
            game.ResultPlayedBoard = true;
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
        return game;
    }


    #region Bishop
    private static void HorsePossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if ((moveFrom.Letter - 2 == moveTo.Letter && (moveFrom.Number - 1 == moveTo.Number || moveFrom.Number + 1 == moveTo.Number)) ||
            (moveFrom.Letter + 2 == moveTo.Letter && (moveFrom.Number - 1 == moveTo.Number || moveFrom.Number + 1 == moveTo.Number)) ||
            ((moveFrom.Letter - 1 == moveTo.Letter || moveFrom.Letter + 1 == moveTo.Letter) &&
            (moveFrom.Number + 2 == moveTo.Number || moveFrom.Number - 2 == moveTo.Number)))
        {
            if (board[moveTo.Letter, moveTo.Number].Contains(color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.King))
                {
                    // Move To is the King
                    game = CheckMate(board, game, moveTo.Letter, moveTo.Number);
                }
                else
                {
                    board[moveTo.Letter, moveTo.Number] = PiecesForm.Horse + color.ToString();
                }
                board[i, j] = PiecesForm.Empty;
                game.ResultPlayedBoard = true;
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }

    private static bool AcceptedBishopMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter) &&
                    (moveFrom.Number != moveTo.Number &&
                    (moveFrom.Letter + moveFrom.Number == moveTo.Letter + moveTo.Number || moveFrom.Letter - moveFrom.Number == moveTo.Letter - moveTo.Number));
    }
    private static void BishopPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {

        if (AcceptedBishopMovements(moveFrom, moveTo))
        {
            if (board[moveTo.Letter, moveTo.Number].Contains(color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.King))
                {
                    // Move To is the King
                    game = CheckMate(board, game, moveTo.Letter, moveTo.Number);
                }
                else
                {
                    board[moveTo.Letter, moveTo.Number] = PiecesForm.Bishop + color.ToString();
                }
                board[i, j] = PiecesForm.Empty;
                game.ResultPlayedBoard = true;
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
    #endregion

    #region Queen
    private void QueenPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        Tower tower = new();
        if (tower.AcceptedTowerMovements(moveFrom, moveTo) || AcceptedBishopMovements(moveFrom, moveTo))
        {
            if (board[moveTo.Letter, moveTo.Number].Contains(color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.King))
                {
                    // Move To is the King
                    game = CheckMate(board, game, moveTo.Letter, moveTo.Number);
                }
                else
                {
                    board[moveTo.Letter, moveTo.Number] = PiecesForm.Queen + color.ToString();
                }
                board[i, j] = PiecesForm.Empty;
                game.ResultPlayedBoard = true;
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
    #endregion

    #region King
    private static void KingPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if ((moveFrom.Letter == moveTo.Letter || moveFrom.Letter - 1 == moveTo.Letter || moveFrom.Letter + 1 == moveTo.Letter) &&
            (moveFrom.Number == moveTo.Number || moveFrom.Number + 1 == moveTo.Number || moveFrom.Number - 1 == moveTo.Number))
        {
            if (board[moveTo.Letter, moveTo.Number].Contains(color.ToString()))
            {
                // Move To is the Same Color 
                game.ResultPlayedBoard = false;
            }
            else
            {
                // Move To is not the Same Color 
                if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.King))
                {
                    // Move To is the King
                    game = CheckMate(board, game, moveTo.Letter, moveTo.Number);
                }
                else
                {
                    board[moveTo.Letter, moveTo.Number] = PiecesForm.King + color.ToString();
                }
                board[i, j] = PiecesForm.Empty;
                game.ResultPlayedBoard = true;
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
    #endregion
}
