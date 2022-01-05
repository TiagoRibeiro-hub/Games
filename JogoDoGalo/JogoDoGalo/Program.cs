#nullable disable
bool playAgain = false;
bool samePlayers = false;
do
{
    // First Player
    Console.Write("\nFirst Player Name: ");
    Player player1 = new Player()
    {
        Name = Console.ReadLine(),
        Value = "X",
    };
    // Second Player
    Console.Write("\nSecond Player Name: ");
    Player player2 = new Player()
    {
        Name = Console.ReadLine(),
        Value = "O",
    };
    //GAME
    do
    {
        Console.Clear(); Console.WriteLine();
        Board board = new Board();
        // Enumerate Possible Moves 
        board.Display();

        // START GAME
        Game game = new();
        while (game.Attempts < 9)
        {
            if (game.PlayerNr == 1)
            {
                game.PlayerName = player1.Name;
            }
            else
            {
                game.PlayerName = player2.Name;
            }

            board.ShowBoard(board.BoardMatrix, game);

            game.PlaceToPlay = game.EnterMove(game, player1, player2);

            board.PlayedBoard(board.BoardMatrix, game);

            // CHECK WINNER
            Winner winner = new();
            if (game.Shift.Contains("X"))
            {
                winner.WinnerResult = winner.CheckWinner(player1.Moves);
                game.PlayerName = player1.Name;
            }
            else
            {
                winner.WinnerResult = winner.CheckWinner(player2.Moves);
                game.PlayerName = player2.Name;
            }

            // PLAY AGAIN
            if (winner.WinnerResult)
            {
                winner.ShowWinner(board, game);
                break;
            }

            // CHANGE PLAYERS
            game.ChangePlayers(game);
            Console.Clear();
        }
        playAgain = game.PlayAgain(player1, player2);
        if (playAgain)
        {
            samePlayers = game.SamePlayers();
        }
    } while (playAgain && samePlayers);

} while (playAgain);
Console.WriteLine("\nFinished");








