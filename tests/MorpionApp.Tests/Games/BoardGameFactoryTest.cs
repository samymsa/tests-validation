using MorpionApp.Games;

namespace MorpionApp.Tests.Games;

public class BoardGameFactoryTest
{
    [Fact]
    public void Create_ValidGameType_ReturnsBoardGame()
    {
        Type gameType = typeof(Morpion);

        BoardGame? boardGame = BoardGameFactory.Create(gameType);

        Assert.NotNull(boardGame);
    }

    [Fact]
    public void Create_InvalidGameType_ThrowsInvalidCastException()
    {
        Type gameType = typeof(NotABoardGame);

        Assert.Throws<InvalidCastException>(() => BoardGameFactory.Create(gameType));
    }
}
