namespace Chess;
#nullable disable
public class Tower : Pieces
{
    public bool AcceptedTowerMovements(Board moveFrom, Board moveTo)
    {
        return (moveFrom.Letter == moveTo.Letter && (moveFrom.Number > moveTo.Number || moveFrom.Number < moveTo.Number)) ||
               (moveFrom.Number == moveTo.Number && (moveFrom.Letter > moveTo.Letter || moveFrom.Letter < moveTo.Letter));
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
    private static void TowerMovement(string[,] board, Game game, int moveToLetter, int moveToNumber, int i, int j, PiecesColor color)
    {
        board[moveToLetter, moveToNumber] = PiecesForm.Tower + color.ToString();
        board[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
    }
    private static void SetMovement(string[,] board, int moveToLetter, int moveToNumber, PiecesColor color, Game game, int i, int j)
    {
        if (MoveNotAllowed(board, moveToLetter, moveToNumber, color))
        {
            // Move To has piece on the line
            game.ResultPlayedBoard = false;
        }
        if (board[moveToLetter, moveToNumber].Contains(PiecesForm.King))
        {
            // Move To is the King
            ShowWinner(game);
        }
        else
        {
            TowerMovement(board, game, moveToLetter, moveToNumber, i, j, color);
        }
    }

    public void TowerPossibleMoves(string[,] board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color)
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
                        if (board[moveTo.Letter, moveToNumber] != PiecesForm.Empty)
                        {
                            SetMovement(board, moveTo.Letter, moveToNumber, color, game, i, j);
                            break;
                        }
                        else if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.Empty))
                        {
                            TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color);
                            break;
                        }
                    }
                }
                else
                {
                    // TEST
                    // Left Direction
                    for (int t = moveFrom.Number; t <= moveTo.Number; t++)
                    {
                        int moveToNumber = t - 1;
                        if (moveToNumber == 0)
                        {
                            moveToNumber += 1;
                        }
                        if (board[moveTo.Letter, moveToNumber] != PiecesForm.Empty)
                        {
                            SetMovement(board, moveTo.Letter, moveToNumber, color, game, i, j);
                            break;
                        }
                        else if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.Empty))
                        {
                            TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color);
                            break;
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
                        if (board[moveToLetter, moveTo.Number] != PiecesForm.Empty)
                        {
                            SetMovement(board, moveToLetter, moveTo.Number, color, game, i, j);
                            break;
                        }
                        else if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.Empty))
                        {
                            TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color);
                            break;
                        }
                    }
                }
                else
                {
                    // Up Direction
                    for (int t = moveFrom.Letter; t <= moveFrom.Letter; t++)
                    {
                        int moveToLetter = t - 1;
                        if (moveToLetter == 0)
                        {
                            moveToLetter += 1;
                        }
                        if (board[moveToLetter, moveTo.Number] != PiecesForm.Empty)
                        {
                            SetMovement(board, moveToLetter, moveTo.Number, color, game, i, j);
                            break;
                        }
                        else if (board[moveTo.Letter, moveTo.Number].Contains(PiecesForm.Empty))
                        {
                            TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color);
                            break;
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


