public class Board : Pieces
{
    public string[,] Matrix { get; set; } = new string[9, 9];
    public int Letter { get; set; }
    public int Number { get; set; }

    private const string space = "  ";
    private readonly Player player = new();
    private static (int, int) GetIntegerMove(string move)
    {
        char[] x = move.ToCharArray();
        char x1 = x[0];// letter
        char x2 = x[1];// nr
        return (ChangeLetterToInt(x1), int.Parse(x2.ToString()));
    }
    private static int ChangeLetterToInt(char x1)
    {
        string value = x1.ToString();
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

    public void Display()
    {
        var board = this.Matrix;
        int positionNr = 0;
        int positionLetter = -1;
        char letter = 'a';
        for (int i = 0; i < board.GetLength(0); i++)
        {
            positionLetter += 1;
            if (positionLetter == 9)
            {
                positionLetter = 8;
            }
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == board[0, positionNr])
                {
                    board[i, j] = positionNr.ToString() + space;
                    positionNr += 1;
                    if (positionNr == 9)
                    {
                        positionNr = 8;
                    }
                }
                else if (board[i, j] == board[positionLetter, 0])
                {
                    board[i, j] = letter.ToString() + " ";
                    letter++;
                }
                else if ((i == 1 || i == 8) && (j == 1 || j == 8))
                {
                    if ((i == 1 && j == 1) || (i == 1 && j == 8))
                    {
                        board[i, j] = PiecesForm.Tower + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Tower + PiecesColor.W;
                    }
                }
                else if ((i == 1 || i == 8) && (j == 2 || j == 7))
                {
                    if ((i == 1 && j == 2) || (i == 1 && j == 7))
                    {
                        board[i, j] = PiecesForm.Horse + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Horse + PiecesColor.W;
                    }
                }
                else if ((i == 1 || i == 8) && (j == 3 || j == 6))
                {
                    if ((i == 1 && j == 3) || (i == 1 && j == 6))
                    {
                        board[i, j] = PiecesForm.Bishop + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Bishop + PiecesColor.W;
                    }
                }
                else if ((i == 1 || i == 8) && (j == 4))
                {
                    if ((i == 1 && j == 4))
                    {
                        board[i, j] = PiecesForm.Queen + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Queen + PiecesColor.W;
                    }
                }
                else if ((i == 1 || i == 8) && (j == 5))
                {
                    if (i == 1 && j == 5)
                    {
                        board[i, j] = PiecesForm.King + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.King + PiecesColor.W;
                    }
                }
                else if (i == 2 || i == 7)
                {
                    if (i == 2)
                    {
                        board[i, j] = PiecesForm.Pawn + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Pawn + PiecesColor.W;
                    }
                }
                else
                {
                    board[i, j] = PiecesForm.Empty;
                }

            }
        }
    }
    public void ShowBoard(string[,] board)
    {
        Console.WriteLine();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write($"  {board[i, j]}   ");
            }
            Console.WriteLine("\n");
        }
        Console.WriteLine();
    }
    public Game PlayedBoard(string[,] board, Moves move, Game game)
    {
        Pieces pieces = new();
        if (move.ConfirmMove(move))
        {
            Board moveFrom = new();
            (moveFrom.Letter, moveFrom.Number) = GetIntegerMove(move.MoveFrom);
            Board moveTo = new();
            (moveTo.Letter, moveTo.Number) = GetIntegerMove(move.MoveTo);

            if(moveFrom.Letter != 0 && moveTo.Letter != 0)
            {
                if (move.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
                {
                    game = player.WhitePlayer(board, game, moveFrom, moveTo, pieces);
                }
                else
                {
                    game = player.BlackPlayer(board, game, moveFrom, moveTo, pieces);
                }
            }
            else
            {
                game.ResultPlayedBoard = false;
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
        return game;
    }
}



