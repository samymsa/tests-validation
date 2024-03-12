namespace MorpionApp.Models.Player.Strategy;

public class AIPlayerStrategy : IPlayerStrategy
{
    public Position GetNextMove(Board board, Cell[] validCells)
    {
        var random = new Random();
        int randomIndex = random.Next(validCells.Length);
        return validCells[randomIndex].Position;
    }
}
