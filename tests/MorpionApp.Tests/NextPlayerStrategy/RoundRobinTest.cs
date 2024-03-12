using MorpionApp.Models;
using MorpionApp.Models.Player;
using MorpionApp.Models.Player.Strategy;
using MorpionApp.NextPlayerStrategy;

namespace MorpionApp.Tests.NextPlayerStrategy;

public class RoundRobinTest
{
    [Fact]
    public void GetNextPlayer_LastPlayer_ReturnsFirstPlayer()
    {
        List<Player> players = [new(Piece.X, new AIPlayerStrategy()), new Player(Piece.O, new AIPlayerStrategy())];
        RoundRobin roundRobin = new();
        int currentPlayerIndex = 1;

        int nextPlayerIndex = roundRobin.GetNextPlayer(players, currentPlayerIndex);

        Assert.Equal(0, nextPlayerIndex);
    }

    [Fact]
    public void GetNextPlayer_FirstPlayer_ReturnsSecondPlayer()
    {
        List<Player> players = [new(Piece.X, new AIPlayerStrategy()), new Player(Piece.O, new AIPlayerStrategy())];
        RoundRobin roundRobin = new();
        int currentPlayerIndex = 0;

        int nextPlayerIndex = roundRobin.GetNextPlayer(players, currentPlayerIndex);

        Assert.Equal(1, nextPlayerIndex);
    }

    [Fact]
    public void GetNextPlayer_OnePlayer_ReturnsSamePlayer()
    {
        List<Player> players = [new(Piece.X, new AIPlayerStrategy())];
        RoundRobin roundRobin = new();
        int currentPlayerIndex = 0;

        int nextPlayerIndex = roundRobin.GetNextPlayer(players, currentPlayerIndex);

        Assert.Equal(0, nextPlayerIndex);
    }
}
