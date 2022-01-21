namespace Chess;
#nullable disable
public class Board : Pieces
{
    public string[,] Matrix { get; set; } = new string[9, 9];
    public int Letter { get; set; }
    public int Number { get; set; }

    public Check IsCheck { get; set; }

    public List<string> AllowedEnPassantListPawn { get; set; } = new();
    public List<string> CapturedWhitePieces { get; set; } = new();
    public List<string> CapturedBlackPieces { get; set; } = new();



    private const string space = "  ";
    private readonly Player player = new();
    

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
                        board[i, j] = PiecesForm.Empty;//PiecesForm.Horse + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Empty;//PiecesForm.Horse + PiecesColor.W;
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
                        board[i, j] = PiecesForm.Empty; //PiecesForm.Pawn + PiecesColor.B;
                    }
                    else
                    {
                        board[i, j] = PiecesForm.Empty; //PiecesForm.Pawn + PiecesColor.W;
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
    public Game PlayedBoard(Board board, Move move, Game game)
    {
        Pieces pieces = new();
        if (move.ConfirmMove(move))
        {
            Board moveFrom = new();
            (moveFrom.Letter, moveFrom.Number) = Funcs.GetIntegerMove(move.MoveFrom);
            Board moveTo = new();
            (moveTo.Letter, moveTo.Number) = Funcs.GetIntegerMove(move.MoveTo);

            if (moveFrom.Letter != 0 && moveTo.Letter != 0)
            {
                if (move.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
                {
                    game = player.WhitePlayer(board, move, game, moveFrom, moveTo, pieces);
                }
                else
                {
                    game = player.BlackPlayer(board, move, game, moveFrom, moveTo, pieces);
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

    public void PlaySpecialMove(Board board, Move move)
    {
        Board moveFirstPiece = new();
        (moveFirstPiece.Letter, moveFirstPiece.Number) = Funcs.GetIntegerMove(move.GetSpecialMove.SpecialMovementFirstPieceTo);
        Board moveSecondPiece = new();
        (moveSecondPiece.Letter, moveSecondPiece.Number) = Funcs.GetIntegerMove(move.GetSpecialMove.SpecialMovementSecondPieceTo);
        string pieceColor = string.Empty;

        // Castling
        Castling castling = new();
        int moveKingEmptyNumber = 0, moveTowerEmptyNumber = 0, moveEmptyLetter = 0;

        // EnPassant
        EnPassant enPassant = new();
        Board movePlayerPawn = new(), emptyOpponentPawn = new(), moveFromOriginal = new();
        if (move.GetSpecialMove.SpecialMoveName == SpecialMovesName.Castling)
        {
            // Castling
            pieceColor = CastlingPrep(move, pieceColor, out moveKingEmptyNumber, out moveTowerEmptyNumber, out moveEmptyLetter);
        }
        else if (move.GetSpecialMove.SpecialMoveName == SpecialMovesName.EnPassant)
        {
            // EnPassant
            pieceColor = EnPassantPrep(move, pieceColor, out moveFromOriginal, out movePlayerPawn, out emptyOpponentPawn);
        }



        for (int i = 0; i < board.Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < board.Matrix.GetLength(1); j++)
            {
                if (move.GetSpecialMove.SpecialMoveName == SpecialMovesName.Castling)
                {
                    castling.CastlingMovement(board.Matrix, moveFirstPiece, moveSecondPiece, pieceColor, moveKingEmptyNumber, moveTowerEmptyNumber, moveEmptyLetter, i, j);
                }
                if (move.GetSpecialMove.SpecialMoveName == SpecialMovesName.EnPassant)
                {
                    enPassant.EnPassantMovement(board, moveFromOriginal, movePlayerPawn, emptyOpponentPawn, pieceColor, i, j);
                }
            }
        }
    }

    private static string EnPassantPrep(Move move, string pieceColor, out Board moveFromOriginal, out Board movePlayerPawn, out Board emptyOpponentPawn)
    {
        moveFromOriginal = new();
        (moveFromOriginal.Letter, moveFromOriginal.Number) = Funcs.GetIntegerMove(move.MoveFrom);
        movePlayerPawn = new();
        (movePlayerPawn.Letter, movePlayerPawn.Number) = Funcs.GetIntegerMove(move.GetSpecialMove.SpecialMovementFirstPieceTo);
        emptyOpponentPawn = new();
        (emptyOpponentPawn.Letter, emptyOpponentPawn.Number) = Funcs.GetIntegerMove(move.GetSpecialMove.SpecialMovementSecondPieceTo);
        // letter W or B
        if (move.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
        {
            pieceColor = PiecesColor.W.ToString();
        }
        else
        {
            pieceColor = PiecesColor.B.ToString();         
        }
        return pieceColor;
    }
    private static string CastlingPrep(Move move, string pieceColor, out int moveKingEmptyNumber, out int moveTowerEmptyNumber, out int moveEmptyLetter)
    {
        // King
        moveKingEmptyNumber = 5;
        // Tower
        moveTowerEmptyNumber = 0;
        // letter W or B
        moveEmptyLetter = 0;

        // Tower
        if (move.GetSpecialMove.CastlingType.ToString() == CastlingType.threeZeros.ToString())
        {
            moveTowerEmptyNumber = 1;
        }
        else
        {
            moveTowerEmptyNumber = 8;
        }
        // letter W or B
        if (move.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
        {
            pieceColor = PiecesColor.W.ToString();
            moveEmptyLetter = 8;
        }
        else
        {
            pieceColor = PiecesColor.B.ToString();
            moveEmptyLetter = 1;
        }
        return pieceColor;
    }
}



