using System;
using System.Collections.Generic;
using indygame.Project.Interfaces;
using indygame.Project.Models;

namespace indygame.Project
{
    public class GameService : IGameService
    {
        public IRoom CurrentRoom { get; set; }
        public bool Playing { get; set; }
        public Player CurrentPlayer { get; set; }

        public void GetUserInput()
        {
            System.Console.Write("What would you like to do? ");
            string[] inputArr = System.Console.ReadLine().ToLower().Split(" ");
            string command = inputArr[0];
            string option = "";
            if (inputArr.Length > 1)
            {
                option = inputArr[1];
                if (inputArr.Length > 2)
                {
                    option += " " + inputArr[2];
                }
            }
            switch (command)
            {
                case "go":
                    Go(option);
                    break;
                case "look":
                case "look around":
                    Look();
                    break;
                case "talk":
                case "talk to":
                    Talk(option);
                    break;
                case "i":
                case "inv":
                case "inventory":
                    Inventory();
                    break;
                case "get":
                case "pick up":
                case "take":
                    TakeItem(option);
                    break;
                case "use":
                    UseItem(option);
                    break;
                case "help":
                    Help();
                    break;
                case "reset":
                case "restart":
                    Reset();
                    break;
                case "q":
                case "quit":
                    Quit();
                    break;
                default:
                    System.Console.WriteLine("I don't recognize that command. Please try again.");
                    GetUserInput();
                    break;
            }
        }

        public void Go(string option)
        {
            Console.Clear();
            if (!Enum.TryParse(option, out Direction direction))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"I don't think that will work.\"");
                Console.ForegroundColor = ConsoleColor.Green;
                return;
            }
            Console.Clear();
            if (CurrentRoom.NearbyRooms.ContainsKey(direction))
            {
                CurrentRoom = CurrentRoom.NearbyRooms[direction];
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"I can't go that way.\"");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        public void Help()
        {
            Console.Clear();
            Console.WriteLine("This is a text-based adventure game which relies heavily on experimenting with text commands.");
            Console.WriteLine("- Use cardinal directions (north, south, east, west) to move from place to place.");
            Console.WriteLine("- LOOK at everything (look statue).");
            Console.WriteLine("- GET everything you can (get bottle).");
            Console.WriteLine("- USE things (use lever).");
            Console.WriteLine("- Type INV to see your inventory.");
            Console.WriteLine("- Use the UP ARROW to cycle through your previous commands.");
            GetUserInput();
        }

        public void Inventory()
        {
            Console.Clear();
            System.Console.WriteLine("Inventory:");
            for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
            {
                System.Console.WriteLine($"- {CurrentPlayer.Inventory[i].Name}");
            }
            GetUserInput();
        }

        public void Talk(string characterName)
        {
            Console.Clear();
            Character character = CurrentRoom.Characters.Find(i => characterName.ToLower() == i.Name.ToLower());
            // System.Console.WriteLine(characterName);
            if (character != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("INDY: \"Hello there.\"");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("TICKET TAKER: \"The show's sold out, sir.\"");
                Console.WriteLine("TICKET TAKER: \"No seats, no standing room, no exceptions.\"");
                Console.ForegroundColor = ConsoleColor.Green;
                GetUserInput();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"There's no one to talk to here.\"");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        public void Look()
        {
            Console.Clear();
            System.Console.WriteLine($"{CurrentRoom.Description}");
            GetUserInput();
        }

        public void Quit()
        {
            Console.Clear();
            System.Console.WriteLine("You have quit the game. Come back and try again soon!");
            Playing = false;
        }

        public void Reset()
        {
            StartGame();
        }

        public void Setup()
        {
            // Create all rooms
            Room boxoffice = new Room("THEATER BOX OFFICE", "Indy is in front of the theater with a large MARQUEE where Sophia's psychic show is taking place. A TICKET TAKER sits in a box office. The only route is to the SOUTH down the street to an ALLEYWAY.");
            Room alleyway = new Room("ALLEYWAY", "Indy is at the corner of the theater. There is a closed newspaper stand nearby with today's NEWSPAPER available, along with a PHONE BOOTH adjacent to it. Around the corner to the EAST lies the back of the theater.");
            Room backdoor = new Room("BACK DOOR OF THEATER", "Indy is at the back of the theater with a DOOR in front of you - it looks like it may lead BACKSTAGE. To the west is the ALLEYWAY. To the east is an area with many BOXES.");
            Room fireescape = new Room("FIRE ESCAPE", "Past the back door, Indy sees a fire escape LADDER. However, there are dozens of LARGE BOXES in the way.");
            Room backstage = new Room("BACKSTAGE", "Indy is in the side wing of the stage-left side of the theater. Indy sees Sophia giving her presentation to a packed audience. There is a STAGEHAND watching closely nearby next to a MACHINE with three BUTTONS and a LEVER.");

            // Create all items
            Item magazine = new Item("'National Archaeology' magazine", "You flip through the pages, looking at a photo of you and Sophia. \"This was taken a long time ago, when I thought we might like each other,\" you say to yourself.");
            Item newspaper = new Item("Newspaper", "It's today's paper.");

            // Create all npcs
            Character tickettaker = new Character("Ticket taker", "She's counting up the receipts.");
            Character stagehand = new Character("Stagehand", "He looks bored.");

            // Establish relationships
            //ROOMS
            boxoffice.AddNearbyRooms(Direction.south, alleyway);
            alleyway.AddNearbyRooms(Direction.north, boxoffice);
            alleyway.AddNearbyRooms(Direction.east, backdoor);
            backdoor.AddNearbyRooms(Direction.east, fireescape);
            backdoor.AddNearbyRooms(Direction.north, backstage);
            backdoor.AddNearbyRooms(Direction.west, alleyway);
            fireescape.AddNearbyRooms(Direction.north, backstage);
            fireescape.AddNearbyRooms(Direction.west, backdoor);
            backstage.AddNearbyRooms(Direction.south, backdoor);
            //ITEMS
            alleyway.Items.Add(newspaper);
            //CHARACTERS
            boxoffice.Characters.Add(tickettaker);
            backstage.Characters.Add(stagehand);

            CurrentRoom = boxoffice;
            Playing = true;
            CurrentPlayer = new Player();
            CurrentPlayer.Inventory.Add(magazine);
        }

        public void StartGame()
        {
            Setup();
            while (Playing)
            {
                System.Console.WriteLine($"{CurrentRoom.Name}: {CurrentRoom.Description}");
                GetUserInput();
            }
        }

        public void TakeItem(string itemName)
        {
            Console.Clear();
            Item item = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
            if (item != null)
            {
                CurrentPlayer.Inventory.Add(item);
                CurrentRoom.Items.Remove(item);
                System.Console.WriteLine($"Indy picked up the {itemName}.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"There's nothing to pick up here.\"");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        public void UseItem(string itemName)
        {
            Console.Clear();
            Item item = CurrentPlayer.Inventory.Find(i => itemName.ToLower() == i.Name.ToLower());
            if (item != null)
            {
                CurrentPlayer.Inventory.Remove(item);
                System.Console.WriteLine($"Indy used the {itemName}.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"There's nothing to use here.\"");
                Console.ForegroundColor = ConsoleColor.Green;

            }
        }
    }
}