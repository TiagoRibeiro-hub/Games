namespace Chess.Board;
#nullable disable
public class ChessBoard : Board
{
    public string[,] CurrentGame { get; set; } 
    public string[,] TestGame { get; set; } 

    public List<string> CapturedWhitePieces { get; set; } = new();
    public List<string> CapturedBlackPieces { get; set; } = new();


    public override string[,] SetMatrix()
    {
        return Matrix = new string[9,9];
    }
}

