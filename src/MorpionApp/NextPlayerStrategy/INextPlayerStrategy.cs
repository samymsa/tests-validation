using MorpionApp.Models.Player;

namespace MorpionApp.NextPlayerStrategy;

public interface INextPlayerStrategy
{
    int GetNextPlayer(List<Player> players, int currentPlayerIndex);
}
