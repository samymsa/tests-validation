using MorpionApp.GameOutcomeResolver;
using MorpionApp.Models;
using MorpionApp.Models.Player;
using MorpionApp.Models.Player.Strategy;
using MorpionApp.NextPlayerStrategy;
using MorpionApp.UI;

namespace MorpionApp.Games;

public abstract class BoardGame(int rows, int columns, IGameOutcomeResolver gameOutcomeResolver, IUserInterface ui, INextPlayerStrategy nextPlayerStrategy)
{
    protected bool Quit = false;
    public Board Board { get; } = new(rows, columns);
    public List<Player> Players { get; } = [
        new Player(Piece.X, new HumanPlayerStrategy(ui)),
        new Player(Piece.O, new AIPlayerStrategy())
    ];
    public int CurrentPlayerIndex { get; protected set; } = 0;
    public Player CurrentPlayer => Players[CurrentPlayerIndex];
    protected IGameOutcomeResolver GameOutcomeResolver { get; } = gameOutcomeResolver;
    protected IUserInterface UI { get; } = ui;
    protected INextPlayerStrategy NextPlayerStrategy { get; } = nextPlayerStrategy;

    public void MainLoop()
    {
        while (!Quit)
        {
            Reset();
            GameLoop();
            if (UI.AskForReplay())
            {
                Quit = false;
            }
        }
    }

    public void Reset()
    {
        Board.RemoveAllPieces();
    }

    public void GameLoop()
    {
        while (!Quit)
        {
            Position lastPlayedPosition = PlayNextMove();
            HandleOutcome(GetOutcome(lastPlayedPosition));
            NextPlayer();
        }
    }

    private void NextPlayer()
    {
        CurrentPlayerIndex = NextPlayerStrategy.GetNextPlayer(Players, CurrentPlayerIndex);
    }

    private Position PlayNextMove()
    {
        Position position = GetNextMove(CurrentPlayer);
        return Play(position, CurrentPlayer);
    }

    protected virtual Position GetNextMove(Player player)
    {
        return player.GetNextMove(Board);
    }

    private Position Play(Position position, Player player)
    {
        Board.SetPiece(position, player.Piece);
        return position;
    }

    private GameOutcome GetOutcome(Position lastPlayedPosition)
    {
        return GameOutcomeResolver.Resolve(Board, lastPlayedPosition);
    }

    private void HandleOutcome(GameOutcome gameOutcome)
    {
        if (gameOutcome == GameOutcome.InProgress) return;

        UI.DisplayBoard(Board);
        Quit = true;

        if (gameOutcome == GameOutcome.Win)
        {
            UI.DisplayWin(CurrentPlayer);
            return;
        }

        UI.DisplayDraw();
    }
}
