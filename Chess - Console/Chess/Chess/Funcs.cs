
namespace Chess;
#nullable disable
public static class Funcs
{
    public static (int, int) GetIntegerMove(string move)
    {
        char[] x = move.ToCharArray();
        char xl = x[0];// letter
        char xn = x[1];// nr
        return (ChangeLetterToInt(xl), int.Parse(xn.ToString()));
    }
    private static int ChangeLetterToInt(char value)
    {
        return value switch
        {
            'a' => 1,
            'b' => 2,
            'c' => 3,
            'd' => 4,
            'e' => 5,
            'f' => 6,
            'g' => 7,
            'h' => 8,
              _ => 0
        };
    }
    public static string ChangeIntToLetter(int moveInt)
    {
        return moveInt switch
        {
            1 => "a",
            2 => "b",
            3 => "c",
            4 => "d",
            5 => "e",
            6 => "f",
            7 => "g",
            8 => "h",
            _ => string.Empty,
        };
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
                    if (board[i, j] == PiecesForm.Rook + colorToBeCaptured)
                    {
                        return PiecesForm.Rook + colorToBeCaptured;
                    }
                    else if (board[i, j] == PiecesForm.Knight + colorToBeCaptured)
                    {
                        return PiecesForm.Knight + colorToBeCaptured;
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
        string res;
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


    public static string NewColor(PiecesColor color)
    {   
        return color.ToString() == PiecesColor.W.ToString() ? PiecesColor.B.ToString() : PiecesColor.W.ToString();
    }
}

