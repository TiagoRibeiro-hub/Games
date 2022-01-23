namespace Games.GameType;
public class ChessMoves : Moves
{
    // Horizontal
    public bool HorizontalUpMove() { return false; }
    public bool HorizontalDownMove() { return false; }

    // Vertical
    public bool VerticalUpMove() { return false; }
    public bool VerticalDownMove() { return false; }

    // Diagonal Up
    public bool DiagonalUpRightMove() { return false; }
    public bool DiagonalUpLeftMove() { return false; }

    // Diagonal Down
    public bool DiagonalDownRightMove() { return false; }
    public bool DiagonalDownLeftMove() { return false; }

}

