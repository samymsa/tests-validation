namespace MorpionApp
{
    public class PuissanceQuatre
    {
        const int WIDTH = 7;
        const int HEIGHT = 4;
        public bool quiterJeu = false;
        public bool tourDuJoueur = true;
        public char[,] grille;

        public PuissanceQuatre()
        {
            grille = new char[WIDTH, HEIGHT];
            ResetGrille();
        }

        private void ResetGrille()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    grille[i, j] = ' ';
                }
            }
        }

        public void BoucleJeu()
        {
            while (!quiterJeu)
            {
                ResetGrille();
                while (!quiterJeu)
                {
                    if (tourDuJoueur)
                    {
                        tourJoueur();
                        if (verifVictoire('X'))
                        {
                            finPartie("Le joueur 1 à gagné !");
                            break;
                        }
                    }
                    else
                    {
                        tourJoueur2();
                        if (verifVictoire('O'))
                        {
                            finPartie("Le joueur 2 à gagné !");
                            break;
                        }
                    }
                    tourDuJoueur = !tourDuJoueur;
                    if (verifEgalite())
                    {
                        finPartie("Aucun vainqueur, la partie se termine sur une égalité.");
                        break;
                    }
                }
                if (!quiterJeu)
                {
                    Console.WriteLine("Appuyer sur [Echap] pour quitter, [Entrer] pour rejouer.");
                GetKey:
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            break;
                        case ConsoleKey.Escape:
                            quiterJeu = true;
                            Console.Clear();
                            break;
                        default:
                            goto GetKey;
                    }
                }

            }
        }

        public void tourJoueur()
        {
            var (row, column) = (0, 0);
            bool moved = false;

            while (!quiterJeu && !moved)
            {
                Console.Clear();
                affichePlateau();
                Console.WriteLine();
                Console.WriteLine("Choisir une case valide est appuyer sur [Entrer]");
                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        quiterJeu = true;
                        Console.Clear();
                        break;

                    case ConsoleKey.RightArrow:
                        if (column >= WIDTH - 1)
                        {
                            column = 0;
                        }
                        else
                        {
                            column = column + 1;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (column <= 0)
                        {
                            column = WIDTH - 1;
                        }
                        else
                        {
                            column = column - 1;
                        }
                        break;

                    case ConsoleKey.Enter:
                        row = HEIGHT - 1;
                        while (grille[row, column] is 'X' or 'O')
                        {
                            if (row == 0)
                            {
                                break;
                            }

                            row = row - 1;
                        }
                        if(grille[row, column] is ' ')
                        {
                            grille[row, column] = 'X';
                            moved = true;
                            quiterJeu = false;
                        }
                        break;
                }

            }
        }

        public void tourJoueur2()
        {
            var (row, column) = (0, 0);
            bool moved = false;

            while (!quiterJeu && !moved)
            {
                Console.Clear();
                affichePlateau();
                Console.WriteLine();
                Console.WriteLine("Choisir une case valide est appuyer sur [Entrer]");
                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        quiterJeu = true;
                        Console.Clear();
                        break;

                    case ConsoleKey.RightArrow:
                        if (column >= WIDTH - 1)
                        {
                            column = 0;
                        }
                        else
                        {
                            column = column + 1;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (column <= 0)
                        {
                            column = WIDTH - 1;
                        }
                        else
                        {
                            column = column - 1;
                        }
                        break;

                    case ConsoleKey.Enter:
                        row = HEIGHT - 1;
                        while (grille[row, column] is 'X' or 'O')
                        {
                            if(row == 0)
                            {
                                break;
                            }

                            row = row - 1;
                        }
                        if (grille[row, column] is ' ')
                        {
                            grille[row, column] = 'O';
                            moved = true;
                            quiterJeu = false;
                        }
                        break;
                }
            }
        }

        public void affichePlateau()
        {
            Console.WriteLine();
            Console.WriteLine($" {grille[0, 0]}  |  {grille[0, 1]}  |  {grille[0, 2]}  |  {grille[0, 3]}  |  {grille[0, 4]}  |  {grille[0, 5]}  |  {grille[0, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine($" {grille[1, 0]}  |  {grille[1, 1]}  |  {grille[1, 2]}  |  {grille[1, 3]}  |  {grille[1, 4]}  |  {grille[1, 5]}  |  {grille[1, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine($" {grille[2, 0]}  |  {grille[2, 1]}  |  {grille[2, 2]}  |  {grille[2, 3]}  |  {grille[2, 4]}  |  {grille[2, 5]}  |  {grille[1, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine($" {grille[3, 0]}  |  {grille[3, 1]}  |  {grille[3, 2]}  |  {grille[3, 3]}  |  {grille[3, 4]}  |  {grille[3, 5]}  |  {grille[1, 6]}");
            Console.WriteLine("    |     |     |     |     |     |");
            Console.WriteLine("----+-----+-----+-----+-----+-----+----");
        }

        public bool verifVictoire(char c) =>
             grille[0, 0] == c && grille[1, 0] == c && grille[2, 0] == c && grille[3, 0] == c ||
             grille[0, 1] == c && grille[1, 1] == c && grille[2, 1] == c && grille[3, 1] == c ||
             grille[0, 2] == c && grille[1, 2] == c && grille[2, 2] == c && grille[3, 2] == c ||
             grille[0, 3] == c && grille[1, 3] == c && grille[2, 3] == c && grille[3, 3] == c ||
             grille[0, 4] == c && grille[1, 4] == c && grille[2, 4] == c && grille[3, 4] == c ||
             grille[0, 5] == c && grille[1, 5] == c && grille[2, 5] == c && grille[3, 5] == c ||
             grille[0, 6] == c && grille[1, 6] == c && grille[2, 6] == c && grille[3, 6] == c ||
             grille[0, 0] == c && grille[0, 1] == c && grille[0, 2] == c && grille[0, 3] == c ||
             grille[0, 1] == c && grille[0, 2] == c && grille[0, 3] == c && grille[0, 4] == c ||
             grille[0, 2] == c && grille[0, 3] == c && grille[0, 3] == c && grille[0, 5] == c ||
             grille[0, 3] == c && grille[0, 4] == c && grille[0, 5] == c && grille[0, 6] == c ||
             grille[1, 0] == c && grille[1, 1] == c && grille[1, 2] == c && grille[1, 3] == c ||
             grille[1, 1] == c && grille[1, 2] == c && grille[1, 3] == c && grille[1, 4] == c ||
             grille[1, 2] == c && grille[1, 3] == c && grille[1, 4] == c && grille[1, 5] == c ||
             grille[1, 4] == c && grille[1, 4] == c && grille[1, 5] == c && grille[1, 6] == c ||
             grille[2, 0] == c && grille[2, 1] == c && grille[2, 2] == c && grille[2, 3] == c ||
             grille[2, 1] == c && grille[2, 2] == c && grille[2, 3] == c && grille[2, 4] == c ||
             grille[2, 2] == c && grille[2, 3] == c && grille[2, 3] == c && grille[2, 5] == c ||
             grille[2, 3] == c && grille[2, 4] == c && grille[2, 5] == c && grille[2, 6] == c ||
             grille[3, 0] == c && grille[3, 1] == c && grille[3, 2] == c && grille[3, 3] == c ||
             grille[3, 1] == c && grille[3, 2] == c && grille[3, 3] == c && grille[3, 4] == c ||
             grille[3, 2] == c && grille[3, 3] == c && grille[3, 4] == c && grille[3, 5] == c ||
             grille[3, 3] == c && grille[3, 4] == c && grille[3, 5] == c && grille[3, 6] == c ||
             grille[0, 0] == c && grille[1, 1] == c && grille[2, 2] == c && grille[3, 3] == c ||
             grille[0, 1] == c && grille[1, 2] == c && grille[2, 3] == c && grille[3, 4] == c ||
             grille[0, 2] == c && grille[1, 3] == c && grille[2, 4] == c && grille[3, 5] == c ||
             grille[0, 3] == c && grille[1, 4] == c && grille[2, 5] == c && grille[3, 6] == c ||
             grille[0, 3] == c && grille[1, 2] == c && grille[2, 1] == c && grille[3, 0] == c ||
             grille[0, 4] == c && grille[1, 4] == c && grille[2, 2] == c && grille[3, 1] == c ||
             grille[0, 5] == c && grille[1, 3] == c && grille[2, 3] == c && grille[3, 2] == c ||
             grille[0, 6] == c && grille[1, 5] == c && grille[2, 4] == c && grille[3, 3] == c;

        public bool verifEgalite() =>
            grille[0, 0] != ' ' && grille[0, 1] != ' ' && grille[0, 2] != ' ' && grille[0, 3] != ' ' && grille[0, 4] != ' ' && grille[0, 5] != ' ' && grille[0, 6] != ' ' &&
            grille[1, 0] != ' ' && grille[1, 1] != ' ' && grille[1, 2] != ' ' && grille[1, 3] != ' ' && grille[1, 4] != ' ' && grille[1, 5] != ' ' && grille[1, 6] != ' ' &&
            grille[2, 0] != ' ' && grille[2, 1] != ' ' && grille[1, 2] != ' ' && grille[2, 3] != ' ' && grille[2, 4] != ' ' && grille[2, 5] != ' ' && grille[2, 6] != ' ' &&
            grille[3, 0] != ' ' && grille[3, 1] != ' ' && grille[3, 2] != ' ' && grille[3, 3] != ' ' && grille[3, 4] != ' ' && grille[3, 5] != ' ' && grille[3, 5] != ' ';


        public void finPartie(string msg)
        {
            Console.Clear();
            affichePlateau();
            Console.WriteLine(msg);
        }
    }
}
