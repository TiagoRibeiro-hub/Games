public class Winner
{
    public bool WinnerResult { get; set; } = false;
    public bool CheckWinner(List<int> moves)
    {
        Winner winner = new();
        if (winner.Vertical(moves)) return true;
        if (winner.Horizontal(moves)) return true;
        if (winner.Diagonal(moves)) return true;

        return false;
    }
    public bool Diagonal(List<int> moves)
    {
        if ((moves.Contains(1) && moves.Contains(5) && moves.Contains(9)) ||
            (moves.Contains(3) && moves.Contains(5) && moves.Contains(7)))
        {
            return true;
        }
        return false;
    }
    public bool Vertical(List<int> moves)
    {
        if ((moves.Contains(1) && moves.Contains(4) && moves.Contains(7)) ||
            (moves.Contains(2) && moves.Contains(5) && moves.Contains(8)) ||
            (moves.Contains(3) && moves.Contains(6) && moves.Contains(9)))
        {
            return true;
        }
        return false;
    }
    public bool Horizontal(List<int> moves)
    {
        if ((moves.Contains(1) && moves.Contains(2) && moves.Contains(3)) ||
            (moves.Contains(4) && moves.Contains(5) && moves.Contains(6)) ||
            (moves.Contains(7) && moves.Contains(8) && moves.Contains(9)))
        {
            return true;
        }
        return false;
    }
    public void ShowWinner(Board board, Game game)
    {
        Console.Clear();
        board.ShowBoard(board.BoardMatrix, game);
        Console.WriteLine($"\nPlayer: {game.PlayerName} wins!!\n");
    }
}