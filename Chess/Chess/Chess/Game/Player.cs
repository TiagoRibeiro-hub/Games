namespace Chess;
#nullable disable
public class Player
{
    public string Name { get; set; }
    public PiecesColor PieceColor { get; set; }
    public string CoinSide { get; set; }

#nullable enable
    public Game WhitePlayer(string[,] board, Game game, Board moveFrom, Board moveTo, Pieces pieces)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (i == moveFrom.Letter && j == moveFrom.Number)
                {
                    if(board[i, j].Contains(PiecesForm.Empty))
                    {
                        game.ResultPlayedBoard = false;
                        break;
                    }
                    else
                    {
                        game = pieces.PossibleMoves(board, game, moveFrom, moveTo, i, j, PiecesColor.White.ToString());
                        break;
                    }                  
                }
            }
        }

        return game;
    }
    public Game BlackPlayer(string[,] board, Game game, Board moveFrom, Board moveTo, Pieces pieces)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (i == moveFrom.Letter && j == moveFrom.Number)
                {
                    if (board[i, j].Contains(PiecesForm.Empty))
                    {
                        game.ResultPlayedBoard = false;
                        break;
                    }
                    else
                    {
                        game = pieces.PossibleMoves(board, game, moveFrom, moveTo, i, j);
                        break;
                    }                
                }
            }
        }

        return game;
    }

}
