using MorpionApp.GameOutcomeResolver;
using MorpionApp.Models;
using MorpionApp.Models.Player;
using MorpionApp.Models.Player.Strategy;
using MorpionApp.NextPlayerStrategy;
using MorpionApp.UI;

namespace MorpionApp.Games;

public abstract class BoardGame(int rows, int columns, IGameOutcomeResolver gameOutcomeResolver, IUserInterface ui, INextPlayerStrategy nextPlayerStrategy)
{
    protected bool quit = false;
    protected Position lastPlayedPosition = new(0, 0);
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
        while (!quit)
        {
            RemoveAllPieces();
            GameLoop();
            if (UI.AskForReplay())
            {
                quit = false;
            }
        }
    }

    public void RemoveAllPieces()
    {
        Board.RemoveAllPieces();
    }

    public void GameLoop()
    {
        while (!quit)
        {
            Player player = CurrentPlayer;
            lastPlayedPosition = PlayNextMove(player);
            NextPlayer();
            HandleOutcome(GetOutcome(), player);
        }
    }

    private void NextPlayer()
    {
        CurrentPlayerIndex = NextPlayerStrategy.GetNextPlayer(Players, CurrentPlayerIndex);
    }

    private Position Play(Position position, Player player)
    {
        Board.SetPiece(position, player.Piece);
        return position;
    }

    protected virtual Position? GetNextMove(Player player)
    {
        return player.GetNextMove(Board);
    }

    private Position PlayNextMove(Player player)
    {
        Position position = GetNextMove(player) ?? throw new InvalidOperationException("No valid move found.");
        return Play(position, player);
    }

    private GameOutcome GetOutcome()
    {
        return GameOutcomeResolver.Resolve(Board, lastPlayedPosition);
    }

    private void HandleOutcome(GameOutcome gameOutcome, Player lastPlayer)
    {
        switch (gameOutcome)
        {
            case GameOutcome.Win:
                UI.DisplayBoard(Board);
                UI.DisplayWin(lastPlayer);
                quit = true;
                break;
            case GameOutcome.Draw:
                UI.DisplayBoard(Board);
                UI.DisplayDraw();
                quit = true;
                break;
            default:
                break;
        }
    }
}
