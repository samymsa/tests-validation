using MorpionApp.Models;

namespace MorpionApp.GameOutcomeResolver;

public class XInARowWins(int xToWin) : IGameOutcomeResolver
{
    private readonly int XToWin = xToWin;

    public GameOutcome Resolve(Board board, Position lastPlayedPosition)
    {
        if (CheckWin(board, lastPlayedPosition)) return GameOutcome.Win;
        if (CheckDraw(board)) return GameOutcome.Draw;
        return GameOutcome.InProgress;
    }

    private bool CheckWin(Board board, Position position)
    {
        return CheckRowWin(board, position)
            || CheckColumnWin(board, position)
            || CheckDiagonalWin(board, position)
            || CheckAntiDiagonalWin(board, position);
    }

    private bool CheckRowWin(Board board, Position position)
    {
        Cell[] row = board.GetRow(position.Row);
        return XInARow(row, board.GetPiece(position));
    }

    private bool CheckColumnWin(Board board, Position position)
    {
        Cell[] column = board.GetColumn(position.Column);
        return XInARow(column, board.GetPiece(position));
    }

    private bool CheckDiagonalWin(Board board, Position position)
    {
        Cell[] diagonal = board.GetDiagonal(position);
        return XInARow(diagonal, board.GetPiece(position));
    }

    private bool CheckAntiDiagonalWin(Board board, Position position)
    {
        Cell[] antiDiagonal = board.GetAntiDiagonal(position);
        return XInARow(antiDiagonal, board.GetPiece(position));
    }

    private bool XInARow(Cell[] cells, Piece? piece)
    {
        if (piece == null) return false;

        int count = 0;
        foreach (Cell cell in cells)
        {
            if (cell.Piece != piece)
            {
                count = 0;
                continue;
            }

            if (++count == XToWin) return true;
        }

        return false;
    }

    private bool CheckDraw(Board board)
    {
        return board.IsFull();
    }
}

