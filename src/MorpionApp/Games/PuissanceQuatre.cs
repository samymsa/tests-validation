using MorpionApp.GameOutcomeResolver;
using MorpionApp.GameSerializer;
using MorpionApp.Models;
using MorpionApp.NextMoveStrategy;
using MorpionApp.NextPlayerStrategy;
using MorpionApp.UI;

namespace MorpionApp.Games;

public class PuissanceQuatre : BoardGame
{
    public PuissanceQuatre() : base(
        new Board(6, 7),
        new XInARowWins(4),
        new ConsoleUI(),
        new RoundRobin(),
        new BottomMostUnoccupiedStrategy(),
        new JSONGameSerializer(), "saves/puissancequatre.save")
    { }
}