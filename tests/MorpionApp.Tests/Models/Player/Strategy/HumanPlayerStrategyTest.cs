using MorpionApp.Models;
using MorpionApp.Models.Player.Strategy;
using MorpionApp.UI;

namespace MorpionApp.Tests.Models.Player.Strategy;

public class HumanPlayerStrategyTest
{
    [Fact]
    public void GetNextMove_SpyUI_AskForPositionCalled()
    {
        SpyUI spyUI = new();
        HumanPlayerStrategy humanPlayerStrategy = new(spyUI);
        Board board = new(3, 3);
        Cell[] validCells = [new(new(0, 0)), new(new(0, 1)), new(new(0, 2))];

        humanPlayerStrategy.GetNextMove(board, validCells);

        Assert.True(spyUI.AskForPositionCalled);
    }
}
