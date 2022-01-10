namespace Chess;
#nullable disable
public class ShowConsole
{
    public void AskPlayerName()
    {
        Console.Write("\nPlayer Name: ");
    }
    public string AskHeadsOrTailsFirstPlayer()
    {
        Console.Write($"\n{Coin.Heads}(h) Or {Coin.Tails}(t)? ");
        string res = Console.ReadLine();
        if (res.ToLower().Contains("h"))
        {
            return Coin.Heads;
        }
        if (res.ToLower().Contains("t"))
        {
            return Coin.Tails;
        }
        Console.Clear();
        Console.WriteLine("\n**** Choose (H) or (T) ****");
        return AskHeadsOrTailsFirstPlayer();
    }
    public string AskHeadsOrTailsSecondPlayer(string player)
    {
        if (player == Coin.Heads)
        {
            return "Tails";
        }
        return Coin.Heads;
    }
    public string AskToChooseColor(string name)
    {
        Console.Write($"\n{name.ToUpper()} choose first: {PiecesColor.White} (w) or {PiecesColor.Black} (b): ");
        string x = Console.ReadLine();
        if (x.ToLower().Contains("w") || x.ToLower().Contains("b"))
        {
            return x;
        }
        Console.Clear();
        Console.WriteLine("\n**** Choose (W) or (B) ****");
        return AskToChooseColor(name);
    }
    public string ShowWhoStarts(Player player1, Player player2)
    {
        if (player1.PieceColor == PiecesColor.White)
        {
            Console.WriteLine($"\n   {player1.Name.ToUpper()} goes first");
            return player1.Name;
        }
        Console.WriteLine($"\n   {player2.Name.ToUpper()} goes first");
        return player2.Name;
    }
    public void MoveNotAllowed()
    {
        Console.WriteLine("\n*** Move Not Allowed ***\n");
    }

    public bool ShowSpecialMove(Move move)
    {
        Console.Write($"\n*********************\n*** SPECIAL MOVE ***\n*********************" +
                $"\n\nIs possible to {move.SpecialMoveName.ToUpper()}:");

        if (move.SpecialMoveName == SpecialMovesName.Castling)
        {
            Console.Write($"\nTower moves to => {move.SpecialMovementFirstPieceTo}" +
            $"\nand King moves to => {move.SpecialMovementSecondPieceTo}");
        }
        if (move.SpecialMoveName == SpecialMovesName.EnPassant)
        {
            Console.Write($"\nYour Pawn moves to => {move.SpecialMovementFirstPieceTo}" +
            $"\nand capture Pawn from => {move.SpecialMovementSecondPieceTo}");
        }
        
        bool res = false;
        bool isOk = false;
        do
        {
            (res, isOk) = Answer(res, isOk);
        } while (res == false);

        return isOk;
    }
    public (bool, bool) Answer(bool res, bool isOk)
    {
        Console.Write("\n\nDo you want to do? y/n: ");
        string result = Console.ReadLine();
        if (string.IsNullOrEmpty(result) || result.Length != 1)
        {
            Console.WriteLine("\n** Answer (Y) or (N) **");
            return (res, isOk);
        }
        if (result.ToLower().Contains("y"))
        {
            isOk = true;
            res = true;
        }
        else if(result.ToLower().Contains("n"))
        {
            isOk = false;
            res = true;
        }
        return (res, isOk);
    }
}
