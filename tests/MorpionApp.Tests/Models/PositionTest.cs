using MorpionApp.Models;

namespace MorpionApp.Tests.Models;

public class PositionTest
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(-1, -1)]
    public void Position_Negative_ThrowsArgumentOutOfRangeException(int row, int column)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Position(row, column));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    public void Position_Positive_ReturnsPosition(int row, int column)
    {
        Position position = new(row, column);
        Assert.Equal(row, position.Row);
        Assert.Equal(column, position.Column);
    }
}
