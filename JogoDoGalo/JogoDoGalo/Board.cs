public class Board
{
    public string[,] BoardMatrix { get; set; } = new string[3, 3];
    public void Display()
    {
        int possibleMoves = 1;
        for (int i = 0; i < this.BoardMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < this.BoardMatrix.GetLength(1); j++)
            {
                this.BoardMatrix[i, j] = possibleMoves.ToString();
                possibleMoves++;
            }
        }
    }
    public void ShowBoard(string[,] board, Game game)
    {
        Console.WriteLine();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if(j == 2 || j == 1)
                {
                    Console.Write($"| {board[i, j]} ");

                }
                else
                {
                    Console.Write($" {board[i, j]} ");
                }
            }
            Console.WriteLine("\n --|---|--");
        }
        Console.WriteLine();
        Console.WriteLine($"Player nrº {game.PlayerNr} ('{game.Shift}') => {game.PlayerName}");
    }
    public void PlayedBoard(string[,] board, Game game)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (game.PlaceToPlay.ToString() == board[i, j])
                {
                    board[i, j] = game.Shift;
                }
            }
        }
    }
}
