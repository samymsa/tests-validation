using MorpionApp.GameOutcomeResolver;
using MorpionApp.Models;
using MorpionApp.Models.Player;
using MorpionApp.NextPlayerStrategy;
using MorpionApp.UI;

namespace MorpionApp.Games;

public class PuissanceQuatre : BoardGame
{
    public PuissanceQuatre() : base(6, 7, new XInARowWins(4), new ConsoleUI(), new RoundRobin())
    {
    }

    protected override Position GetNextMove(Player player)
    {
        Position nextMove = player.GetNextMove(Board, Board.GetUnoccupiedCells());
        for (int row = Board.RowsCount - 1; row >= 0; row--)
        {
            Position position = new(row, nextMove.Column);
            if (!Board.IsOccupied(position))
            {
                return position;
            }
        }
        return nextMove;
    }
}