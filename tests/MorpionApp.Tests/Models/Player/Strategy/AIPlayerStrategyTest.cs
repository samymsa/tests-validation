using MorpionApp.Models;
using MorpionApp.Models.Player.Strategy;

namespace MorpionApp.Tests.Models.Player.Strategy;

public class AIPlayerStrategyTest
{
    [Fact]
    public void GetNextMove_ValidCells_ReturnsValidPosition()
    {
        AIPlayerStrategy aiPlayerStrategy = new();
        Board board = new(3, 3);
        Cell[] validCells = [new(new(0, 0)), new(new(0, 1)), new(new(0, 2))];

        Position position = aiPlayerStrategy.GetNextMove(board, validCells);

        Assert.NotNull(position);
        Assert.Contains(position, validCells.Select(cell => cell.Position));
    }
}
