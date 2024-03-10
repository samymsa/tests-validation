using MorpionApp.Models;

namespace MorpionApp.Tests.Models;

public class CellTest
{
    [Fact]
    public void Cell_Position_ReturnsPosition()
    {
        Position position = new(0, 0);
        Cell cell = new(position);
        Assert.Equal(position, cell.Position);
    }

    [Fact]
    public void Cell_Initial_NotOccupied()
    {
        Cell cell = new(new Position(0, 0));
        Assert.False(cell.IsOccupied());
    }


    [Fact]
    public void IsOccupied_PieceIsNotNull_ReturnsTrue()
    {
        Cell cell = new(new Position(0, 0));
        cell.SetPiece(Piece.X);
        Assert.True(cell.IsOccupied());
    }

    [Fact]
    public void IsOccupied_PieceIsNull_ReturnsFalse()
    {
        Cell cell = new(new Position(0, 0));
        Assert.False(cell.IsOccupied());
    }

    [Fact]
    public void RemovePiece_Occupied_NotOccupied()
    {
        Cell cell = new(new Position(0, 0));
        cell.SetPiece(Piece.X);
        cell.RemovePiece();
        Assert.False(cell.IsOccupied());
    }

    [Fact]
    public void RemovePiece_NotOccupied_NotOccupied()
    {
        Cell cell = new(new Position(0, 0));
        cell.RemovePiece();
        Assert.False(cell.IsOccupied());
    }

    [Fact]
    public void SetPiece_NotOccupied_Occupied()
    {
        Cell cell = new(new Position(0, 0));
        cell.SetPiece(Piece.X);
        Assert.True(cell.IsOccupied());
    }

    [Theory]
    [InlineData(Piece.X)]
    [InlineData(Piece.O)]
    public void SetPiece_NotOccupied_PieceEqualsPiece(Piece piece)
    {
        Cell cell = new(new Position(0, 0));
        cell.SetPiece(piece);
        Assert.Equal(piece, cell.Piece);
    }

    [Fact]
    public void SetPiece_Occupied_ThrowsInvalidOperationException()
    {
        Cell cell = new(new Position(0, 0));
        cell.SetPiece(Piece.X);
        Assert.Throws<InvalidOperationException>(() => cell.SetPiece(Piece.O));
    }
}
