namespace Chess;
#nullable disable
public class Moves : Player
{
    public string MoveFrom { get; set; }
    public string MoveTo { get; set; }
    public List<string> MovesList { get; set; } = GetMovesList();

    private ShowConsole showConsole = new();
    private static List<string> GetMovesList()
    {
        List<string> moves = new();
        for (int i = 1; i < 9; i++)
        {
            moves.Add("a" + i);
            moves.Add("b" + i);
            moves.Add("c" + i);
            moves.Add("d" + i);
            moves.Add("e" + i);
            moves.Add("f" + i);
            moves.Add("g" + i);
            moves.Add("h" + i);
        }
        return moves;
    }
    private string FromMove()
    {
        Console.Write("From: ");
        string fromMove = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fromMove))
        {
            return FromMove();
        }
        if (fromMove.Length != 2)
        {
            showConsole.MoveNotAllowed();
            return FromMove();
        }
        return fromMove;
    }
    private string ToMove()
    {
        Console.Write("To: ");
        string toMove = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(toMove))
        {
            return ToMove();
        }
        if (toMove.Length != 2)
        {
            showConsole.MoveNotAllowed();
            return FromMove();
        }
        return toMove;
    }
    public Moves EnterMove(Player player, string pieceColor)
    {
        Console.WriteLine($"\n{player.Name.ToUpper()} color ({player.PieceColor}) enter your move:");
        if (pieceColor.Contains(PiecesColor.White.ToString()))
        {
            this.PieceColor = PiecesColor.White;
        }
        else
        {
            this.PieceColor = PiecesColor.Black;
        }
        return new Moves()
        {
            MoveFrom = FromMove(),
            MoveTo = ToMove(),
            Name = player.Name,
            PieceColor = this.PieceColor
        };
    }
    public bool ConfirmMove(Moves move)
    {
        int res = 0;
        foreach (var item in MovesList)
        {
            if (move.MoveFrom.ToLower().Contains(item))
            {
                res += 1;
            }
            if (move.MoveTo.ToLower().Contains(item))
            {
                res += 1;
            }
        }
        if (res == 2)
        {
            return true;
        }
        return false;
    }

}