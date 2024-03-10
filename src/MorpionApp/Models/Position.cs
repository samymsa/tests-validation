namespace MorpionApp.Models;

public class Position
{
    public int Row { get; }
    public int Column { get; }

    public Position(int row, int column)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfNegative(column);
        Row = row;
        Column = column;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position position)
        {
            return Row == position.Row && Column == position.Column;
        }
        return false;
    }
}
