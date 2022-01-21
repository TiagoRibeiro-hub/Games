namespace Chess;
#nullable disable
public class Castling
{
    public string CastlingTowerMovesTo { get; set; }
    public string CastlingKingMovesTo { get; set; }
    public CastlingType CastlingType { get; set; }

    public SpecialMove IsPossibleCastlingMove(string[,] board, SpecialMove sp, int moveFromLetter, int moveFromNumber, int l, string color)
    {
        // Rules Missing:
        // The King cannot be in Check.
        // The squares the King has to go over when castling cannot be under attack and not the square where it lands on.
        string moveToLetter = string.Empty;
        if (l == 8)
        {
            moveToLetter = "h";
        }
        else if (l == 1)
        {
            moveToLetter = "a";
        }
        if (moveFromLetter == l && moveFromNumber == 1)
        {
            if ((board[moveFromLetter, moveFromNumber + 1] == PiecesForm.Empty) &&
                (board[moveFromLetter, moveFromNumber + 2] == PiecesForm.Empty) &&
                (board[moveFromLetter, moveFromNumber + 3] == PiecesForm.Empty) &&
                (board[moveFromLetter, moveFromNumber + 4] == PiecesForm.King + color))
            {
                int moveToNumberTower = moveFromNumber + 3;
                int moveToNumberKing = moveFromNumber + 2;
                // possible 0-0-0
                sp.SpecialMoveIsPossible = true;
                // tower change with king
                this.CastlingTowerMovesTo = moveToLetter + moveToNumberTower.ToString();
                this.CastlingKingMovesTo = moveToLetter + moveToNumberKing.ToString();
                this.CastlingType = CastlingType.threeZeros;

            }
        }
        else if (moveFromLetter == l && moveFromNumber == 8)
        {
            if ((board[moveFromLetter, moveFromNumber - 1] == PiecesForm.Empty) &&
                (board[moveFromLetter, moveFromNumber - 2] == PiecesForm.Empty) &&
                (board[moveFromLetter, moveFromNumber - 3] == PiecesForm.King + color))
            {
                int moveToNumberTower = moveFromNumber - 2;
                int moveToNumberKing = moveFromNumber - 1;
                // possible 0-0
                sp.SpecialMoveIsPossible = true;
                // tower change with king
                this.CastlingTowerMovesTo = moveToLetter + moveToNumberTower.ToString();
                this.CastlingKingMovesTo = moveToLetter + moveToNumberKing.ToString();
                this.CastlingType = CastlingType.twoZeros;
            }
        }
        else
        {
            sp.SpecialMoveIsPossible = false;
        }
        if (sp.SpecialMoveIsPossible)
        {
            sp.SpecialMovementFirstPieceTo = this.CastlingTowerMovesTo;
            sp.SpecialMovementSecondPieceTo = this.CastlingKingMovesTo;
            sp.CastlingType = this.CastlingType;
        }
        return sp;
    }
    public void CastlingMovement(string[,] board, Board moveTower, Board moveKing, string pieceColor, int moveKingEmptyNumber, int moveTowerEmptyNumber, int moveEmptyLetter, int i, int j)
    {
        if (i == moveKing.Letter && j == moveKing.Number)
        {
            // KING
            board[i, j] = PiecesForm.King + pieceColor;
            board[moveEmptyLetter, moveKingEmptyNumber] = PiecesForm.Empty;
        }
        else if (i == moveTower.Letter && j == moveTower.Number)
        {
            // TOWER
            board[i, j] = PiecesForm.Tower + pieceColor;
            board[moveEmptyLetter, moveTowerEmptyNumber] = PiecesForm.Empty;
        }
    }
}
