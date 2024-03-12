using MorpionApp.Games;
using MorpionApp.UI;

namespace MorpionApp;

public class Program
{
    private static readonly IUserInterface UI = new ConsoleUI();

    public static void Main(string[] args)
    {
        do
        {
            Type? gameType = UI.AskForGameType();
            if (gameType is null) return;
            BoardGame? game = BoardGameFactory.Create(gameType);
            if (game is null) return;
            game.MainLoop();
        } while (UI.AskForAnotherGame());
    }
}
