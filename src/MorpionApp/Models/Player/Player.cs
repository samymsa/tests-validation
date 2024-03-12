using MorpionApp.Models.Player.Strategy;

namespace MorpionApp.Models.Player;

public class Player(Piece piece, IPlayerStrategy playerStrategy)
{
    public Piece Piece { get; } = piece;

    private readonly IPlayerStrategy PlayerStrategy = playerStrategy;

    public Position GetNextMove(Board board, Cell[] validCells)
    {
        return PlayerStrategy.GetNextMove(board, validCells);
    }
}