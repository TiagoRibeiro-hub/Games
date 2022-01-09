﻿namespace Chess;
public class Move : SpecialMove
{
    public string MoveFrom { get; set; }
    public string MoveTo { get; set; }
    public List<string> MovesList { get; set; } = GetMovesList();


    private ShowConsole showConsole = new();
    private static List<string> GetMovesList()
    {
        List<string> moves = new();
        for (int i = 1; i < 9; i++)
        {
            moves.Add("a" + i);
            moves.Add("b" + i);
            moves.Add("c" + i);
            moves.Add("d" + i);
            moves.Add("e" + i);
            moves.Add("f" + i);
            moves.Add("g" + i);
            moves.Add("h" + i);
        }
        return moves;
    }
    private string FromMove()
    {
        Console.Write("From: ");
        string fromMove = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fromMove) || fromMove.Length != 2)
        {
            showConsole.MoveNotAllowed();
            fromMove = string.Empty;
        }
        return fromMove;
    }
    private string ToMove()
    {
        Console.Write("To: ");
        string toMove = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(toMove) || toMove.Length != 2)
        {
            showConsole.MoveNotAllowed();
            toMove = string.Empty;
        }
        return toMove;
    }
    public (Move, bool) EnterMove(string[,] board, Player player)
    {
        Console.WriteLine($"\n{player.Name.ToUpper()} color ({player.PieceColor}) enter your move:");

        string valueFrom;
        valueFrom = FromMove();
        if (string.IsNullOrWhiteSpace(valueFrom))
        {
            return (new Move(), false);
        }
        string valueTo = ToMove();
        if (string.IsNullOrWhiteSpace(valueTo))
        {
            return (new Move(), false);
        }
        if (player.PieceColor.ToString().Contains(PiecesColor.White.ToString()))
        {
            this.PieceColor = PiecesColor.White;
        }
        else
        {
            this.PieceColor = PiecesColor.Black;
        }
        // see if has special move
        SpecialMove hasSpecialMoves = new();
        hasSpecialMoves = hasSpecialMoves.HasSpecialMoves(board, valueFrom, player.PieceColor.ToString());
        if (hasSpecialMoves.SpecialMoveIsPossible)
        {
            return (new Move()
            {
                Name = player.Name,
                MoveFrom = valueFrom,
                MoveTo = valueTo,
                PieceColor = this.PieceColor,
                SpecialMoveIsPossible = hasSpecialMoves.SpecialMoveIsPossible,
                SpecialMoveName = hasSpecialMoves.SpecialMoveName,
                CastlingTowerMovesTo = hasSpecialMoves.CastlingTowerMovesTo,
                CastlingKingMovesTo = hasSpecialMoves.CastlingKingMovesTo,
                CastlingType = hasSpecialMoves.CastlingType,
            },true);
        }
        return (new Move()
        {
            Name = player.Name,
            MoveFrom = valueFrom,
            MoveTo = valueTo,
            PieceColor = this.PieceColor,
            SpecialMoveIsPossible = hasSpecialMoves.SpecialMoveIsPossible,
            SpecialMoveName = hasSpecialMoves.SpecialMoveName,
        }, true);
    }
    public bool ConfirmMove(Move move)
    {
        int res = 0;
        foreach (var item in MovesList)
        {
            if (move.MoveFrom.ToLower().Contains(item))
            {
                res += 1;
            }
            if (move.MoveTo.ToLower().Contains(item))
            {
                res += 1;
            }
        }
        if (res == 2)
        {
            return true;
        }
        return false;
    }

}