namespace MorpionApp.Models.Player.Strategy;

public class SpyPlayerStrategy : IPlayerStrategy
{
    public bool GetNextMoveCalled { get; private set; } = false;

    public Position GetNextMove(Board board, Cell[] validCells)
    {
        GetNextMoveCalled = true;
        return new Position(0, 0);
    }
}
