
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
                    if (board[i, j] == PiecesForm.Tower + colorToBeCaptured)
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
            if (colorToBeCaptured == PiecesColor.W.ToString())
            {
                board.CapturedWhitePieces.Add(capturedPiece);
            }
            else
            {
                board.CapturedBlackPieces.Add(capturedPiece);
            }
        }
    }
    public static string ChooseFromCapturedList(Board board, PiecesColor color)
    {
        string recoverPiece = string.Empty;
        Console.WriteLine("\n * ********************\n *** CAPTURED LIST ***\n * ********************\n");
        if (color.ToString() == PiecesColor.W.ToString())
        {
            if (board.CapturedWhitePieces.Count > 0)
            {
                recoverPiece = RecoverPiece(board, color.ToString());
            }
        }
        else
        {
            if (board.CapturedBlackPieces.Count > 0)
            {
                recoverPiece = RecoverPiece(board, color.ToString());
            }
        }
        if (string.IsNullOrEmpty(recoverPiece))
        {
            Console.WriteLine("Captured List is Empty");
        }

        return recoverPiece;
    }
    private static string RecoverPiece(Board board, string color)
    {
        Dictionary<int, string> possibleAnswers = new();
        int count = 0;

        if (color == PiecesColor.W.ToString())
        {
            foreach (var item in board.CapturedWhitePieces)
            {
                count += 1;
                possibleAnswers.Add(count, item);
                Console.WriteLine(count.ToString() + " => " + item.ToString());
            }
        }
        else
        {
            foreach (var item in board.CapturedBlackPieces)
            {
                count += 1;
                possibleAnswers.Add(count, item);
                Console.WriteLine(count.ToString() + " => " + item.ToString());
            }
        }
        return AnswerChooseCapturedList(possibleAnswers);
    }
    private static string AnswerChooseCapturedList(Dictionary<int, string> possibleAnswers)
    {
        string res = string.Empty;
        bool ok = false;
        do
        {
            Console.Write("\nChoose a option: ");
            res = Console.ReadLine();
            if (!string.IsNullOrEmpty(res) && res.Length == 1)
            {
                int number;
                bool success = int.TryParse(res, out number);
                if (success)
                {
                    if (possibleAnswers.ContainsKey(number))
                    {
                        ok = true;
                        res = possibleAnswers.GetValueOrDefault(number);
                    }
                }         
            }
            if (ok == false)
            {
                Console.WriteLine("\n ** Choose a number from the list **\n");
            }

        } while (ok == false);

        return res;
    }
    public static string IsCheckBy(Board board, int moveToLetter, int moveToNumber, PiecesColor color, string pieceForm)
    {
        string newColor = NewColor(color);
        string l = Funcs.ChangeIntToLetter(moveToLetter);
        // pieceForm => Tower or Bishop 
        return board.Matrix[moveToLetter, moveToNumber].Contains(pieceForm)
            ? pieceForm + newColor + " => " + l + moveToNumber
            : PiecesForm.Queen + newColor + " => " + l + moveToNumber;
    }

    public static string NewColor(PiecesColor color)
    {   
        return color.ToString() == PiecesColor.W.ToString() ? PiecesColor.B.ToString() : PiecesColor.W.ToString();
    }
}

