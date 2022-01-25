﻿namespace Chess;
#nullable disable
public class Check
{
    public bool IsCheck { get; set; } = false;
    public string ByPiece { get; set; }

    public string LastKingPositionWhite { get; set; } = "h5"; 
    public string LastKingPositionBlack { get; set; } = "a5";


    public bool CheckPiece(Board board, int moveToLetter, int moveToNumber, string piecesForm, PiecesColor color, bool stopSearch, bool tower, bool bishop, bool pawn, bool horse)
    {
        board.IsCheck.IsCheck = false;
        string newColor = Funcs.NewColor(color);
        if (board.Matrix[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // same color is protected
            stopSearch = true;
        }
        else if (tower)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Pawn + newColor) ||
            bishop && board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Horse + newColor)
            || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Bishop + newColor))
            {
                // pawn & horse & bishop cant capture with front movement or lateral movement PROTECTING FROM TOWER
                stopSearch = true;
            }
            if (board.Matrix[moveToLetter, moveToNumber].Contains(piecesForm + newColor) || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Queen + newColor))
            {
                board.IsCheck.IsCheck = true;
                board.IsCheck.ByPiece = IsCheckBy(board, moveToLetter, moveToNumber, newColor, piecesForm);
                stopSearch = true;
            }
        }
        else if (bishop)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Horse + newColor) ||
            board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Pawn + newColor) ||
            board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Tower + newColor))
            {
                // pawn & horse & tower cant capture with diagonal movement PROTECTING FROM BISHOP
                stopSearch = true;
            }
            if (board.Matrix[moveToLetter, moveToNumber].Contains(piecesForm + newColor) || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Queen + newColor))
            {
                board.IsCheck.IsCheck = true;
                board.IsCheck.ByPiece = IsCheckBy(board, moveToLetter, moveToNumber, newColor, piecesForm);
                stopSearch = true;
            }
        }
        else if (pawn)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Pawn + newColor))
            {
                board.IsCheck.IsCheck = true;
                board.IsCheck.ByPiece = IsCheckBy(board, moveToLetter, moveToNumber, newColor, PiecesForm.Pawn);
                stopSearch = true;
            }
        }
        else if (horse)
        {
            if (board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Horse + newColor))
            {
                board.IsCheck.IsCheck = true;
                board.IsCheck.ByPiece = IsCheckBy(board, moveToLetter, moveToNumber, newColor, PiecesForm.Horse);
                stopSearch = true;
            }
        }
        else
        {
            stopSearch = false;
        }
        return stopSearch;
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

}

