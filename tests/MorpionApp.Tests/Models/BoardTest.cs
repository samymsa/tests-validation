using MorpionApp.Models;

namespace MorpionApp.Tests.Models;

public class BoardTest
{

    [Fact]
    public void Board_RowsCount_ReturnsRowsCount()
    {
        int size = 3;
        Board board = new(size, size);
        Assert.Equal(size, board.RowsCount);
    }

    [Fact]
    public void Board_ColumnsCount_ReturnsColumnsCount()
    {
        int size = 3;
        Board board = new(size, size);
        Assert.Equal(size, board.ColumnsCount);
    }

    [Fact]
    public void GetUnoccupiedCells_Initial_ReturnsAllCells()
    {
        int size = 3;
        Board board = new(size, size);
        Cell[] unoccupiedCells = board.GetUnoccupiedCells();
        Assert.Equal(size * size, unoccupiedCells.Length);
    }

    [Fact]
    public void GetUnoccupiedCells_CellOccupied_ReturnsUnoccupiedCells()
    {
        int size = 3;
        Board board = new(size, size);
        Cell cell = board.GetCell(new Position(1, 1));
        cell.SetPiece(Piece.X);
        Cell[] unoccupiedCells = board.GetUnoccupiedCells();
        Assert.Equal(size * size - 1, unoccupiedCells.Length);
    }

    [Fact]
    public void GetCell_ValidPosition_ReturnsCell()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(1, 1);
        Cell cell = board.GetCell(position);
        Assert.Equal(position, cell.Position);
    }

    [Fact]
    public void GetCell_InvalidPosition_ThrowsArgumentOutOfRangeException()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(size, size);
        Assert.Throws<ArgumentOutOfRangeException>(() => board.GetCell(position));
    }

    [Fact]
    public void GetRow_ValidRow_ReturnsColumnsCountCells()
    {
        int size = 3;
        Board board = new(size, size);
        Cell[] row = board.GetRow(1);
        Assert.Equal(board.ColumnsCount, row.Length);
    }

    [Fact]
    public void GetColumn_ValidColumn_ReturnsRowsCountCells()
    {
        int size = 3;
        Board board = new(size, size);
        Cell[] column = board.GetColumn(1);
        Assert.Equal(board.RowsCount, column.Length);
    }

    [Fact]
    public void GetPiece_ValidPosition_ReturnsPiece()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(1, 1);
        board.SetPiece(position, Piece.X);
        Assert.Equal(Piece.X, board.GetPiece(position));
    }

    [Fact]
    public void GetPiece_InvalidPosition_ThrowsArgumentOutOfRangeException()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(size, size);
        Assert.Throws<ArgumentOutOfRangeException>(() => board.GetPiece(position));
    }

    [Fact]
    public void SetPiece_ValidUnoccupiedPosition_PieceIsSet()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(1, 1);
        board.SetPiece(position, Piece.X);
        Assert.Equal(Piece.X, board.GetPiece(position));
    }

    [Fact]
    public void SetPiece_ValidOccupiedPosition_ThrowsInvalidOperationException()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(1, 1);
        board.SetPiece(position, Piece.X);
        Assert.Throws<InvalidOperationException>(() => board.SetPiece(position, Piece.O));
    }

    [Fact]
    public void SetPiece_InvalidPosition_ThrowsArgumentOutOfRangeException()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(size, size);
        Assert.Throws<ArgumentOutOfRangeException>(() => board.SetPiece(position, Piece.X));
    }

    [Fact]
    public void RemoveAllPieces_BoardWithPieces_AllCellsUnoccupied()
    {
        int size = 3;
        Board board = new(size, size);
        Position position = new(1, 1);
        board.SetPiece(position, Piece.X);
        board.RemoveAllPieces();
        Assert.Equal(size * size, board.GetUnoccupiedCells().Length);
    }
}
