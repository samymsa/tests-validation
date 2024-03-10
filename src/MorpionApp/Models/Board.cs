class Board
{
    public Cell[,] Cells { get; }
    public int RowsCount { get; }
    public int ColumnsCount { get; }

    public Board(int rowsCount, int columnsCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rowsCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columnsCount);
        RowsCount = rowsCount;
        ColumnsCount = columnsCount;
        Cells = new Cell[rowsCount, columnsCount];
        for (int row = 0; row < RowsCount; row++)
        {
            for (int column = 0; column < ColumnsCount; column++)
            {
                Cells[row, column] = new Cell(new Position(row, column));
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
        return Cells[position.Row, position.Column];
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

        // Find the top-left cell of the diagonal starting from the given position.
        int row = position.Row;
        int column = position.Column;
        while (row > 0 && column > 0)
        {
            row--;
            column--;
        }

        // Go downards and rightwards until the end of the board.
        while (row < RowsCount && column < ColumnsCount)
        {
            diagonal.Add(GetCell(new Position(row, column)));
            row++;
            column++;
        }

        return diagonal.ToArray();
    }

    public Cell[] GetAntiDiagonal(Position position)
    {
        CheckPosition(position);
        List<Cell> antiDiagonal = [];

        // Find the top-right cell of the anti-diagonal starting from the given position.
        int row = position.Row;
        int column = position.Column;
        while (row > 0 && column < ColumnsCount - 1)
        {
            row--;
            column++;
        }

        // Go downards and leftwards until the end of the board.
        while (row < RowsCount && column >= 0)
        {
            antiDiagonal.Add(GetCell(new Position(row, column)));
            row++;
            column--;
        }

        return antiDiagonal.ToArray();
    }

    public Cell[][] GetDiagonals(Position position)
    {
        return [GetDiagonal(position), GetAntiDiagonal(position)];
    }

    public Cell[] GetUnoccupiedCells()
    {
        return Cells.Cast<Cell>().Where(cell => !cell.IsOccupied()).ToArray();
    }

    public Position[] GetUnoccupiedPositions()
    {
        return GetUnoccupiedCells().Select(cell => cell.Position).ToArray();
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

    public void RemovePiece(Position position)
    {
        CheckPosition(position);
        GetCell(position).RemovePiece();
    }

    public void RemoveAllPieces()
    {
        foreach (var cell in Cells)
        {
            cell.RemovePiece();
        }
    }
}