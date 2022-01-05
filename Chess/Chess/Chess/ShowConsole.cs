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
}
