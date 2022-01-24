namespace Chess;
#nullable disable
public class Player 
{
    public string Name { get; set; }
    public PiecesColor PieceColor { get; set; }
    public string CoinSide { get; set; }

#nullable enable
    public Game WhitePlayer(Board board, Move move, Game game, Board moveFrom, Board moveTo, Pieces pieces)
    {
        bool flag = false;
        for (int i = 0; i < board.Matrix.GetLength(0); i++)
        {
            if (flag)
            {
                break;
            }
            for (int j = 0; j < board.Matrix.GetLength(1); j++)
            {
                if (i == moveFrom.Letter && j == moveFrom.Number)
                {
                    if(board.Matrix[i, j].Contains(PiecesForm.Empty))
                    {
                        game.ResultPlayedBoard = false;
                        flag = true;
                        break;
                    }
                    else
                    {
                        game = pieces.PossibleMoves(board, move, game, moveFrom, moveTo, i, j, PiecesColor.White.ToString());
                        flag = true;
                        break;
                    }                  
                }
            }
        }

        return game;
    }
    public Game BlackPlayer(Board board, Move move, Game game, Board moveFrom, Board moveTo, Pieces pieces)
    {
        bool flag = false;
        for (int i = 0; i < board.Matrix.GetLength(0); i++)
        {
            if (flag)
            {
                break;
            }
            for (int j = 0; j < board.Matrix.GetLength(1); j++)
            {
                if (i == moveFrom.Letter && j == moveFrom.Number)
                {
                    if (board.Matrix[i, j].Contains(PiecesForm.Empty))
                    {
                        game.ResultPlayedBoard = false;
                        flag = true;
                        break;
                    }
                    else
                    {
                        game = pieces.PossibleMoves(board, move, game, moveFrom, moveTo, i, j);
                        flag = true;
                        break;
                    }                
                }
            }
        }

        return game;
    }

}
