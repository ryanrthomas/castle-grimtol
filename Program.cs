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
            Console.WriteLine("You can type 'help' at any time to bring up a list of game commands.");
            Console.WriteLine("Indiana Jones has arrived in New York City after hearing that Klaus Kerner is after Indy's former colleague Sophia Hapgood in search of the lost city of Atlantis. You have to warn her!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("INDY: \"Well, here I am in New York wondering how Sophia Hapgood got mixed up with Nazi spies.\"");
            Console.ForegroundColor = ConsoleColor.Green;

            GameService gm = new GameService();
            gm.PlayHalfSong();
            gm.StartGame();
        }
    }
}
