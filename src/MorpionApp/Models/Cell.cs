class Cell
{
    public Position Position { get; }
    public Piece? Piece { get; private set; }

    public Cell(Position position)
    {
        Position = position;
    }

    public Cell(Position position, Piece piece)
    {
        Position = position;
        Piece = piece;
    }

    public void SetPiece(Piece piece)
    {
        if (IsOccupied())
        {
            throw new InvalidOperationException("Cell is already occupied");
        }
        Piece = piece;
    }

    public void RemovePiece()
    {
        Piece = null;
    }

    public bool IsOccupied()
    {
        return Piece != null;
    }
}