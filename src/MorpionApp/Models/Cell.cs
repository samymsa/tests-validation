namespace MorpionApp.Models;

public class Cell(Position position)
{
    public Position Position { get; } = position;
    public Piece? Piece { get; private set; }

    public void SetPiece(Piece piece)
    {
        if (IsOccupied())
        {
            throw new InvalidOperationException("La cellule est déjà occupée.");
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