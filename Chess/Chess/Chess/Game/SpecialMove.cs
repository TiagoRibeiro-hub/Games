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
    public CastlingType CastlingType { get; set; } = CastlingType.empty;

    // EnPassant
    public string AllowedEnPassantPawn { get; set; }


#nullable enable
    private static void BoardMovePrep(string moveFrom, string moveTo, out Board moveFromInt, out Board moveToInt)
    {
        moveFromInt = new();
        (moveFromInt.Letter, moveFromInt.Number) = moveFromInt.GetIntegerMove(moveFrom);

        moveToInt = new();
        (moveToInt.Letter, moveToInt.Number) = moveFromInt.GetIntegerMove(moveTo);
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

    public bool HasSpecialMoves(Board board, string moveFrom, string moveTo, string pieceColor)
    {
        SpecialMove sp = new();
        Board moveFromInt;
        Board moveToInt;
        BoardMovePrep(moveFrom, moveTo, out moveFromInt, out moveToInt);

        bool specialMoveIsPossible = false;
        for (int i = 0; i < board.Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < board.Matrix.GetLength(1); j++)
            {
                if (board.Matrix[moveFromInt.Letter, moveFromInt.Number] != PiecesForm.Empty)
                {
                    if (board.Matrix[moveFromInt.Letter, moveFromInt.Number].Contains(PiecesForm.Pawn))
                    {
                        sp = EnPassant(board, sp, moveFromInt, moveToInt, pieceColor);
                        if (sp.SpecialMoveIsPossible)
                        {
                            board.SpecialMovesList.Add(sp);
                            specialMoveIsPossible = sp.SpecialMoveIsPossible;
                        }                        
                    }
                    if (board.Matrix[moveFromInt.Letter, moveFromInt.Number].Contains(PiecesForm.Tower))
                    {
                        sp = Castling(board.Matrix, sp, moveFromInt.Letter, moveFromInt.Number, pieceColor);
                        if (sp.SpecialMoveIsPossible)
                        {
                            board.SpecialMovesList.Add(sp);
                            specialMoveIsPossible = sp.SpecialMoveIsPossible;
                        }
                    }
                }
            }
        }
        return specialMoveIsPossible;
    }


    private SpecialMove EnPassant(Board board, SpecialMove sp, Board moveFromInt, Board moveToInt, string pieceColor)
    {
        EnPassant enPassant = new();
        if (pieceColor.Contains(PiecesColor.White.ToString()))
        {
            sp = enPassant.IsPossibleEnPassantMove(board, sp, moveFromInt, moveToInt, PiecesColor.W.ToString());
        }
        else
        {
            sp = enPassant.IsPossibleEnPassantMove(board, sp, moveFromInt, moveToInt, PiecesColor.B.ToString());
        }
        if (sp.SpecialMoveIsPossible)
        {
            sp.SpecialMoveName = SpecialMovesName.EnPassant;
            sp.SpecialMovePieceType = PiecesForm.Pawn;
        }
        return sp;
    }


}
