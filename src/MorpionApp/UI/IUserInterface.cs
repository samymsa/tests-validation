using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.UI;

public interface IUserInterface
{
    Position AskForPosition(Board board, Cell[] validCells);
    bool AskForReplay();
    void DisplayBoard(Board board);
    void DisplayMessage(string message);
    void DisplayDraw();
    void DisplayWin(Player player);
}
