using MorpionApp.Models;
using PlayerModel = MorpionApp.Models.Player.Player;
using MorpionApp.Models.Player.Strategy;

namespace MorpionApp.Tests.Models.Player;

public class PlayerTest
{
    [Fact]
    public void GetNextMove_SpyPlayerStrategy_Called()
    {
        SpyPlayerStrategy playerStrategy = new();
        PlayerModel player = new(Piece.X, playerStrategy);
        Board board = new(3, 3);
        Cell[] validCells = [new(new(0, 0)), new(new(0, 1)), new(new(0, 2))];

        player.GetNextMove(board, validCells);

        Assert.True(playerStrategy.GetNextMoveCalled);
    }
}
