using MorpionApp.GameOutcomeResolver;
using MorpionApp.GameSerializer;
using MorpionApp.Models;
using MorpionApp.Models.Player;
using MorpionApp.Models.Player.Strategy;
using MorpionApp.NextMoveStrategy;
using MorpionApp.NextPlayerStrategy;
using MorpionApp.UI;

namespace MorpionApp.Games;

public class BoardGame(
    Board board,
    IGameOutcomeResolver gameOutcomeResolver,
    IUserInterface ui,
    INextPlayerStrategy nextPlayerStrategy,
    INextMoveStrategy nextMoveStrategy,
    IGameSerializer gameSerializer,
    string savePath = "saves/game.save")
{
    private readonly string SavePath = savePath;
    private bool Quit = false;
    public Board Board { get; set; } = board;
    public List<Player> Players { get; } = [
        new Player(Piece.X, new HumanPlayerStrategy(ui)),
        new Player(Piece.O, new AIPlayerStrategy())
    ];
    public int CurrentPlayerIndex { get; set; } = 0;
    private Player CurrentPlayer => Players[CurrentPlayerIndex];
    private readonly IGameOutcomeResolver GameOutcomeResolver = gameOutcomeResolver;
    private readonly IUserInterface UI = ui;
    private readonly INextPlayerStrategy NextPlayerStrategy = nextPlayerStrategy;
    private readonly INextMoveStrategy NextMoveStrategy = nextMoveStrategy;
    private readonly IGameSerializer GameSerializer = gameSerializer;

    // Default constructor is needed for deserialization
    public BoardGame() : this(
        new Board(3, 3),
        new XInARowWins(3),
        new ConsoleUI(),
        new RoundRobin(),
        new UnoccupiedStrategy(),
        new JSONGameSerializer())
    { }

    public void MainLoop()
    {
        Load();
        while (!Quit)
        {
            GameLoop();
            Reset();
            Save();
            if (UI.AskForReplay())
            {
                Quit = false;
            }
        }
    }

    private void Reset()
    {
        Board.RemoveAllPieces();
    }

    private void GameLoop()
    {
        while (!Quit)
        {
            Position lastPlayedPosition = PlayNextMove();
            HandleOutcome(GetOutcome(lastPlayedPosition));
            NextPlayer();
            Save();
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

    private Position GetNextMove(Player player)
    {
        return NextMoveStrategy.GetNextMove(player, Board);
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

    private void Save()
    {
        GameSerializer.Save(SavePath, this);
    }

    private void Load()
    {
        if (!File.Exists(SavePath)) return;
        GameSerializer.Load(SavePath, this);
    }
}
