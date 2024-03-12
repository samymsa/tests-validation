using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.NextMoveStrategy;

public class BottomMostUnoccupiedStrategy : INextMoveStrategy
{
    public Position GetNextMove(Player player, Board board)
    {
        Position nextMove = player.GetNextMove(board, board.GetUnoccupiedCells());
        for (int row = board.RowsCount - 1; row >= 0; row--)
        {
            Position position = new(row, nextMove.Column);
            if (!board.IsOccupied(position))
            {
                return position;
            }
        }
        return nextMove;
    }
}

