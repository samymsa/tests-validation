using MorpionApp.UI;

namespace MorpionApp.Models.Player.Strategy;

public class HumanPlayerStrategy(IUserInterface ui) : IPlayerStrategy
{
    private IUserInterface UI { get; } = ui;

    public Position GetNextMove(Board board, Cell[] validCells)
    {
        return UI.AskForPosition(board, validCells);
    }
}
