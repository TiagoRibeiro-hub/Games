namespace Chess;
public class EnPassant
{
    private static string BlackEnPassant(SpecialMove sp, Board moveFromInt, int n, string item)
    {
        string specialMovementFirstPieceTo = Funcs.ChangeIntToLetter(moveFromInt.Letter + 1) + n.ToString();
        string specialMovementSecondPieceTo = item; // it will be empty
        return specialMovementFirstPieceTo + ";" + specialMovementSecondPieceTo; 
    }

    private static string WhiteEnPassant(SpecialMove sp, Board moveFromInt, int n, string item)
    {
        string specialMovementFirstPieceTo = Funcs.ChangeIntToLetter(moveFromInt.Letter - 1) + n.ToString();
        string specialMovementSecondPieceTo = item; // it will be empty
        return specialMovementFirstPieceTo + ";" + specialMovementSecondPieceTo;
    }

    private static void PossibleMovesEnPassantList(SpecialMove sp, int flag, string move)
    {
        if (flag > 0)
        {
            if (sp.PossibleMovesEnPassant is null)
            {
                sp.PossibleMovesEnPassant = new();
            }
            sp.PossibleMovesEnPassant.Add(move);
        }
    }

    public SpecialMove IsPossibleEnPassantMove(Board board, SpecialMove sp, Board moveFromInt, Board moveToInt, string color)
    {
        int l, n;
        int flag = 0;
        sp.SpecialMoveIsPossible = false;

        foreach (var item in board.AllowedEnPassantListPawn)
        {
            (l, n) = Funcs.GetIntegerMove(item);
            // Capture may happen for both sides
            if (moveFromInt.Letter == l && moveFromInt.Number + 1 == n)
            {
                //Capture to right
                string x;
                if (color == PiecesColor.W.ToString())
                {
                    x = WhiteEnPassant(sp, moveFromInt, n, item);
                    flag += 1;
                }
                else
                {
                    x = BlackEnPassant(sp, moveFromInt, n, item);
                    flag += 1;                 
                }
                PossibleMovesEnPassantList(sp, flag, x);
            }
            if (moveFromInt.Letter == l && moveFromInt.Number - 1 == n)
            {
                //Capture to left
                string x;
                if (color == PiecesColor.W.ToString())
                {

                    x = WhiteEnPassant(sp, moveFromInt, n, item);
                    flag += 1;
                }
                else
                {
                    x = BlackEnPassant(sp, moveFromInt, n, item);
                    flag += 1;
                }
                PossibleMovesEnPassantList(sp, flag, x);
            }
        }
        if(flag > 0)
        {
            sp.SpecialMoveIsPossible = true;
        }

        return sp;
    }

    public void EnPassantMovement(Board board, Board moveFromOriginal, Board movePlayerPawn, Board emptyOpponentPawn, string pieceColor, int i, int j)
    {
        if(i == movePlayerPawn.Letter && j == movePlayerPawn.Number)
        {
            // Capture
            board.Matrix[i, j] = PiecesForm.Pawn + pieceColor;
            board.Matrix[moveFromOriginal.Letter, moveFromOriginal.Number] = PiecesForm.Empty;
        }
        if(i == emptyOpponentPawn.Letter && j == emptyOpponentPawn.Number)
        {
            // Captured
            board.Matrix[i, j] = PiecesForm.Empty;
        }
    }
}

