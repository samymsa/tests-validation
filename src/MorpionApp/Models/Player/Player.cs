using MorpionApp.Models.Player.Strategy;

namespace MorpionApp.Models.Player;

public class Player(Piece piece, IPlayerStrategy playStrategy) : IPlayer
{
    public Piece Piece { get; } = piece;

    public IPlayerStrategy PlayStrategy { get; set; } = playStrategy;

    public Position GetNextMove(Board board)
    {
        return PlayStrategy.GetNextMove(board, Piece);
    }
}