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
            System.Console.WriteLine(@"
MMMMMMMMMMWXK0OOOOkkO0KXWWMMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMMMNOl:;,,;;;,,,;;:kKNMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMWKo;,''''........':o0WMMMMMMMMMMMMMMMMMMMMMM
MMMMMMNd;,..............,;oKNWMMMMMMMMMMMMMMMMMMMM
MMMMMNkc,'........''.....'':loxOXWMMMMMMMMMMMMMMMM
MMMNkc:;,'''',,,,,'........';:;:cx0kdx0NMMMMMMMMMM
MWKl,,;;,,'...'.........  .';;;:;::...'coxOKNWMMMM
WKc,,,,'''............. ...,,;;,'...........;lx0WM
Nd,,;;,''...............''',,,;:coooc;..';,.''.'oX
Xl,;;;,,,,''......'''',,,'..,oOXWMMMWXOo,',;kKd,'x
Wk:;;;;;;;;;,,,,,,,,,,'.....,d0KXX0OOOOOo,.lXW0;,O
MWKxl:;,,;;;,,,,,,,;codoc;,''''';cloxO0Oo,;dOo;;xW
MMMMNKOkxdlccccccodOKXXXK0000Oxo:,'......',;::dKWM
MMMMMN00000OkOKXXXXNNNWWMMMMMMMWNX0kxxxxkO0KNWMMMM
MMMMMN00KXNNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
            Console.WriteLine("You can type 'help' at any time to bring up a list of game commands.");
            Console.WriteLine("Indiana Jones has arrived in New York City after hearing that Klaus Kerner is after his former colleague Sophia Hapgood in search of the lost city of Atlantis.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("INDY: \"Well, here I am in New York wondering how Sophia Hapgood got mixed up with Nazi spies.\"");
            Console.ForegroundColor = ConsoleColor.Green;

            GameService gm = new GameService();
            gm.StartGame();
        }
    }
}
