namespace Chess;
#nullable disable
public class SpecialMove : Player
{
    public string SpecialMoveName { get; set; } = string.Empty;
    public bool NormalMove { get; set; }
    public bool SpecialMoveIsPossible { get; set; } = false;
    public string SpecialMovePieceType { get; set; }

    //the piece the player chooses to move
    public string SpecialMovementFirstPieceTo { get; set; }
    public string SpecialMovementSecondPieceTo { get; set; }

    // Castling
    public CastlingType CastlingType { get; set; }

    // EnPassant
    public string AllowedEnPassantPawn { get; set; }


#nullable enable
    private static void BoardMovePrep(string moveFrom, string moveTo, out int moveFromLetter, out int moveFromNumber, out int moveToLetter, out int moveToNumber)
    {
        Board moveFromInt = new();
        (moveFromLetter, moveFromNumber) = moveFromInt.GetIntegerMove(moveFrom);

        Board moveToInt = new();
        (moveToLetter, moveToNumber) = moveFromInt.GetIntegerMove(moveTo);
    }
    private SpecialMove Castling(string[,] board, SpecialMove sp, int moveFromLetter, int moveFromNumber, string pieceColor)
    {
        Castling castling = new();
        int l;
        if (pieceColor.Contains(PiecesColor.White.ToString()))
        {
            l = 8;
            sp = castling.IsPossibleCastlingMove(board, sp, moveFromLetter, moveFromNumber, l, PiecesColor.W.ToString());
        }
        else
        {
            l = 1;
            sp = castling.IsPossibleCastlingMove(board, sp, moveFromLetter, moveFromNumber, l, PiecesColor.B.ToString());
        }
        if (sp.SpecialMoveIsPossible)
        {
            sp.SpecialMoveName = SpecialMovesName.Castling;
            sp.SpecialMovePieceType = PiecesForm.Tower;
        }
        return sp;
    }

    public SpecialMove HasSpecialMoves(string[,] board, string moveFrom, string moveTo, string pieceColor)
    {
        SpecialMove sp = new();
        int moveFromLetter, moveFromNumber; 
        int moveToLetter, moveToNumber;
        BoardMovePrep(moveFrom, moveTo, out moveFromLetter, out moveFromNumber, out moveToLetter, out moveToNumber);

        bool flag = false;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (flag)
            {
                break;
            }
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[moveFromLetter, moveFromNumber] != PiecesForm.Empty)
                {
                    if (board[moveFromLetter, moveFromNumber].Contains(PiecesForm.Pawn))
                    {
                        sp = EnPassant(board, sp, moveFromLetter, moveFromNumber, moveToLetter, moveToNumber, pieceColor);
                        flag = true;
                        break;
                    }
                    if (board[moveFromLetter, moveFromNumber].Contains(PiecesForm.Tower))
                    {
                        sp = Castling(board, sp, moveFromLetter, moveFromNumber, pieceColor);
                        flag = true;
                        break;
                    }
                }
            }
        }
        return sp;
    }


    private SpecialMove EnPassant(string[,] board, SpecialMove sp, int moveFromLetter, int moveFromNumber, int moveToLetter, int moveToNumber, string pieceColor)
    {
        EnPassant enPassant = new();
        if (pieceColor.Contains(PiecesColor.White.ToString()))
        {
            sp = enPassant.IsPossibleEnPassantMove(board, sp, moveFromLetter, moveFromNumber, moveToLetter, moveToNumber, PiecesColor.W.ToString());
        }
        else
        {
            sp = enPassant.IsPossibleEnPassantMove(board, sp, moveFromLetter, moveFromNumber, moveToLetter, moveToNumber, PiecesColor.B.ToString());
        }
        if (sp.SpecialMoveIsPossible)
        {
            sp.SpecialMoveName = SpecialMovesName.EnPassant;
            sp.SpecialMovePieceType = PiecesForm.Pawn;
        }
        return sp;
    }


}
