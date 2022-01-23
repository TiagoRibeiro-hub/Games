namespace Chess.Board;
#nullable disable
public abstract class Board
{
    public string[,] Matrix { get; set; }

    public abstract string[,] SetMatrix();
}

