namespace Chess;
#nullable disable
public class Game
{
    public PiecesColor Shift { get; set; }
    public string WhoPlays { get; set; }
    public bool ResultPlayedBoard { get; set; }
    public string Messages { get; set; }

#nullable enable
    public string FlipCoin()
    {
        Random rnr = new();
        int nr = 0;
        for (int i = 0; i < 1; i++)
        {
            nr = rnr.Next(0, 1000);
        }
        if (nr % 2 == 0)
        {
            return Coin.Heads;
        }
        return Coin.Tails;
    }
    public string ChooseWhoStarts(string resFlipCoin, ShowConsole console, Player player1, Player player2)
    {
        if (resFlipCoin.Contains(Coin.Heads) && player1.CoinSide.Contains(Coin.Heads))
        {
            string res = console.AskToChooseColor(player1.Name);
            if (res.ToLower().Contains("w"))
            {
                player1.PieceColor = PiecesColor.White;
                player2.PieceColor = PiecesColor.Black;
            }
            else
            {
                player1.PieceColor = PiecesColor.Black;
                player2.PieceColor = PiecesColor.White;
            }
        }
        else
        {
            string res = console.AskToChooseColor(player2.Name);
            if (res.ToLower().Contains("w"))
            {
                player2.PieceColor = PiecesColor.White;
                player1.PieceColor = PiecesColor.Black;
            }
            else
            {
                player2.PieceColor = PiecesColor.Black;
                player1.PieceColor = PiecesColor.White;
            }
        }
        Console.Clear();
        return console.ShowWhoStarts(player1, player2);
    }

    public void ShiftDistribution(Game game, Player player1, Player player2)
    {
        
        if (player1.Name.Contains(game.WhoPlays))
        {
            player1.PieceColor = game.Shift;
        }
        else
        {
            player2.PieceColor = game.Shift;
        }
    }
    
    public void ChangePlayers(Game game, Player player1, Player player2)
    {
        if (game.Shift.ToString().Contains(PiecesColor.White.ToString()))
        {
            if (player1.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
            {
                game.WhoPlays = player2.Name;
            }
            else
            {
                game.WhoPlays = player1.Name;
            }
            game.Shift = PiecesColor.Black;
        }
        else
        {
            if (player1.PieceColor.ToString().Contains(PiecesColor.Black.ToString()))
            {
                game.WhoPlays = player2.Name;
            }
            else
            {
                game.WhoPlays = player1.Name;
            }
            game.Shift = PiecesColor.White;
        }

    }
}
