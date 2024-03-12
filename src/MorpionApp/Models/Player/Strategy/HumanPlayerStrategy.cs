using MorpionApp.UI;

namespace MorpionApp.Models.Player.Strategy;

public class HumanPlayerStrategy(ConsoleUI ui) : IPlayerStrategy
{
    private ConsoleUI UI { get; } = ui;

    public Position GetNextMove(Board board, Piece piece)
    {
        return UI.AskForPosition(board);
    }
}
