namespace Chess;
#nullable disable
public partial class Pieces
{
    public string Name { get; set; }
    public PiecesColor Color { get; set; }


#nullable enable
    private static PiecesColor ColorOption(string colorOpt)
    {
        PiecesColor color;
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
    public Game PossibleMoves(Board board, Move move, Game game, Board moveFrom, Board moveTo, int i, int j, string colorOpt = "Black")
    {
        PiecesColor color = ColorOption(colorOpt);

        if (board.Matrix[i, j].Contains(PiecesForm.Pawn + color))
        {
            // Move From is Pawn 
            Pawn pawn = new();
            pawn.PawnPossibleMoves(board, move, game, moveFrom, moveTo, i, j, color);
        }
        else if (board.Matrix[i, j].Contains(PiecesForm.Rook + color))
        {
            // Move From is Tower 
            Rook tower = new();
            tower.TowerPossibleMoves(board, game, moveFrom, moveTo, i, j, color, false);
        }
        else if (board.Matrix[i, j].Contains(PiecesForm.Knight + color))
        {
            // Move From is Horse 
            Knight horse = new();
            horse.HorsePossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else if (board.Matrix[i, j].Contains(PiecesForm.Bishop + color))
        {
            // Move From is Bishop 
            Bishop bishop = new();
            bishop.BishopPossibleMoves(board, game, moveFrom, moveTo, i, j, color, false);
        }
        else if (board.Matrix[i, j].Contains(PiecesForm.Queen + color))
        {
            // Move From is Queen 
            Queen queen = new();
            queen.QueenPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else if (board.Matrix[i, j].Contains(PiecesForm.King + color))
        {
            // Move From is King 
            King king = new();
            king.KingPossibleMoves(board, game, moveFrom, moveTo, i, j, color);
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
        return game;
    }

    public static void CheckMate(Board board, Game game, int moveToLetter, int moveToNumber)
    {
        if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
        {
            // Move To is the King
            game.Messages = $"\n**** {game.WhoPlays} is the winner ****\n";
            game.ResultPlayedBoard = true;
            board.IsCheck.IsCheckMate = true;
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }

    public void LastPositionKing(Board board, PiecesColor color, out Board moveLastPositionKing, out PiecesColor newColor)
    {
        moveLastPositionKing = new();
        if (color.ToString() == PiecesColor.W.ToString())
        {
            (moveLastPositionKing.Letter, moveLastPositionKing.Number) = Funcs.GetIntegerMove(board.IsCheck.LastKingPositionBlack);
            newColor = PiecesColor.B;
        }
        else
        {
            (moveLastPositionKing.Letter, moveLastPositionKing.Number) = Funcs.GetIntegerMove(board.IsCheck.LastKingPositionWhite);
            newColor = PiecesColor.W;
        }
    }
}
