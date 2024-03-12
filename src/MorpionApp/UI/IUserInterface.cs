using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.UI;

public interface IUserInterface
{
    Position AskForPosition(Board board, Cell[] validCells);
    Type? AskForGameType();
    bool AskForReplay();
    bool AskForAnotherGame();
    void DisplayBoard(Board board);
    void DisplayMessage(string message);
    void DisplayDraw();
    void DisplayWin(Player player);
}
