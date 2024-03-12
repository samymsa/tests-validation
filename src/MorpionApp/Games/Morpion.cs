using MorpionApp.GameOutcomeResolver;
using MorpionApp.UI;

namespace MorpionApp.Games;

public class Morpion : BoardGame
{
    public Morpion() : base(3, 3, new XInARowWins(3), new ConsoleUI())
    {
    }
}