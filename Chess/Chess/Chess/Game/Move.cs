namespace Chess;
public class Move : Player
{
    public string MoveFrom { get; set; }
    public string MoveTo { get; set; }
    public List<string> MovesList { get; set; } = GetMovesList();
    public SpecialMove GetSpecialMove { get; set; }


    private readonly ShowConsole showConsole = new();
    private readonly SpecialMove specialMove = new();
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

    // METHODS
    private static string MoveAnswer(string fromOrTo)
    {
        bool ok = false;
        string move = string.Empty;
        do
        {
            Console.Write(fromOrTo + ": ");
            move = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(move) && move.Length == 2)
            {
                ok = true;
            }
        } while (ok == false);
        return move;
    }
    private string FromMove()
    {
        return MoveAnswer("From");
    }
    private string ToMove()
    {
        return MoveAnswer("To");
    }



    public (Move, bool) EnterMove(Board board, Player player)
    {
        Console.WriteLine($"\n{player.Name.ToUpper()} color ({player.PieceColor}) enter your move:");

        string valueFrom;
        valueFrom = FromMove();
        if (string.IsNullOrWhiteSpace(valueFrom))
        {
            return (new Move(), false);
        }
        string valueTo = ToMove();
        if (string.IsNullOrWhiteSpace(valueTo))
        {
            return (new Move(), false);
        }
        if (player.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
        {
            this.PieceColor = PiecesColor.White;
        }
        else
        {
            this.PieceColor = PiecesColor.Black;
        }
        // see if has special move
        Move move = new()
        {
            Name = player.Name,
            MoveFrom = valueFrom,
            MoveTo = valueTo,
            PieceColor = this.PieceColor,
            GetSpecialMove = specialMove.HasSpecialMoves(board, valueFrom, valueTo, player.PieceColor.ToString())
        };
        return (move, true);
    }
    public bool ConfirmMove(Move move)
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