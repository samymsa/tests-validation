using MorpionApp.Models;
using MorpionApp.Models.Player;

namespace MorpionApp.UI;

public class ConsoleUI : IUserInterface
{
    private const int CELL_WIDTH = 5;
    private const int CELL_HEIGHT = 3;
    private int CursorRow = 0;
    private int CursorColumn = 0;

    public Position AskForPosition(Board board)
    {
        Position? position;
        do
        {
            DisplayBoard(board);
            DisplayMessage("Choisir une case valide est appuyer sur [Entrer]");
            UpdateCursorPosition();
            ConsoleKey key = Console.ReadKey(true).Key;
            position = HandleKey(board, key);
        } while (position is null || board.IsOccupied(position));
        return position;
    }

    private Position? HandleKey(Board board, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                MoveCursorUp(board);
                break;
            case ConsoleKey.DownArrow:
                MoveCursorDown(board);
                break;
            case ConsoleKey.LeftArrow:
                MoveCursorLeft(board);
                break;
            case ConsoleKey.RightArrow:
                MoveCursorRight(board);
                break;
            case ConsoleKey.Enter:
                return new(CursorRow, CursorColumn);
            default:
                break;
        }
        return null;
    }

    private void MoveCursorUp(Board board)
    {
        CursorRow = (CursorRow + board.RowsCount - 1) % board.RowsCount;
    }

    private void MoveCursorDown(Board board)
    {
        CursorRow = (CursorRow + 1) % board.RowsCount;
    }

    private void MoveCursorLeft(Board board)
    {
        CursorColumn = (CursorColumn + board.ColumnsCount - 1) % board.ColumnsCount;
    }

    private void MoveCursorRight(Board board)
    {
        CursorColumn = (CursorColumn + 1) % board.ColumnsCount;
    }

    private void UpdateCursorPosition()
    {
        Console.SetCursorPosition(CursorColumn * (CELL_WIDTH + 1) + CELL_WIDTH / 2, CursorRow * (CELL_HEIGHT + 1) + CELL_HEIGHT / 2);
    }

    public bool AskForReplay()
    {
        ConsoleKey key;
        do
        {
            DisplayMessage("Appuyer sur [Echap] pour quitter, [Entrer] pour rejouer.");
            key = Console.ReadKey(true).Key;
        }
        while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
        return key == ConsoleKey.Enter;
    }

    public void DisplayDraw()
    {
        DisplayMessage("Aucun vainqueur, la partie se termine sur une égalité.");
        
    }

    public void DisplayWin(Player player)
    {
        DisplayMessage($"Le joueur {player.Piece} a gagné !");
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayBoard(Board board)
    {
        Console.Clear();
        for (int row = 0; row < board.RowsCount; row++)
        {
            DisplayBoardRow(board, row);
        }
    }

    private void DisplayBoardRow(Board board, int row)
    {
        if (row > 0)
        {
            var dashes = Enumerable.Repeat(new string('-', CELL_WIDTH), board.ColumnsCount).ToArray();
            Console.WriteLine(string.Join('+', dashes));
        }

        for (int line = 0; line < CELL_HEIGHT; line++)
        {
            for (int column = 0; column < board.ColumnsCount; column++)
            {
                if (column > 0)
                {
                    Console.Write("|");
                }
                var spaces = new string(' ', CELL_WIDTH / 2);
                var piece = board.GetPiece(new(row, column));
                var pieceString = piece?.ToString() ?? " ";
                var separator = line == CELL_HEIGHT / 2 ? pieceString : " ";
                Console.Write($"{spaces}{separator}{spaces}");
            }
            Console.WriteLine();
        }
    }

}