using MorpionApp.Models;

namespace MorpionApp.GameOutcomeResolver;

public interface IGameOutcomeResolver
{
    GameOutcome Resolve(Board board, Position lastPlayedPosition);
}

