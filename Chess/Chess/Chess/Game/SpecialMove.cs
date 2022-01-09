namespace Chess;
#nullable disable
public class SpecialMove : Castling
{
    public string SpecialMoveName { get; set; } = string.Empty;
    public bool NormalMove { get; set; }
    public bool SpecialMoveIsPossible { get; set; } = false;

    List<string> SpecialMoveListPawn { get; set; }

#nullable enable
    public SpecialMove HasSpecialMoves(string[,] board, string moveFrom, string pieceColor)
    {
        SpecialMove sp = new();
        Board move = new();
        int moveFromLetter;
        int moveFromNumber;
        (moveFromLetter, moveFromNumber) = move.GetIntegerMove(moveFrom);

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[moveFromLetter, moveFromNumber] != PiecesForm.Empty)
                {
                    //if (board[moveFromLetter, moveFromNumber].Contains(PiecesForm.Pawn))
                    //{
                    //    sp = EnPassant(board, moveFromLetter, moveFromNumber, pieceColor);
                    //    break;
                    //}
                    if (board[moveFromLetter, moveFromNumber].Contains(PiecesForm.Tower))
                    {
                        sp = Castling(board, sp, moveFromLetter, moveFromNumber, pieceColor);
                        break;
                    }
                }
            }
        }
        return sp;
    }
    //private SpecialMove EnPassant(string[,] board, int moveFromLetter, int moveFromNumber, string pieceColor)
    //{
 
    //}

    private SpecialMove Castling(string[,] board, SpecialMove sp, int moveFromLetter, int moveFromNumber, string pieceColor)
    {
        int l;
        if (pieceColor.Contains(PiecesColor.White.ToString()))
        {
            l = 8;
            sp = IsPossibleCastlingMove(board, sp, moveFromLetter, moveFromNumber, l, PiecesColor.W.ToString());
        }
        else
        {
            l = 1;
            sp = IsPossibleCastlingMove(board, sp, moveFromLetter, moveFromNumber, l, PiecesColor.B.ToString());
        }
        if (sp.SpecialMoveIsPossible)
        {
            sp.SpecialMoveName = SpecialMovesName.Castling;
        }
        return sp;
    }

}


public static class SpecialMovesName
{
    internal const string EnPassant = "EnPassant";
    internal const string Castling = "Castling";

}
