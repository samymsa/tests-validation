using MorpionApp.Games;

namespace MorpionApp.GameSerializer;

public interface IGameSerializer
{
    void Save(string filePath, BoardGame game);
    void Load(string filePath, BoardGame game);
}
