using System.Text.Json;
using MorpionApp.Games;

namespace MorpionApp.GameSerializer;

public class JSONGameSerializer : IGameSerializer
{
    public void Save(string filePath, BoardGame game)
    {
        string data = JsonSerializer.Serialize(game);
        CreateDirectory(filePath);
        File.WriteAllText(filePath, data);
    }

    public void Load(string filePath, BoardGame game)
    {
        string data = File.ReadAllText(filePath);
        BoardGame? tmpGame = JsonSerializer.Deserialize<BoardGame>(data);
        if (tmpGame != null)
        {
            game.Board = tmpGame.Board;
            game.CurrentPlayerIndex = tmpGame.CurrentPlayerIndex;
        }
    }

    private void CreateDirectory(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if (directory != null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}