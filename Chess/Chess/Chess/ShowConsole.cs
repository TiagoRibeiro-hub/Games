﻿namespace Chess;
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

    public bool ShowSpecialMove(Board board, Move move)
    {
        Console.Write($"\n*********************\n*** SPECIAL MOVE ***\n*********************" +
                $"\n\nIs possible to {move.GetSpecialMove.SpecialMoveName.ToUpper()}:");

        if (move.GetSpecialMove.SpecialMoveName == SpecialMovesName.Castling)
        {
            Console.Write($"\nTower moves to => {move.GetSpecialMove.SpecialMovementFirstPieceTo.ToUpper()}" +
            $"\nand King moves to => {move.GetSpecialMove.SpecialMovementSecondPieceTo.ToUpper()}");
        }
        if (move.GetSpecialMove.SpecialMoveName == SpecialMovesName.EnPassant)
        {
            int count = ShowPossiblesMovesEnPassant(move);
            Console.Write("\n\nDo you want to play the special move? (y/n):");
            string res = Console.ReadLine();
            bool resOpt = false;
            do
            {
                if (!string.IsNullOrEmpty(res) && res.Length == 1)
                {
                    if (res.ToLower() == "y")
                    {
                        resOpt = true;
                        string nrCount;
                        if (count == 1)
                        {
                            nrCount = "(1)";
                        }
                        else
                        {
                            nrCount = "(1 or 2)";
                        }
                        Console.Write("\n\nChoose a option:");
                        string option = Console.ReadLine();
                        if (option.Contains("1"))
                        {
                            ShowPossiblesMovesEnPassant(move, 1);
                            resOpt = true;
                        }
                        else if (option.Contains("2"))
                        {
                            ShowPossiblesMovesEnPassant(move, 2);
                            resOpt = true;
                        }
                    }
                    if (res.ToLower() == "n")
                    {
                        return false;
                    }
                }
                if (resOpt == false)
                {
                    Console.WriteLine("\n** Answer (Y) or (N) **");
                }
            } while (resOpt == false);
        }

        bool result = false;
        bool isOk = false;
        do
        {
            (result, isOk) = Answer(result, isOk);
        } while (result == false);

        return isOk;
    }

    private static int ShowPossiblesMovesEnPassant(Move move, int option = 0)
    {
        int count = 0;
        foreach (var item in move.GetSpecialMove.PossibleMovesEnPassant)
        {
            count += 1;
            bool flag = false;
            string opt = string.Empty;
            string[] moves = item.Split(';');
            string goesTo = string.Empty, empty = string.Empty;
            for (int i = 0; i < moves.Length; i++)
            {
                if (i == 0)
                {
                    goesTo = moves[i];
                }
                else
                {
                    empty = moves[i];
                }
            }
            if (count == 1)
            {
                opt = "First";
            }
            if (count == 2)
            {
                opt = "Second";
            }
            if (option == 0)
            {
                ShowOptions(opt, goesTo, empty);
            }
            if (option == 1 && count == 1)
            {
                ShowOptions(opt, goesTo, empty);
                flag = true;
            }
            if (option == 2 && count == 2)
            {
                ShowOptions(opt, goesTo, empty);
                flag = true;
            }
            if (flag)
            {
                move.GetSpecialMove.SpecialMovementFirstPieceTo = goesTo;
                move.GetSpecialMove.SpecialMovementSecondPieceTo = empty;
                break;
            }
        }
        return count;
    }

    private static void ShowOptions(string opt, string goesTo, string empty)
    {
        Console.Write($"\n\n{opt} option" +
                        $"\nYour Pawn moves to => {goesTo.ToUpper()}" +
                        $"\nand capture the Pawn from => {empty.ToUpper()}");
    }

    public (bool, bool) Answer(bool res, bool isOk)
    {
        Console.Write("\n\nConfirm the move? y/n: ");
        string result = Console.ReadLine();
        if (!string.IsNullOrEmpty(result) && result.Length == 1)
        {
            if (result.ToLower().Contains("y"))
            {
                isOk = true;
                res = true;
            }
            else if (result.ToLower().Contains("n"))
            {
                isOk = false;
                res = true;
            }
        }
        if(res == false)
        {
            Console.WriteLine("\n** Answer (Y) or (N) **");
        }      
        return (res, isOk);
    }
}
