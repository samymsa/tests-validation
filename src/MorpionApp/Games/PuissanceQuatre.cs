using MorpionApp.Models;

namespace MorpionApp.Games;

public class PuissanceQuatre : BoardGame
{
    public PuissanceQuatre() : base(6, 7, 4)
    {
    }

    public override void HandleEnterKey()
    {
        for (int row = Rows - 1; row >= 0; row--)
        {
            Position position = new(row, column);
            if (!Board.IsOccupied(position))
            {
                Board.SetPiece(position, CurrentPlayer.Piece);
                lastPlayedPosition = position;
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
                break;
            }
        }
    }

    public override void HandleInput(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                MoveCursorLeft();
                break;
            case ConsoleKey.RightArrow:
                MoveCursorRight();
                break;
            case ConsoleKey.Enter:
                HandleEnterKey();
                break;
            case ConsoleKey.Escape:
                HandleEscapeKey();
                break;
            default:
                break;
        }
    }

}