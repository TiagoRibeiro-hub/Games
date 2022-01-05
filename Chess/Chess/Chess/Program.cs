global using Chess;

ShowConsole console = new();

// FIRST PLAYER
//console.AskPlayerName();
//Player player1 = new()
//{
//    Name = Console.ReadLine(),
//};
Player player1 = new()
{
    Name = "Player1"
};

player1.CoinSide = console.AskHeadsOrTailsFirstPlayer();

//// SECOND PLAYER
//console.AskPlayerName();
//Player player2 = new()
//{
//    Name = Console.ReadLine(),
//};
Player player2 = new()
{
    Name = "Player2"
};
player2.CoinSide = console.AskHeadsOrTailsSecondPlayer(player1.CoinSide);


//CHOOSE WHITE OR BLACK WHO STARTS
Console.Clear();
Game game = new();
string resFlipCoin = game.FlipCoin();
game.WhoPlays = game.ChooseWhoStarts(resFlipCoin, console, player1, player2);
game.Shift = PiecesColor.White;
game.ShiftDistribution(game, player1, player2);

// DISPLAY BOARD
Board board = new();
board.Display();
board.ShowBoard(board.Matrix);

// START GAME
Moves moves = new();
bool finishedGame = false;
do
{
    game.ResultPlayedBoard = true;
    do
    {
        if (game.ResultPlayedBoard == false)
        {
            console.MoveNotAllowed();
        }
        if (game.WhoPlays.Contains(player1.Name))
        {
            moves = moves.EnterMove(player1, game.Shift.ToString());
        }
        else
        {
            moves = moves.EnterMove(player2, game.Shift.ToString());
        }
        game = board.PlayedBoard(board.Matrix, moves, game);

    } while (game.ResultPlayedBoard == false);
    Console.Clear();
    board.ShowBoard(board.Matrix);

    //CHANGE PLAYER
    game.ChangePlayers(game, player1, player2);
    game.ShiftDistribution(game, player1, player2);

} while (finishedGame == false);



//https://www.instructables.com/Playing-Chess/