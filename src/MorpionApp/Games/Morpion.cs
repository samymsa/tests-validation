using MorpionApp.GameOutcomeResolver;
using MorpionApp.GameSerializer;
using MorpionApp.Models;
using MorpionApp.NextMoveStrategy;
using MorpionApp.NextPlayerStrategy;
using MorpionApp.UI;

namespace MorpionApp.Games;

public class Morpion : BoardGame
{
    public Morpion() : base(
        new Board(3, 3),
        new XInARowWins(3),
        new ConsoleUI(),
        new RoundRobin(),
        new UnoccupiedStrategy(),
        new JSONGameSerializer(), "saves/morpion.save")
    { }
}