﻿namespace Chess;
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
        if (board[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // Move To is the Same Color 
            return true;
        }
        return false;
    }
    private static void IsKingInCheck(Board board, Game game, PiecesColor color, int moveToLetter, int moveToNumber)
    {
        IsCheckByTower isCheckByTower = new();
        //move from => last position
        //move to => +1 -1
        Board moveLastPositionKing = new();
        PiecesColor newColor;
        if (color.ToString() == PiecesColor.W.ToString())
        {
            (moveLastPositionKing.Letter, moveLastPositionKing.Number) = Funcs.GetIntegerMove(board.IsCheck.LastKingPositionBlack);
            newColor = PiecesColor.B;
        }
        else
        {
            (moveLastPositionKing.Letter, moveLastPositionKing.Number) = Funcs.GetIntegerMove(board.IsCheck.LastKingPositionWhite);
            newColor = PiecesColor.W;
        }

        Tower tower = new();
        Board moveToChange = new();
        if (moveLastPositionKing.Number == moveToNumber && moveLastPositionKing.Letter < moveToLetter)
        {
            //KING IS UP
            // MOVE KING DOWN
            moveToChange.Letter = moveLastPositionKing.Letter + 1; // one down
            moveToChange.Number = moveLastPositionKing.Number;
            // tower down
            tower.DownDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
        if (moveLastPositionKing.Number == moveToNumber && moveLastPositionKing.Letter > moveToLetter)
        {
            //KING IS DOWN
            // MOVE KING UP
            moveToChange.Letter = moveLastPositionKing.Letter - 1; // one up
            moveToChange.Number = moveLastPositionKing.Number;
            // tower up
            tower.UpDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
        if (moveLastPositionKing.Letter == moveToLetter && moveLastPositionKing.Number > moveToNumber)
        {
            //KING IS RIGHT
            // MOVE KING LEFT
            moveToChange.Number = moveLastPositionKing.Number - 1; // one left
            moveToChange.Letter = moveLastPositionKing.Letter;
            // tower left
            tower.LeftDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
        if (moveLastPositionKing.Letter == moveToLetter && moveLastPositionKing.Number < moveToNumber)
        {
            //KING IS LEFT
            // MOVE KING RIGHT
            moveToChange.Number = moveLastPositionKing.Number + 1;// one right
            moveToChange.Letter = moveLastPositionKing.Letter;
            // tower right
            tower.RightDirection(board, game, moveLastPositionKing, moveToChange, 0, 0, newColor, false, true);
        }
    }
    private static void TowerMovement(Board board, Game game, int moveToLetter, int moveToNumber, int i, int j, PiecesColor color, bool queen)
    {
        Board moveTo = new() { Letter = moveToLetter, Number = moveToNumber };
        Funcs.CapuredList(board, color, moveTo);
        if (queen == false)
        {
            board.Matrix[moveToLetter, moveToNumber] = PiecesForm.Tower + color.ToString();
        }
        if (queen)
        {
            board.Matrix[moveToLetter, moveToNumber] = PiecesForm.Queen + color.ToString();
        }
        board.Matrix[i, j] = PiecesForm.Empty;
        game.ResultPlayedBoard = true;
        // SEE IF KING IS IN CHECK
        IsKingInCheck(board, game, color, moveToLetter, moveToNumber);
    }

    private static void SetMovement(Board board, int moveToLetter, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen)
    {

        if (MoveNotAllowed(board.Matrix, moveToLetter, moveToNumber, color) == false)
        {
            // Move To is not the Same Color
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.King))
            {
                // Move To is the King           
                game = CheckMate(board.Matrix, game, moveToLetter, moveToNumber);
            }
            TowerMovement(board, game, moveToLetter, moveToNumber, i, j, color, queen);
            game.ResultPlayedBoard = true;
        }
        else
        {
            // Move To has piece the same color on the line
            game.ResultPlayedBoard = false;
        }
    }




    private static bool HorizontalMovement(Board board, Board moveTo, int moveToNumber, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            res = board.IsCheck.CheckPiece(board, moveTo.Letter, moveToNumber, PiecesForm.Tower, color, res);
        }
        else
        {
            if (board.Matrix[moveTo.Letter, moveToNumber] != PiecesForm.Empty)
            {
                SetMovement(board, moveTo.Letter, moveToNumber, color, game, i, j, queen);
            }
            else if (moveToNumber == moveTo.Number)
            {
                TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color, queen);
            }
            else
            {
                res = false;
            }
        }
        return res;
    }
    private static bool VerticalMovement(Board board, Board moveTo, int moveToLetter, PiecesColor color, Game game, int i, int j, bool queen, bool king)
    {
        bool res = true;
        if (king)
        {
            res = board.IsCheck.CheckPiece(board, moveToLetter, moveTo.Number, PiecesForm.Tower, color, res);
        }
        else
        {
            if (board.Matrix[moveToLetter, moveTo.Number] != PiecesForm.Empty)
            {
                SetMovement(board, moveToLetter, moveTo.Number, color, game, i, j, queen);
            }
            else if (moveToLetter == moveTo.Letter)
            {
                TowerMovement(board, game, moveTo.Letter, moveTo.Number, i, j, color, queen);
            }
            else
            {
                res = false;
            }
        }
        return res;
    }

    public void UpDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToLetterLimit;
        if (king)
        {
            moveToLetterLimit = 1;
        }
        else
        {
            moveToLetterLimit = moveTo.Letter;
        }
        for (int t = moveFrom.Letter; t > moveToLetterLimit; t--)
        {
            int moveToLetter = t - 1;
            //if (moveToLetter == 0)
            //{
            //    moveToLetter += 1;
            //}
            bool res = VerticalMovement(board, moveTo, moveToLetter, color, game, i, j, queen, king);
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
    public void DownDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToLetterLimit;
        if (king)
        {
            moveToLetterLimit = 8;
        }
        else
        {
            moveToLetterLimit = moveTo.Letter;
        }
        for (int t = moveFrom.Letter; t < moveToLetterLimit; t++)
        {
            int moveToLetter = t + 1;
            //if (moveToLetter == 9)
            //{
            //    moveToLetter -= 1;
            //}
            bool res = VerticalMovement(board, moveTo, moveToLetter, color, game, i, j, queen, king);

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

    public void RightDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToNumberLimit;
        if (king)
        {
            moveToNumberLimit = 8;
        }
        else
        {
            moveToNumberLimit = moveTo.Number;
        }
        for (int t = moveFrom.Number; t < moveToNumberLimit; t++)
        {
            int moveToNumber = t + 1;
            //if (moveToNumber == 9)
            //{
            //    moveToNumber -= 1;
            //}
            bool res = HorizontalMovement(board, moveTo, moveToNumber, color, game, i, j, queen, king);
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
    public void LeftDirection(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen, bool king)
    {
        int moveToNumberLimit;
        if (king)
        {
            moveToNumberLimit = 1;
        }
        else
        {
            moveToNumberLimit = moveTo.Number;
        }
        for (int t = moveFrom.Number; t > moveToNumberLimit; t--)
        {
            int moveToNumber = t - 1;
            //if (moveToNumber == 0)
            //{
            //    moveToNumber += 1;
            //}
            bool res = HorizontalMovement(board, moveTo, moveToNumber, color, game, i, j, queen, king);
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



    public void TowerPossibleMoves(Board board, Game game, Board moveFrom, Board moveTo, int i, int j, PiecesColor color, bool queen)
    {
        if (AcceptedTowerMovements(moveFrom, moveTo))
        {
            if (moveFrom.Letter == moveTo.Letter)
            {
                // HORIZONTAL MOVEMENT
                if (moveFrom.Number < moveTo.Number)
                {
                    // Right Direction
                    RightDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
                else
                {
                    // Left Direction
                    LeftDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
            }
            else if (moveFrom.Number == moveTo.Number)
            {
                // VERTICAL MOVEMENT
                if (moveFrom.Letter < moveTo.Letter)
                {
                    // Down Direction
                    DownDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
                else
                {
                    // Up Direction
                    UpDirection(board, game, moveFrom, moveTo, i, j, color, queen, false);
                }
            }
        }
        else
        {
            game.ResultPlayedBoard = false;
        }
    }
}


