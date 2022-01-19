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
game.FinishedGame = false;
string resFlipCoin = game.FlipCoin();
game.WhoPlays = game.ChooseWhoStarts(resFlipCoin, console, player1, player2);
game.Shift = PiecesColor.White;

// PREPARE THE GAME
game.ShiftDistribution(game, player1, player2);
game.ResultPlayedBoard = true;

// DISPLAY BOARD
Board board = new();
board.Display();
board.ShowBoard(board.Matrix);

// START GAME
Move move = new();
move.GetSpecialMove = new();
do
{
    move.GetSpecialMove.SpecialMoveIsPossible = false;
    do
    {
        move.GetSpecialMove.NormalMove = true;
        // ENTER THE MOVE
        if (move.GetSpecialMove.SpecialMoveIsPossible)
        {
            move.GetSpecialMove.NormalMove = false;
            move.GetSpecialMove.SpecialMoveIsPossible = false;
            game.ResultPlayedBoard = true;
            //Show Special move
            bool isOk = console.ShowSpecialMove(board, move);
            if (isOk)
            {
                // SpecialMove
                board.PlaySpecialMove(board.Matrix, move);
                break;
            }
        }
        if (move.GetSpecialMove.NormalMove)
        {
            if (game.ResultPlayedBoard == false)
            {
                console.MoveNotAllowed();
            }
            if (game.WhoPlays.Contains(player1.Name))
            {
                (move, game.ResultPlayedBoard) = move.EnterMove(board, player1);
            }
            else
            {
                (move, game.ResultPlayedBoard) = move.EnterMove(board, player2);
            }
        }
        if (move.GetSpecialMove.SpecialMoveIsPossible)
        {
            // Temporarily
            game.ResultPlayedBoard = false;
        }
        else if (game.ResultPlayedBoard)
        {
            // MAKING THE MOVE
            game = board.PlayedBoard(board, move, game);
            if (!string.IsNullOrWhiteSpace(move.GetSpecialMove.AllowedEnPassantPawn))
            {
                board.AllowedEnPassantListPawn.Add(move.GetSpecialMove.AllowedEnPassantPawn);
            }
        }


    } while (game.ResultPlayedBoard == false);

    Console.Clear();
    board.ShowBoard(board.Matrix);

    //CHANGE PLAYER
    game.ChangePlayers(game, player1, player2);
    game.ShiftDistribution(game, player1, player2);

} while (game.FinishedGame == false);



//https://www.instructables.com/Playing-Chess/