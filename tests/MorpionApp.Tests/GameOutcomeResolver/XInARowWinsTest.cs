using MorpionApp.GameOutcomeResolver;
using MorpionApp.Models;

namespace MorpionApp.Tests.GameOutcomeResolver;

public class XInARowWinsTest
{
    [Fact]
    public void Resolve_ThreeInARow_ReturnsWin()
    {
        Board board = new(3, 3);
        IGameOutcomeResolver gameOutcomeResolver = new XInARowWins(3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(0, 1), Piece.X);
        board.SetPiece(new(0, 2), Piece.X);
        Position lastPlayedPosition = new(0, 2);

        GameOutcome outcome = gameOutcomeResolver.Resolve(board, lastPlayedPosition);

        Assert.Equal(GameOutcome.Win, outcome);
    }

    [Fact]
    public void Resolve_ThreeInAColumn_ReturnsWin()
    {
        Board board = new(3, 3);
        IGameOutcomeResolver gameOutcomeResolver = new XInARowWins(3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(1, 0), Piece.X);
        board.SetPiece(new(2, 0), Piece.X);
        Position lastPlayedPosition = new(2, 0);

        GameOutcome outcome = gameOutcomeResolver.Resolve(board, lastPlayedPosition);

        Assert.Equal(GameOutcome.Win, outcome);
    }

    [Fact]
    public void Resolve_ThreeInADiagonal_ReturnsWin()
    {
        Board board = new(3, 3);
        IGameOutcomeResolver gameOutcomeResolver = new XInARowWins(3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(1, 1), Piece.X);
        board.SetPiece(new(2, 2), Piece.X);
        Position lastPlayedPosition = new(2, 2);

        GameOutcome outcome = gameOutcomeResolver.Resolve(board, lastPlayedPosition);

        Assert.Equal(GameOutcome.Win, outcome);
    }

    [Fact]
    public void Resolve_ThreeInAntiDiagonal_ReturnsWin()
    {
        Board board = new(3, 3);
        IGameOutcomeResolver gameOutcomeResolver = new XInARowWins(3);
        board.SetPiece(new(0, 2), Piece.X);
        board.SetPiece(new(1, 1), Piece.X);
        board.SetPiece(new(2, 0), Piece.X);
        Position lastPlayedPosition = new(2, 0);

        GameOutcome outcome = gameOutcomeResolver.Resolve(board, lastPlayedPosition);

        Assert.Equal(GameOutcome.Win, outcome);
    }

    [Fact]
    public void Resolve_TwoInARow_ReturnsInProgress()
    {
        Board board = new(3, 3);
        IGameOutcomeResolver gameOutcomeResolver = new XInARowWins(3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(0, 1), Piece.X);
        Position lastPlayedPosition = new(0, 1);

        GameOutcome outcome = gameOutcomeResolver.Resolve(board, lastPlayedPosition);

        Assert.Equal(GameOutcome.InProgress, outcome);
    }

    [Fact]
    public void Resolve_FullBoard_ReturnsDraw()
    {
        Board board = new(3, 3);
        IGameOutcomeResolver gameOutcomeResolver = new XInARowWins(3);
        board.SetPiece(new(0, 0), Piece.X);
        board.SetPiece(new(0, 1), Piece.O);
        board.SetPiece(new(0, 2), Piece.X);
        board.SetPiece(new(1, 0), Piece.X);
        board.SetPiece(new(1, 1), Piece.O);
        board.SetPiece(new(1, 2), Piece.X);
        board.SetPiece(new(2, 0), Piece.O);
        board.SetPiece(new(2, 1), Piece.X);
        board.SetPiece(new(2, 2), Piece.O);
        Position lastPlayedPosition = new(2, 2);

        GameOutcome outcome = gameOutcomeResolver.Resolve(board, lastPlayedPosition);

        Assert.Equal(GameOutcome.Draw, outcome);
    }
}
