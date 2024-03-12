using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.NextMoveStrategy;

public class UnoccupiedStrategy : INextMoveStrategy
{
    public Position GetNextMove(Player player, Board board)
    {
        return player.GetNextMove(board, board.GetUnoccupiedCells());
    }
}
