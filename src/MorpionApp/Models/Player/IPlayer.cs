namespace MorpionApp.Models.Player;

public interface IPlayer
{
    public Piece Piece { get; }
    public Position GetNextMove(Board board);
}