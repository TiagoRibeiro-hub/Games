
namespace Chess;
public static class Funcs
{
    public static (int, int) GetIntegerMove(string move)
    {
        char[] x = move.ToCharArray();
        char xl = x[0];// letter
        char xn = x[1];// nr
        return (ChangeLetterToInt(xl), int.Parse(xn.ToString()));
    }
    private static int ChangeLetterToInt(char xl)
    {
        string value = xl.ToString();
        if (value.Contains("a"))
        {
            return 1;
        }
        if (value.Contains("b"))
        {
            return 2;
        }
        if (value.Contains("c"))
        {
            return 3;
        }
        if (value.Contains("d"))
        {
            return 4;
        }
        if (value.Contains("e"))
        {
            return 5;
        }
        if (value.Contains("f"))
        {
            return 6;
        }
        if (value.Contains("g"))
        {
            return 7;
        }
        if (value.Contains("h"))
        {
            return 8;
        }
        return 0;
    }
    public static string ChangeIntToLetter(int moveInt)
    {
        switch (moveInt)
        {
            case 1: return "a";
            case 2: return "b";
            case 3: return "c";
            case 4: return "d";
            case 5: return "e";
            case 6: return "f";
            case 7: return "g";
            case 8: return "h";
            default: return string.Empty;
        }
    }
    public static string WhichPieceIs(string[,] board, Board moveTo, string colorToBeCaptured)
    {
        bool flag = false;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (flag)
            {
                break;
            }
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (moveTo.Letter == i && moveTo.Number == j)
                {
                    if (board[i, j] == PiecesForm.Pawn + colorToBeCaptured)
                    {
                        return PiecesForm.Pawn + colorToBeCaptured;
                    }
                    else if (board[i, j] == PiecesForm.Tower + colorToBeCaptured)
                    {
                        return PiecesForm.Tower + colorToBeCaptured;
                    }
                    else if (board[i, j] == PiecesForm.Horse + colorToBeCaptured)
                    {
                        return PiecesForm.Horse + colorToBeCaptured;
                    }
                    else if (board[i, j] == PiecesForm.Bishop + colorToBeCaptured)
                    {
                        return PiecesForm.Bishop + colorToBeCaptured;
                    }
                    else if (board[i, j] == PiecesForm.Queen + colorToBeCaptured)
                    {
                        return PiecesForm.Queen + colorToBeCaptured;
                    }
                    else
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return string.Empty;
    }
    public static void CapuredList(Board board, PiecesColor color, Board moveTo)
    {
        string colorToBeCaptured = color.ToString() == PiecesColor.W.ToString() ? PiecesColor.B.ToString() : PiecesColor.W.ToString();

        string capturedPiece = WhichPieceIs(board.Matrix, moveTo, colorToBeCaptured);

        if (!string.IsNullOrEmpty(capturedPiece))
        {
            _ = colorToBeCaptured == PiecesColor.W.ToString() ? board.CapturedBlackPieces.Add(capturedPiece) : board.CapturedWhitePieces.Add(capturedPiece);
        }

    }
}

