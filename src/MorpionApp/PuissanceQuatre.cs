namespace MorpionApp
{
    public class PuissanceQuatre
    {
        const int WIDTH = 7;
        const int HEIGHT = 4;
        const int CELL_WIDTH = 6;
        const int CELL_HEIGHT = 4;
        private bool quit = false;
        private char[,] grid;
        private char[] players = ['X', 'O'];
        private int currentPlayerIndex = 0;
        private int row = 0;
        private int column = 0;

        public PuissanceQuatre()
        {
            grid = new char[HEIGHT, WIDTH];
            ResetGrid();
        }

        private void ResetGrid()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    grid[i, j] = ' ';
                }
            }
        }

        private char GetCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }

        private void SetNextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        private void MoveCursorLeft()
        {
            column = (column + WIDTH - 1) % WIDTH;
        }

        private void MoveCursorRight()
        {
            column = (column + 1) % WIDTH;
        }

        private void UpdateCursorPositon()
        {
            Console.SetCursorPosition(column * CELL_WIDTH + 1, row * CELL_HEIGHT + 1);
        }

        private ConsoleKey GetInput()
        {
            return Console.ReadKey(true).Key;
        }

        private void HandleEnterKey()
        {
            for (int currentRow = HEIGHT - 1; currentRow >= 0; currentRow--)
            {
                if (grid[currentRow, column] is ' ')
                {
                    grid[currentRow, column] = GetCurrentPlayer();
                    SetNextPlayer();
                    break;
                }
            }
        }

        private void HandleEscapeKey()
        {
            quit = true;
            Console.Clear();
        }

        private void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
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

        private void HandleRestart(ConsoleKey key)
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

        private void AskForInput()
        {
            Console.WriteLine("Choisir une case valide est appuyer sur [Entrer]");
        }

        private ConsoleKey AskForRestart()
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

        private void GameLoop()
        {
            while (!quit)
            {
                char currentPlayer = GetCurrentPlayer();
                affichePlateau();
                AskForInput();
                UpdateCursorPositon();
                HandleInput(GetInput());
                if (verifVictoire(currentPlayer))
                {
                    finPartie($"Le joueur {currentPlayer} à gagné !");
                    return;
                }
                if (verifEgalite())
                {
                    finPartie("Aucun vainqueur, la partie se termine sur une égalité.");
                    return;
                }
            }
            affichePlateau();
        }

        public void MainLoop()
        {
            while (!quit)
            {
                ResetGrid();
                GameLoop();
                if (quit)
                {
                    return;
                }
                HandleRestart(AskForRestart());
            }
        }


        public void affichePlateau()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($" {grid[0, 0]}  |  {grid[0, 1]}  |  {grid[0, 2]}  |  {grid[0, 3]}  |  {grid[0, 4]}  |  {grid[0, 5]}  |  {grid[0, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine($" {grid[1, 0]}  |  {grid[1, 1]}  |  {grid[1, 2]}  |  {grid[1, 3]}  |  {grid[1, 4]}  |  {grid[1, 5]}  |  {grid[1, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine($" {grid[2, 0]}  |  {grid[2, 1]}  |  {grid[2, 2]}  |  {grid[2, 3]}  |  {grid[2, 4]}  |  {grid[2, 5]}  |  {grid[1, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine($" {grid[3, 0]}  |  {grid[3, 1]}  |  {grid[3, 2]}  |  {grid[3, 3]}  |  {grid[3, 4]}  |  {grid[3, 5]}  |  {grid[1, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine();
        }

        public bool verifVictoire(char c) =>
             grid[0, 0] == c && grid[1, 0] == c && grid[2, 0] == c && grid[3, 0] == c ||
             grid[0, 1] == c && grid[1, 1] == c && grid[2, 1] == c && grid[3, 1] == c ||
             grid[0, 2] == c && grid[1, 2] == c && grid[2, 2] == c && grid[3, 2] == c ||
             grid[0, 3] == c && grid[1, 3] == c && grid[2, 3] == c && grid[3, 3] == c ||
             grid[0, 4] == c && grid[1, 4] == c && grid[2, 4] == c && grid[3, 4] == c ||
             grid[0, 5] == c && grid[1, 5] == c && grid[2, 5] == c && grid[3, 5] == c ||
             grid[0, 6] == c && grid[1, 6] == c && grid[2, 6] == c && grid[3, 6] == c ||
             grid[0, 0] == c && grid[0, 1] == c && grid[0, 2] == c && grid[0, 3] == c ||
             grid[0, 1] == c && grid[0, 2] == c && grid[0, 3] == c && grid[0, 4] == c ||
             grid[0, 2] == c && grid[0, 3] == c && grid[0, 3] == c && grid[0, 5] == c ||
             grid[0, 3] == c && grid[0, 4] == c && grid[0, 5] == c && grid[0, 6] == c ||
             grid[1, 0] == c && grid[1, 1] == c && grid[1, 2] == c && grid[1, 3] == c ||
             grid[1, 1] == c && grid[1, 2] == c && grid[1, 3] == c && grid[1, 4] == c ||
             grid[1, 2] == c && grid[1, 3] == c && grid[1, 4] == c && grid[1, 5] == c ||
             grid[1, 4] == c && grid[1, 4] == c && grid[1, 5] == c && grid[1, 6] == c ||
             grid[2, 0] == c && grid[2, 1] == c && grid[2, 2] == c && grid[2, 3] == c ||
             grid[2, 1] == c && grid[2, 2] == c && grid[2, 3] == c && grid[2, 4] == c ||
             grid[2, 2] == c && grid[2, 3] == c && grid[2, 3] == c && grid[2, 5] == c ||
             grid[2, 3] == c && grid[2, 4] == c && grid[2, 5] == c && grid[2, 6] == c ||
             grid[3, 0] == c && grid[3, 1] == c && grid[3, 2] == c && grid[3, 3] == c ||
             grid[3, 1] == c && grid[3, 2] == c && grid[3, 3] == c && grid[3, 4] == c ||
             grid[3, 2] == c && grid[3, 3] == c && grid[3, 4] == c && grid[3, 5] == c ||
             grid[3, 3] == c && grid[3, 4] == c && grid[3, 5] == c && grid[3, 6] == c ||
             grid[0, 0] == c && grid[1, 1] == c && grid[2, 2] == c && grid[3, 3] == c ||
             grid[0, 1] == c && grid[1, 2] == c && grid[2, 3] == c && grid[3, 4] == c ||
             grid[0, 2] == c && grid[1, 3] == c && grid[2, 4] == c && grid[3, 5] == c ||
             grid[0, 3] == c && grid[1, 4] == c && grid[2, 5] == c && grid[3, 6] == c ||
             grid[0, 3] == c && grid[1, 2] == c && grid[2, 1] == c && grid[3, 0] == c ||
             grid[0, 4] == c && grid[1, 4] == c && grid[2, 2] == c && grid[3, 1] == c ||
             grid[0, 5] == c && grid[1, 3] == c && grid[2, 3] == c && grid[3, 2] == c ||
             grid[0, 6] == c && grid[1, 5] == c && grid[2, 4] == c && grid[3, 3] == c;

        public bool verifEgalite() =>
            grid[0, 0] != ' ' && grid[0, 1] != ' ' && grid[0, 2] != ' ' && grid[0, 3] != ' ' && grid[0, 4] != ' ' && grid[0, 5] != ' ' && grid[0, 6] != ' ' &&
            grid[1, 0] != ' ' && grid[1, 1] != ' ' && grid[1, 2] != ' ' && grid[1, 3] != ' ' && grid[1, 4] != ' ' && grid[1, 5] != ' ' && grid[1, 6] != ' ' &&
            grid[2, 0] != ' ' && grid[2, 1] != ' ' && grid[1, 2] != ' ' && grid[2, 3] != ' ' && grid[2, 4] != ' ' && grid[2, 5] != ' ' && grid[2, 6] != ' ' &&
            grid[3, 0] != ' ' && grid[3, 1] != ' ' && grid[3, 2] != ' ' && grid[3, 3] != ' ' && grid[3, 4] != ' ' && grid[3, 5] != ' ' && grid[3, 5] != ' ';


        public void finPartie(string msg)
        {
            affichePlateau();
            Console.WriteLine(msg);
        }
    }
}
