namespace MorpionApp.Models.Player.Strategy;

public interface IPlayerStrategy
{
    public Position GetNextMove(Board board, Piece piece);
}