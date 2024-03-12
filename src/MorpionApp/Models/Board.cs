namespace MorpionApp.Models;

public class Board
{
    public Cell[][] Cells { get; set; }
    public int RowsCount { get; set; }
    public int ColumnsCount { get; set; }

    public Board(int rowsCount, int columnsCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rowsCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columnsCount);
        RowsCount = rowsCount;
        ColumnsCount = columnsCount;
        Cells = new Cell[RowsCount][];
        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int row = 0; row < RowsCount; row++)
        {
            Cells[row] = new Cell[ColumnsCount];
            for (int column = 0; column < ColumnsCount; column++)
            {
                Cells[row][column] = new Cell(new(row, column));
            }
        }
    }

    private void CheckPosition(Position position)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(position.Row, RowsCount);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(position.Column, ColumnsCount);
    }

    public Cell GetCell(Position position)
    {
        CheckPosition(position);
        return Cells[position.Row][position.Column];
    }

    public Cell[] GetRow(int row)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, RowsCount);
        return Enumerable.Range(0, ColumnsCount).Select(column => GetCell(new Position(row, column))).ToArray();
    }

    public Cell[] GetColumn(int column)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnsCount);
        return Enumerable.Range(0, RowsCount).Select(row => GetCell(new Position(row, column))).ToArray();
    }

    public Cell[] GetDiagonal(Position position)
    {
        CheckPosition(position);
        List<Cell> diagonal = [];
        Position topLeft = GoTopLeft(position);
        int row = topLeft.Row;
        int column = topLeft.Column;

        while (row < RowsCount && column < ColumnsCount)
        {
            diagonal.Add(GetCell(new Position(row, column)));
            row++;
            column++;
        }

        return [.. diagonal];
    }


    public Cell[] GetAntiDiagonal(Position position)
    {
        CheckPosition(position);
        List<Cell> antiDiagonal = [];
        Position topRight = GoTopRight(position);
        int row = topRight.Row;
        int column = topRight.Column;

        while (row < RowsCount && column >= 0)
        {
            antiDiagonal.Add(GetCell(new(row, column)));
            row++;
            column--;
        }

        return [.. antiDiagonal];
    }

    private Position GoTopLeft(Position startingPosition)
    {
        int row = startingPosition.Row;
        int column = startingPosition.Column;
        while (row > 0 && column > 0)
        {
            row--;
            column--;
        }
        return new(row, column);
    }

    private Position GoTopRight(Position startingPosition)
    {
        int row = startingPosition.Row;
        int column = startingPosition.Column;
        while (row > 0 && column < ColumnsCount - 1)
        {
            row--;
            column++;
        }
        return new(row, column);
    }

    public Cell[] GetUnoccupiedCells()
    {
        return Cells.SelectMany(row => row).Where(cell => !cell.IsOccupied()).ToArray();
    }

    public bool IsOccupied(Position position)
    {
        return GetCell(position).IsOccupied();
    }

    public bool IsFull()
    {
        return GetUnoccupiedCells().Length == 0;
    }

    public Piece? GetPiece(Position position)
    {
        CheckPosition(position);
        return GetCell(position).Piece;
    }

    public void SetPiece(Position position, Piece piece)
    {
        CheckPosition(position);
        GetCell(position).SetPiece(piece);
    }

    public void RemoveAllPieces()
    {
        Cells.SelectMany(row => row).ToList().ForEach(cell => cell.RemovePiece());
    }
}