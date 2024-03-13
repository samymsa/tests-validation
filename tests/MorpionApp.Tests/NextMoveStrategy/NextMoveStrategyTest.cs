using MorpionApp.Models;
using MorpionApp.Models.Player;
using MorpionApp.Models.Player.Strategy;
using MorpionApp.NextMoveStrategy;
using MorpionApp.UI;

namespace MorpionApp.Tests.NextMoveStrategy;

public class NextMoveStrategyTest
{
    public static IEnumerable<object[]> NextMoveStrategies =>
        [
            [new UnoccupiedStrategy()],
            [new BottomMostUnoccupiedStrategy()],
        ];

    [Theory]
    [MemberData(nameof(NextMoveStrategies))]
    public void GetNextMove_Empty_ReturnsUnoccupiedPosition(INextMoveStrategy strategy)
    {
        Player player = new(Piece.X, new AIPlayerStrategy());
        Board board = new(3, 3);

        Position position = strategy.GetNextMove(player, board);

        Assert.NotNull(position);
        Assert.False(board.IsOccupied(position));
    }

    [Theory]
    [MemberData(nameof(NextMoveStrategies))]
    public void GetNextMove_SomeOccupied_ReturnsUnoccupiedPosition(INextMoveStrategy strategy)
    {
        Player player = new(Piece.X, new AIPlayerStrategy());
        Board board = new(3, 3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(0, 1), Piece.O);
        board.SetPiece(new(0, 2), Piece.X);

        Position position = strategy.GetNextMove(player, board);

        Assert.NotNull(position);
        Assert.False(board.IsOccupied(position));
    }

    [Theory]
    [MemberData(nameof(NextMoveStrategies))]
    public void GetNextMove_AllButOneOccupied_ReturnsUnoccupiedPosition(INextMoveStrategy strategy)
    {
        Player player = new(Piece.X, new AIPlayerStrategy());
        Board board = new(3, 3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(0, 1), Piece.O);
        board.SetPiece(new(0, 2), Piece.X);
        board.SetPiece(new(1, 0), Piece.O);
        board.SetPiece(new(1, 1), Piece.X);
        board.SetPiece(new(1, 2), Piece.O);
        board.SetPiece(new(2, 0), Piece.X);
        board.SetPiece(new(2, 1), Piece.O);

        Position position = strategy.GetNextMove(player, board);

        Assert.NotNull(position);
        Assert.False(board.IsOccupied(position));
    }
}
