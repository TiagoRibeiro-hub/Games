namespace Chess;
#nullable disable
public class SpecialMoves : Player
{
    public string SpecialMovesName { get; set; }
    public bool NormalMove { get; set; }
    public bool IsPossible { get; set; }

    public SpecialMoves HasSpecialMoves(string MoveFrom)
    {
        return new SpecialMoves()
        {
            Name = "",
            IsPossible = false,
        };
    }
    public bool EnPassant(string MoveFrom)
    {
        return false;
    }
    public bool Castling(string MoveFrom)
    {
        return false;
    }
}
