namespace MorpionApp.Games;

public class BoardGameFactory
{
    public static BoardGame? Create(Type gameType)
    {
        return (BoardGame?)Activator.CreateInstance(gameType);
    }
}
