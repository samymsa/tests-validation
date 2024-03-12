namespace MorpionApp.Models;

public class Cell(Position position, Piece? piece = null)
{
    public Position Position { get; set; } = position;
    public Piece? Piece { get; set; } = piece;

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