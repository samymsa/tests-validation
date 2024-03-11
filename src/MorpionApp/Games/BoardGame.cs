using MorpionApp.GameOutcomeResolver;
using MorpionApp.Models;

namespace MorpionApp.Games;

public abstract class BoardGame(int rows, int columns, IGameOutcomeResolver gameOutcomeResolver)
{
    const int CELL_WIDTH = 5;
    const int CELL_HEIGHT = 3;
    protected bool quit = false;
    protected int row = 0;
    protected int column = 0;
    protected Position lastPlayedPosition = new(0, 0);

    public int Rows { get; } = rows;
    public int Columns { get; } = columns;
    public Board Board { get; } = new(rows, columns);
    public List<Player> Players { get; } = [new Player(Piece.X), new Player(Piece.O)];
    public int CurrentPlayerIndex { get; protected set; } = 0;
    public Player CurrentPlayer => Players[CurrentPlayerIndex];
    protected IGameOutcomeResolver GameOutcomeResolver { get; } = gameOutcomeResolver;

    public void MainLoop()
    {
        while (!quit)
        {
            RemoveAllPieces();
            GameLoop();
            if (quit)
            {
                return;
            }
            HandleRestart(AskForRestart());
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
            DisplayBoard();
            AskForInput();
            UpdateCursorPosition();
            HandleInput(GetInput());
            HandleOutcome(GetOutcome(), player);
        }
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
                EndGame($"Le joueur {lastPlayer.Piece} a gagné !");
                quit = true;
                break;
            case GameOutcome.Draw:
                EndGame("Aucun vainqueur, la partie se termine sur une égalité.");
                quit = true;
                break;
            default:
                break;
        }
    }

    public void AskForInput()
    {
        Console.WriteLine("Choisir une case valide est appuyer sur [Entrer]");
    }

    public ConsoleKey AskForRestart()
    {
        ConsoleKey key;
        do
        {
            Console.WriteLine("Appuyer sur [Echap] pour quitter, [Entrer] pour rejouer.");
            key = Console.ReadKey(true).Key;
        }
        while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
        return key;
    }

    public void HandleRestart(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.Escape:
                quit = true;
                break;
            default:
                break;
        }
    }

    public void UpdateCursorPosition()
    {
        Console.SetCursorPosition(column * (CELL_WIDTH + 1) + CELL_WIDTH / 2, row * (CELL_HEIGHT + 1) + CELL_HEIGHT / 2);
    }

    public void MoveCursorUp()
    {
        row = (row + Rows - 1) % Rows;
    }

    public void MoveCursorDown()
    {
        row = (row + 1) % Rows;
    }

    public void MoveCursorLeft()
    {
        column = (column + Columns - 1) % Columns;
    }

    public void MoveCursorRight()
    {
        column = (column + 1) % Columns;
    }

    public virtual void HandleInput(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                MoveCursorUp();
                break;
            case ConsoleKey.DownArrow:
                MoveCursorDown();
                break;
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

    public virtual void HandleEnterKey()
    {
        Position position = new(row, column);
        if (!Board.IsOccupied(position))
        {
            Board.SetPiece(position, CurrentPlayer.Piece);
            lastPlayedPosition = position;
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        }
    }

    public void HandleEscapeKey()
    {
        quit = true;
        Console.Clear();
    }

    public ConsoleKey GetInput()
    {
        return Console.ReadKey(true).Key;
    }

    public void EndGame(string message)
    {
        DisplayBoard();
        Console.WriteLine(message);
    }

    public void DisplayBoard()
    {
        Console.Clear();

        for (int row = 0; row < Rows; row++)
        {
            if (row > 0)
            {
                var dashes = Enumerable.Repeat(new string('-', CELL_WIDTH), Columns).ToArray();
                Console.WriteLine(string.Join('+', dashes));
            }

            for (int line = 0; line < CELL_HEIGHT; line++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (column > 0)
                    {
                        Console.Write("|");
                    }
                    var spaces = new string(' ', CELL_WIDTH / 2);
                    var piece = Board.GetPiece(new(row, column));
                    var pieceString = piece?.ToString() ?? " ";
                    var separator = line == CELL_HEIGHT / 2 ? pieceString : " ";
                    Console.Write($"{spaces}{separator}{spaces}");
                }
                Console.WriteLine();
            }
        }
    }
}
