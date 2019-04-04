using System;
using indygame.Project;

namespace indygame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Indiana Jones Checkpoint Project!");
            Console.WriteLine("You have arrived in New York City after hearing that Klaus Kerner is after your former collague Sophia Hapgood in search of the lost city of Atlantis.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Indy: \"Well, here I am in New York wondering how Sophia Hapgood got mixed up with Nazi spies.\"");
            Console.ForegroundColor = ConsoleColor.Green;

            GameService gm = new GameService();
            gm.StartGame();
        }
    }
}
