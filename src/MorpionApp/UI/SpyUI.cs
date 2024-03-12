using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.UI;

public class SpyUI : IUserInterface
{
    public bool AskForPositionCalled { get; private set; }
    public bool AskForGameTypeCalled { get; private set; }
    public bool AskForReplayCalled { get; private set; }
    public bool AskForAnotherGameCalled { get; private set; }
    public bool DisplayBoardCalled { get; private set; }
    public bool DisplayMessageCalled { get; private set; }
    public bool DisplayDrawCalled { get; private set; }
    public bool DisplayWinCalled { get; private set; }

    public SpyUI()
    {
        Reset();
    }

    public void Reset()
    {
        AskForPositionCalled = false;
        AskForGameTypeCalled = false;
        AskForReplayCalled = false;
        AskForAnotherGameCalled = false;
        DisplayBoardCalled = false;
        DisplayMessageCalled = false;
        DisplayDrawCalled = false;
        DisplayWinCalled = false;
    }

    public Position AskForPosition(Board board, Cell[] validCells)
    {
        AskForPositionCalled = true;
        return new Position(0, 0);
    }

    public Type? AskForGameType()
    {
        AskForGameTypeCalled = true;
        return null;
    }

    public bool AskForReplay()
    {
        AskForReplayCalled = true;
        return false;
    }

    public bool AskForAnotherGame()
    {
        AskForAnotherGameCalled = true;
        return false;
    }

    public void DisplayBoard(Board board)
    {
        DisplayBoardCalled = true;
    }

    public void DisplayMessage(string message)
    {
        DisplayMessageCalled = true;
    }

    public void DisplayDraw()
    {
        DisplayDrawCalled = true;
    }

    public void DisplayWin(Player player)
    {
        DisplayWinCalled = true;
    }
}
