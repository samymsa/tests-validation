using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.NextMoveStrategy;

public interface INextMoveStrategy
{
    Position GetNextMove(Player player, Board board);
}
