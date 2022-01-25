namespace Chess;
#nullable disable
public class Check
{
    public string ByPiece { get; set; }
    public Dictionary<string, string> SideCheckedConfirmationDict { get; set; }
    public string SideToCheck { get; set; }

    public bool IsCheckMate { get; set; } = false;
    public string LastKingPositionWhite { get; set; } = "h5";
    public string LastKingPositionBlack { get; set; } = "a5";


    //public Dictionary<string, string> SetDictionaryKey()
    //{
    //    Dictionary<string, string> dict = new();
    //    dict.Add(SideToCheckOpt.Up, string.Empty);
    //    dict.Add(SideToCheckOpt.DiagonalUpRight, string.Empty);
    //    dict.Add(SideToCheckOpt.DiagonalUpLeft, string.Empty);
    //    dict.Add(SideToCheckOpt.Down, string.Empty);
    //    dict.Add(SideToCheckOpt.DiagonalDownRight, string.Empty);
    //    dict.Add(SideToCheckOpt.DiagonalDownLeft, string.Empty);
    //    dict.Add(SideToCheckOpt.Right, string.Empty);
    //    dict.Add(SideToCheckOpt.Left, string.Empty);

    //    return dict;
    //}
    public bool CheckPiece(Board board, int moveToLetter, int moveToNumber, string piecesForm, PiecesColor color, bool stopSearch, bool tower, bool bishop, bool pawn, bool horse)
    {
        string newColor = Funcs.NewColor(color);
        if (board.Matrix[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // same color is protected
            stopSearch = true;
        }
        else if (tower)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Pawn + newColor) ||
            bishop && board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Knight + newColor)
            || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Bishop + newColor))
            {
                // pawn & horse & bishop cant capture with front movement or lateral movement PROTECTING FROM TOWER
                stopSearch = true;
            }
            if (board.Matrix[moveToLetter, moveToNumber].Contains(piecesForm + newColor) || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Queen + newColor))
            {
                Checked(board, moveToLetter, moveToNumber, piecesForm, newColor);
                stopSearch = true;
            }
        }
        else if (bishop)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Knight + newColor) ||
            board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Pawn + newColor) ||
            board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Rook + newColor))
            {
                // pawn & horse & tower cant capture with diagonal movement PROTECTING FROM BISHOP
                stopSearch = true;
            }
            if (board.Matrix[moveToLetter, moveToNumber].Contains(piecesForm + newColor) || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Queen + newColor))
            {
                Checked(board, moveToLetter, moveToNumber, piecesForm, newColor);
                stopSearch = true;
            }
        }
        else if (pawn)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Pawn + newColor))
            {
                Checked(board, moveToLetter, moveToNumber, PiecesForm.Pawn, newColor);
            }
        }
        else if (horse)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Knight + newColor))
            {
                Checked(board, moveToLetter, moveToNumber, PiecesForm.Knight, newColor);
            }
        }
        else
        {
            stopSearch = false;
        }
        return stopSearch;
    }

    public void Checked(Board board, int moveToLetter, int moveToNumber, string piecesForm, string newColor)
    {
        board.IsCheck.ByPiece = IsCheckBy(board, moveToLetter, moveToNumber, newColor, piecesForm);

        _ = board.IsCheck.SideCheckedConfirmationDict.TryAdd(board.IsCheck.SideToCheck, board.IsCheck.ByPiece);
    }
    public string IsCheckBy(Board board, int moveToLetter, int moveToNumber, string color, string pieceForm)
    {
        string l = Funcs.ChangeIntToLetter(moveToLetter);
        // pieceForm => Tower or Bishop 
        return board.Matrix[moveToLetter, moveToNumber].Contains(pieceForm)
            ? pieceForm + color + " => " + l + moveToNumber
            : PiecesForm.Queen + color + " => " + l + moveToNumber;
    }
    public void ResetMoves(Board originalMoveFrom, Board moveFromChange, Board originalMoveTo, Board moveToChange)
    {
        moveFromChange.Letter = originalMoveFrom.Letter;
        moveFromChange.Number = originalMoveFrom.Number;
        moveToChange.Letter = originalMoveTo.Letter;
        moveToChange.Number = originalMoveTo.Number;
    }

    public void IsCheckMateMethod(Board board, Board moveTo)
    {
        if (moveTo.Number == 1 && (moveTo.Letter >= 2 && moveTo.Letter <= 7))
        {
            //UP , DOWN , RIGHT , DIAGONAL UP RIGHT , DIAGONAL DOWN RIGHT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Up)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Down)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpRight)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownRight)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Right)
                {
                    count += 1;
                }
            }
            if(count == 5)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Number == 8 && (moveTo.Letter >= 2 && moveTo.Letter <= 7))
        {
            //UP , DOWN , LEFT , DIAGONAL UP LEFT , DIAGONAL DOWN LEFT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Up)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Down)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpLeft)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownLeft)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Left)
                {
                    count += 1;
                }
            }
            if (count == 5)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Letter == 1 && (moveTo.Number >= 2 && moveTo.Number <= 7))
        {
            //DOWN , RIGHT , LEFT , DIAGONAL DOWN RIGHT , DIAGONAL DOWN LEFT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Left)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Right)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownRight)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownLeft)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Down)
                {
                    count += 1;
                }
            }
            if (count == 5)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Letter == 8 && (moveTo.Number >= 2 && moveTo.Number <= 7))
        {
            //UP , RIGHT , LEFT , DIAGONAL UP LEFT , DIAGONAL UP RIGHT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Left)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Right)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpRight)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpLeft)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Up)
                {
                    count += 1;
                }
            }
            if (count == 5)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if ((moveTo.Letter >= 2 && moveTo.Letter <= 7) && (moveTo.Number >= 2 && moveTo.Number <= 7))
        {
            // UP , DOWN , RIGHT , LEFT
            // DIAGONAL DOWN RIGHT , DIAGONAL DOWN LEFT
            // DIAGONAL UP LEFT , DIAGONAL UP RIGHT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Left)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Right)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Down)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Up)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownRight)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownLeft)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpRight)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpLeft)
                {
                    count += 1;
                }

            }
            if (count == 8)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Number == 1 && moveTo.Letter == 1)
        {
            // DOWN , RIGHT ,  DIAGONAL UP RIGHT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Down)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Right)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpRight)
                {
                    count += 1;
                }
            }
            if (count == 3)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Number == 8 && moveTo.Letter == 1)
        {
            // DOWN , LEFT , DIAGONAL DOWN LEFT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Down)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Left)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalDownLeft)
                {
                    count += 1;
                }
            }
            if (count == 3)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Number == 1 && moveTo.Letter == 8)
        {
            // UP , RIGHT , DIAGONAL UP RIGHT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Up)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Right)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpRight)
                {
                    count += 1;
                }
            }
            if (count == 3)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
        if (moveTo.Number == 8 && moveTo.Letter == 8)
        {
            // UP , LEFT , DIAGONAL UP LEFT
            int count = 0;
            foreach (var item in board.IsCheck.SideCheckedConfirmationDict)
            {
                if (item.Key == SideToCheckOpt.Up)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.Left)
                {
                    count += 1;
                }
                if (item.Key == SideToCheckOpt.DiagonalUpLeft)
                {
                    count += 1;
                }
            }
            if (count == 3)
            {
                board.IsCheck.IsCheckMate = true;
            }
        }
    }

}

