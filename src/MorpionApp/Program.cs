namespace MorpionApp
{
    public class Program
    {
        private static ConsoleKey GetInput(ConsoleKey[] keys, string message)
        {
            ConsoleKey key;
            do
            {
                Console.WriteLine(message);
                key = Console.ReadKey(true).Key;
            } while (!keys.Contains(key));
            return key;
        }


        public static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKey key = GetInput([ConsoleKey.X, ConsoleKey.P, ConsoleKey.Escape], "Jouer à quel jeu ? Taper [X] pour le morpion et [P] pour le puissance 4 ou [Echap] pour quitter.");
                switch (key)
                {
                    case ConsoleKey.X:
                        Morpion morpion = new Morpion();
                        morpion.MainLoop();
                        break;
                    case ConsoleKey.P:
                        PuissanceQuatre puissanceQuatre = new PuissanceQuatre();
                        puissanceQuatre.MainLoop();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }

                key = GetInput([ConsoleKey.R, ConsoleKey.Escape], "Jouer à un autre jeu ? Taper [R] pour changer de jeu. Taper [Echap] pour quitter.");
                switch (key)
                {
                    case ConsoleKey.R:
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}
