namespace Chess;
#nullable disable
public class Pawn : Pieces
{
    public List<int> FirstMoveWhite { get; set; } = new List<int>();
    public List<int> FirstMoveBlack { get; set; } = new List<int>();

    private List<int> GetFirstMoveList(List<int> list, int color)
    {
        for (int i = 1; i <= 8; i++)
        {
            list.Add(color + i);
        }
        return list;
    }
    private static bool ConfirmIfIsFirstMove(int value, bool res, List<int> list)
    {
        foreach (var item in list)
        {
            if (item == value)
            {
                res = true;
                break;
            }
        }
        return res;
    }
    private bool IsPawnFirstMove(PiecesColor color, Board moveFrom)
    {
        int value = moveFrom.Letter + moveFrom.Number;
        bool res = false;
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            // White Pawn List
            this.FirstMoveWhite = GetFirstMoveList(this.FirstMoveWhite, 7);
            res = ConfirmIfIsFirstMove(value, res, this.FirstMoveWhite);
        }
        else
        {
            // Black Pawn List
            this.FirstMoveBlack = GetFirstMoveList(this.FirstMoveBlack, 2);
            res = ConfirmIfIsFirstMove(value, res, this.FirstMoveBlack);
        }
        return res;
    }

    private static void PawnMovement(string[,] board, Move move, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if((moveTo.Letter == moveFrom.Letter - 2) || (moveTo.Letter == moveFrom.Letter + 2))
        {
            // the house where the pawn is will be inserted in "board.AllowedEnPassantListPawn"
            move.AllowedEnPassantPawn = move.MoveTo;
        }
        else
        {
            move.AllowedEnPassantPawn = string.Empty;
        }
        board[moveTo.Letter, moveTo.Number] = PiecesForm.Pawn + color.ToString();
        board[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    public void PawnPossibleMoves(string[,] board, Move move, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        int letterFromOptOne;
        int letterFromOptTwo = 0;
        bool flag = false;
        if (IsPawnFirstMove(color, moveFrom))
        {
            flag = true;
        }
        if (color.ToString().Contains(PiecesColor.W.ToString()))
        {
            if (flag)
            {
                letterFromOptTwo = moveFrom.Letter - 2;
            }
            letterFromOptOne = moveFrom.Letter - 1;
        }
        else
        {
            if (flag)
            {
                letterFromOptTwo = moveFrom.Letter + 2;
            }
            letterFromOptOne = moveFrom.Letter + 1;
        }

        if ((letterFromOptOne == moveTo.Letter || letterFromOptTwo == moveTo.Letter) && (moveFrom.Number == moveTo.Number || moveFrom.Number + 1 == moveTo.Number || moveFrom.Number - 1 == moveTo.Number))
        {
            if (moveFrom.Number + 1 == moveTo.Number || moveFrom.Number - 1 == moveTo.Number)
            {
                // DIAGONAL MOVEMENT TO CAPTURE
                if (board[moveTo.Letter, moveTo.Number].Contains(color.ToString()) ||
                    board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.Empty))
                {
                    // Move To is the Same Color 
                    game.ResultPlayedBoard = false;
                }
                else
                {
                    // Move To is not the Same Color 
                    game = CheckMate(board, game, moveTo.Letter, moveTo.Number);
                    PawnMovement(board, move, game, moveFrom, moveTo, i, j, color);
                }
            }
            else
            {
                // FRONT MOVEMENT
                if (board[moveTo.Letter, moveTo.Number] != PiecesForm.Empty)
                {
                    // Move To has a Piece in front 
                    game.ResultPlayedBoard = false;
                }
                else
                {
                    PawnMovement(board, move, game, moveFrom, moveTo, i, j, color);
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }

    }
}
