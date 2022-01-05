public class Game
{
    public Player Player { get; set; }
    public string Shift { get; set; } = "X";
    public int PlayerNr { get; set; } = 1;
    public string PlayerName { get; set; }
    public int PlaceToPlay { get; set; }
    public int Attempts { get; set; } = 0;
    public List<int> TotalMoves { get; set; } = new List<int>();


    public int EnterMove(Game game, Player player1, Player player2)
    {
        int play;
        bool isInt;
        do
        {
            bool notValid = false;
            isInt = int.TryParse(Console.ReadLine(), out play);
            if (game.TotalMoves.Contains(play))
            {
                notValid = true;
            }
            if (isInt == false || play == 0 || play >= 10 || notValid)
            {
                if (notValid)
                {
                    Console.WriteLine("This move is not allowed, already played");
                }
                else
                {
                    Console.WriteLine("Choose a number between 1 and 10");
                }
                isInt = false;
            }
        } while (isInt == false);

        if (game.Shift.Contains("X"))
        {
            player1.Moves.Add(play);
        }
        else
        {
            player2.Moves.Add(play);
        }
        game.TotalMoves.Add(play);
        return play;
    }
    public void ChangePlayers(Game game)
    {
        game.Attempts = +1;
        if (game.PlayerNr == 1)
        {
            game.PlayerNr = 2;
            game.Shift = "O";
        }
        else
        {
            game.PlayerNr = 1;
            game.Shift = "X";
        }
    }
    public bool PlayAgain(Player player1, Player player2)
    {
        Console.WriteLine("Want To Play Again? y/n");
        string res = Console.ReadLine();
        if (res.ToLower().Contains("y"))
        {
            player1.Moves = new();
            player2.Moves = new();
            return true;
        }
        return false;
    }
    public bool SamePlayers()
    {
        Console.WriteLine("Same players? y/n");
        string res = Console.ReadLine();
        if (res.Contains("y"))
        {
            return true;
        }
        return false;
    }

}
