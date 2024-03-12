namespace MorpionApp.Models.Player.Strategy;

public class AIPlayerStrategy : IPlayerStrategy
{
    public Position GetNextMove(Board board, Piece piece)
    {
        var random = new Random();
        Cell[] unoccupiedCells = board.GetUnoccupiedCells();
        int randomIndex = random.Next(unoccupiedCells.Length);
        return unoccupiedCells[randomIndex].Position;
    }
}
