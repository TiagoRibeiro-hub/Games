namespace Chess;
#nullable disable
public class Tower : Pieces
{
    public bool AcceptedTowerMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter == moveTo.Letter && (moveFrom.Number > moveTo.Number || moveFrom.Number < moveTo.Number)) ||
               (moveFrom.Number == moveTo.Number && (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter));
    }
    private static void TowerMovement(Board board, Game game, int moveToLetter, int moveToNumber, int i, int j, PiecesColor color)
    {
        Board moveTo = new() { Letter = moveToLetter, Number = moveToNumber };
        Funcs.CapuredList(board, color, moveTo);
        board.Matrix[moveToLetter, moveToNumber] = PiecesForm.Tower + color.ToString();
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }

    private static bool MoveNotAllowed(string[,] board, int moveToLetter, int moveToNumber, PiecesColor color)
    {
        if (board[moveToLetter, moveToNumber].Contains(color.ToString()))
        {
            // Move To is the Same Color 
            return true;
        }
        return false;
    }
    private static void SetMovement(Board board, int moveToLetter, int moveToNumber, PiecesColor color, Game game, int i, int j)
    {

        if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color))
        {
            // Move To has piece the same color on the line
            game.ResultPlayedBoard = false;
        }
        else if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
        {
            // Move To is the King           
            game = CheckMate(board.Matrix, game, moveToLetter, moveToNumber);
            if (game.ResultPlayedBoard)
            {
                TowerMovement(board, game, moveToLetter, moveToNumber, i, j, color);
            }
        }
        else
        {
            TowerMovement(board, game, moveToLetter, moveToNumber, i, j, color);
        }
    }
    private static bool HorizontalMovement(Board board, Board moveTo, int moveToNumber, PiecesColor color, Game game, int i, int j)
    {
        bool res = true;
        if (board.Matrix[moveTo.Letter, moveToNumber] != PiecesForm.Empty)
        {
            SetMovement(board, moveTo.Letter, moveToNumber, color, game, i, j);
        }
        else if (moveToNumber == moveTo.Number)
        {
            TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color);
        }
        else
        {
            res = false;
        }
        return res;
    }
    private static bool VerticalMovement(Board board, Board moveTo, int moveToLetter, PiecesColor color, Game game, int i, int j)
    {
        bool res = true;
        if (board.Matrix[moveToLetter, moveTo.Number] != PiecesForm.Empty)
        {
            SetMovement(board, moveToLetter, moveTo.Number, color, game, i, j);
        }
        else if (moveToLetter == moveTo.Letter)
        {
            TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color);
        }
        else
        {
            res = false;
        }
        return res;
    }

    public void TowerPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
    {
        if (AcceptedTowerMovements(moveFrom, moveTo))
        {
            if (moveFrom.Letter == moveTo.Letter)
            {
                // HORIZONTAL MOVEMENT
                if (moveFrom.Number < moveTo.Number)
                {
                    // Right Direction
                    for (int t = moveFrom.Number; t <= moveTo.Number; t++)
                    {
                        int moveToNumber = t + 1;
                        if (moveToNumber == 9)
                        {
                            moveToNumber -= 1;
                        }
                        bool res = HorizontalMovement(board, moveTo, moveToNumber, color, game, i, j);
                        if (res)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    // Left Direction
                    for (int t = moveFrom.Number; t >= moveTo.Number; t--)
                    {
                        int moveToNumber = t - 1;
                        if (moveToNumber == 0)
                        {
                            moveToNumber += 1;
                        }
                        bool res = HorizontalMovement(board, moveTo, moveToNumber, color, game, i, j);
                        if (res)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else if (moveFrom.Number == moveTo.Number)
            {
                // VERTICAL MOVEMENT
                if (moveFrom.Letter < moveTo.Letter)
                {
                    // Down Direction
                    for (int t = moveFrom.Letter; t <= moveTo.Letter; t++)
                    {
                        int moveToLetter = t + 1;
                        if (moveToLetter == 9)
                        {
                            moveToLetter -= 1;
                        }
                        bool res = VerticalMovement(board, moveTo, moveToLetter, color, game, i, j);
                        if (res)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    // Up Direction
                    for (int t = moveFrom.Letter; t >= moveTo.Letter; t--)
                    {
                        int moveToLetter = t - 1;
                        if (moveToLetter == 0)
                        {
                            moveToLetter += 1;
                        }
                        bool res = VerticalMovement(board, moveTo, moveToLetter, color, game, i, j);
                        if (res)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }


}


