global using Chess;

// FIRST PLAYER
ShowConsole.AskPlayerName();
Player player1 = new()
{
    Name = Console.ReadLine(),
};
player1.CoinSide = ShowConsole.AskHeadsOrTailsFirstPlayer();

//// SECOND PLAYER
ShowConsole.AskPlayerName();
Player player2 = new()
{
    Name = Console.ReadLine(),
};
player2.CoinSide = ShowConsole.AskHeadsOrTailsSecondPlayer(player1.CoinSide);

bool continueGame = false;
do
{
    //CHOOSE WHITE OR BLACK WHO STARTS 
    // PREPARE THE GAME
    Console.Clear();
    Game game = new();
    game.FinishedGame = false;
    game.ResultPlayedBoard = true;
    if (continueGame == false)
    {
        string resFlipCoin = game.FlipCoin();
        game.WhoPlays = game.ChooseWhoStarts(resFlipCoin, player1, player2);
    }
    else 
    {
        if(player1.PieceColor.ToString() == PiecesColor.White.ToString())
        {
            game.WhoPlays = player1.Name;
        }
        else
        {
            game.WhoPlays = player2.Name;
        }
    }
    game.Shift = PiecesColor.White;
    game.ShiftDistribution(game, player1, player2);

    // DISPLAY BOARD
    Board board = new();
    board.Display();
    board.ShowBoard(board.Matrix);
    board.IsCheck = new();
    //board.IsCheck.ListCheckByToPrint = new();
    board.IsCheck.SideCheckedConfirmationDict = new();

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
                bool isOk = ShowConsole.ShowSpecialMove(board, move);
                if (isOk)
                {
                    // SpecialMove
                    board.PlaySpecialMove(board, move);
                    break;
                }
            }
            if (move.GetSpecialMove.NormalMove)
            {
                if (game.ResultPlayedBoard == false)
                {
                    ShowConsole.MoveNotAllowed();
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
            if(board.IsCheck.IsCheckMate == false)
            {
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
            }
            if (board.IsCheck.IsCheckMate)
            {
                game.ResultPlayedBoard = true;
            }
        } while (game.ResultPlayedBoard == false || board.IsCheck.IsCheckMate == false);

        Console.Clear();
        if (board.IsCheck.IsCheckMate)
        {

            Console.WriteLine("********************\n**** CHECKMATE *****\n********************");
            game.FinishedGame = true;
        }
        else
        {
            board.ShowBoard(board.Matrix);
            if (board.IsCheck.SideCheckedConfirmationDict.Any())
            {
                // is check by...
                ShowConsole.IsCheckBy(board);
                board.IsCheck.SideCheckedConfirmationDict.Clear();
            }
            //CHANGE PLAYER
            game.ChangePlayers(game, player1, player2);
            game.ShiftDistribution(game, player1, player2);
        }

    } while (game.FinishedGame == false);

    Console.WriteLine("*** FINISHED ***\nPlay again? (y/n):");
    bool flag = false;
    do
    {
        string res = Console.ReadLine();
        if (!string.IsNullOrEmpty(res) || res.Length == 1)
        {
            if (res.ToLower() == "y")
            {
                flag = true;
                continueGame = true;
                
                if (player1.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
                {
                    player1.PieceColor = PiecesColor.Black;
                    player2.PieceColor = PiecesColor.White;
                }
                else
                {
                    player1.PieceColor = PiecesColor.White;
                    player2.PieceColor = PiecesColor.Black;
                }
            }
            if(res.ToLower() == "n")
            {
                flag = true;
                continueGame = false;
            }
        }
        else
        {
            Console.WriteLine("** Answer Y or N **\n");
        }

    } while (flag == false);

} while (continueGame);
Console.WriteLine("ByeBye !!");
