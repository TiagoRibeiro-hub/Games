namespace Chess;
#nullable disable
public class Pieces
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
    public Game PossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, string colorOpt = "Black")
    {
        PiecesColor color = ColorOption(colorOpt);

        if (board[i, j].Contains(PiecesForm.Pawn + color))
        {
            // Move From is Pawn 
            PawnPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        if (board[i, j].Contains(PiecesForm.Tower + color))
        {
            // Move From is Tower 
            TowerPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        if (board[i, j].Contains(PiecesForm.Horse + color))
        {
            // Move From is Horse 
            HorsePossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        if (board[i, j].Contains(PiecesForm.Bishop + color))
        {
            // Move From is Bishop 
            BishopPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        if (board[i, j].Contains(PiecesForm.Queen + color))
        {
            // Move From is Queen 
            QueenPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        if (board[i, j].Contains(PiecesForm.King + color))
        {
            // Move From is King 
            KingPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        return game;
    }
    private static string ShowWinner(Game game)
    {
        return $"\n**** {game.WhoPlays} is the winner ****\n";
    }
    private static Game CheckMate(string[,] board, Game game, Board moveTo)
    {
        if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.King))
        {
            // Move To is the King
            game.Messages = ShowWinner(game);
            game.ResultPlayedBoard = true;
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
        return game;
    }
    #region Pawn
    private static void PawnMovement(string[,] board, Game game, Board moveTo, int i, int j, PiecesColor color)
    {
        board[moveTo.Letter, moveTo.Number] = PiecesForm.Pawn + color.ToString();
        board[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    private static void PawnPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        int x;
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            x = moveFrom.Letter - 1;
        }
        else
        {
            x = moveFrom.Letter + 1;
        }

        if (x == moveTo.Letter && (moveFrom.Number == moveTo.Number || moveFrom.Number + 1 == moveTo.Number || moveFrom.Number - 1 == moveTo.Number))
        {
            if (moveFrom.Number + 1 == moveTo.Number || moveFrom.Number - 1 == moveTo.Number)
            {
                // Diagonal Movement To Capture
                if (board[moveTo.Letter, moveTo.Number].Contains(color.ToString()) || 
                    board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.Empty))
                {
                    // Move To is the Same Color 
                    game.ResultPlayedBoard = false;
                }
                else
                {
                    // Move To is not the Same Color 
                    game = CheckMate(board, game, moveTo);
                    if(game.ResultPlayedBoard == false)
                    {
                        PawnMovement(board, game, moveTo, i, j, color);
                    }
                }
            }
            else
            {
                // Front Movement
                if (board[moveTo.Letter, moveTo.Number] != PiecesForm.Empty)
                {
                    // Move To has a Piece in front 
                    game.ResultPlayedBoard = false;
                }
                else
                {
                    PawnMovement(board, game, moveTo, i, j, color);
                }
            }

        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
    #endregion

    #region Tower
    private static bool AcceptedTowerMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter == moveTo.Letter && (moveFrom.Number > moveTo.Number || moveFrom.Number < moveTo.Number)) ||
                    (moveFrom.Number == moveTo.Number && (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter));
    }
    private static void TowerPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if (AcceptedTowerMovements(moveFrom, moveTo))
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
                    ShowWinner(game);
                }
                else
                {
                    board[moveTo.Letter, moveTo.Number] = PiecesForm.Tower + color.ToString();
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
                    ShowWinner(game);
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
                    ShowWinner(game);
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
    private static void QueenPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if (AcceptedTowerMovements(moveFrom, moveTo) || AcceptedBishopMovements(moveFrom, moveTo))
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
                    ShowWinner(game);
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
                    ShowWinner(game);
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
