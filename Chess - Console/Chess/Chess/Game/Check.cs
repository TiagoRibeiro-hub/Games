namespace Chess;
#nullable disable
public class Check
{
    public bool IsCheck { get; set; } = false;
    public string ByPiece { get; set; }

    public string LastKingPositionWhite { get; set; } = "h5";
    public string LastKingPositionBlack { get; set; } = "a5";


    public bool CheckPiece(Board board, int moveToLetter, int moveToNumber, string piecesForm, PiecesColor color, bool stopSearch)
    {
        board.IsCheck.IsCheck = false;
        string newColor = Funcs.NewColor(color);
        if (board.Matrix[moveToLetter, moveToNumber].Contains("." + color.ToString()))
        {
            // same color is protected
            stopSearch = true;
        }
        else if (board.Matrix[moveToLetter, moveToNumber].Contains(piecesForm + newColor) || board.Matrix[moveToLetter, moveToNumber].Contains(PiecesForm.Queen + newColor))
        {
            board.IsCheck.IsCheck = true;
            board.IsCheck.ByPiece = IsCheckBy(board, moveToLetter, moveToNumber, color, PiecesForm.Tower);
            stopSearch = true;
        }
        else
        {
            stopSearch = false;
        }
        return stopSearch;
    }

    private static string IsCheckBy(Board board, int moveToLetter, int moveToNumber, PiecesColor color, string pieceForm)
    {
        string newColor = Funcs.NewColor(color);
        string l = Funcs.ChangeIntToLetter(moveToLetter);
        // pieceForm => Tower or Bishop 
        return board.Matrix[moveToLetter, moveToNumber].Contains(pieceForm)
            ? pieceForm + newColor + " => " + l + moveToNumber
            : PiecesForm.Queen + newColor + " => " + l + moveToNumber;
    }
}

