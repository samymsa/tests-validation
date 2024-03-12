using MorpionApp.Models.Player;

namespace MorpionApp.NextPlayerStrategy;

public class RoundRobin : INextPlayerStrategy
{
    public int GetNextPlayer(List<Player> players, int currentPlayerIndex)
    {
        return (currentPlayerIndex + 1) % players.Count;
    }
}
