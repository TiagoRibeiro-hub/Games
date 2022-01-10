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

do
{
    move.SpecialMoveIsPossible = false;
    do
    {
        move.NormalMove = true;
        // ENTER THE MOVE
        if (move.SpecialMoveIsPossible)
        {
            move.NormalMove = false;
            move.SpecialMoveIsPossible = false;
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
        if (move.NormalMove)
        {
            if (game.ResultPlayedBoard == false)
            {
                console.MoveNotAllowed();
            }
            if (board.AllowedEnPassantListPawn.Any())
            {
                // show possible enPassant moves
                
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
        if (move.SpecialMoveIsPossible)
        {
            // Temporarily
            game.ResultPlayedBoard = false;
        }
        else if (game.ResultPlayedBoard)
        {
            // MAKING THE MOVE
            game = board.PlayedBoard(board.Matrix, move, game);
            if (!string.IsNullOrWhiteSpace(move.AllowedEnPassantPawn))
            {
                board.AllowedEnPassantListPawn.Add(move.AllowedEnPassantPawn);
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