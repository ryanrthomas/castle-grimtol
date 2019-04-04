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
        public bool Talking { get; set; }
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
            Talking = true;
            Character character = CurrentRoom.Characters.Find(i => characterName.ToLower() == i.Name.ToLower());
            // System.Console.WriteLine(characterName);
            // int index = 0;
            if (character != null)
            {
                while (Talking)
                {
                    Console.Clear();
                    if (characterName == "stagehand")
                    {
                        if (character.TalkedTo == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Excuse me.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Aha! You must be the new doorman. About time they got rid of Biff, he was such a pushover.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"I want a reading with Ms. Hapgood.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Are you crazy? During the show? Write her a letter!\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else if (character.TalkedTo == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Excuse me.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Take it easy and watch the show.\"");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"Here, my friends, is ATLANTIS, as it might have appeared in its heyday. Glorious, prosperous, socially and technially advanced beyond our wildest dreams! 5,000 years ago, while everyone else still wore animal skins...the mighty spirits of Atlantis dared to build a city where knowledge and power were united in true happiness. Centuries later, the famous philospher Plato wrote about it. He placed Atlantis on a continent out in the deep ocean, and described how it was divided into three circular parts, such as you see here...\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Isn't she something? She'll go on for hours.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else if (character.TalkedTo == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Excuse me.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Shh! She's just coming to the exciting part!\"");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"What befell this serene city? We may never know for sure.\"");
                            Console.WriteLine("\"Was it the sea level, slowly creeping higher? Or the Earth itself, suddenly shifting?\"");
                            Console.WriteLine("\"However it happened, panic must have gripped the citizens on that fateful day when proud Atlantis sank beneath the waves...\"");
                            Console.WriteLine("\"...Or...perhaps it was a volcanic eruption, and SOMETHING remains, even now.\"");
                            Console.WriteLine("\"On some questions, the Great Spirit who guides my thoughts...\"");
                            Console.WriteLine("\"The all-seeing NUR-AB-SAL is silent.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"I've been through this a hundred times -- the woman never stops!\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else if (character.TalkedTo == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Excuse me.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Yeah? What now?\"");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Isn't there something you'd rather be doing?\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Like what? Show business is my whole life!\"");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Don't you have any hobbies?\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Sure, I read.\"");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"What if I gave you something to read?\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"I might take a look.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Excuse me.\"");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Stop pestering me and watch the show, will ya?\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                    }
                    else if (characterName == "ticket taker")
                    {
                        if (character.TalkedTo == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Hello there.\"");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"{characterName.ToUpper()}: \"The show's sold out, sir.\"");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"But--\"");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"{characterName.ToUpper()}: \"No seats, no standing room, no exceptions.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo = -1;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Excuse me.\"");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"{characterName.ToUpper()}: \"Come back next week.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                    }
                    Talking = false;
                    GetUserInput();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"With whom should I talk?\"");
                Console.ForegroundColor = ConsoleColor.Green;
                Talking = false;
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
        public void GameOver()
        {
            Console.Clear();
            System.Console.WriteLine("GAME OVER. Come back and try again soon!");
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
            Room backstage = new Room("BACKSTAGE", "Indy is in the side wing of the stage-left side of the theater. Indy sees Sophia giving her presentation to a packed audience. There is a STAGEHAND watching closely nearby next to a MACHINE with three LEVERS and a BUTTON.");

            // Create all items
            Item magazine = new Item("Magazine", "A copy of 'National Archaeology'. You flip through the pages, looking at a photo of you and Sophia. \"This was taken a long time ago, when I thought we might like each other,\" you say to yourself.", true);
            Item newspaper = new Item("Newspaper", "It's today's paper.", true);

            // Create all npcs
            Character tickettaker = new Character("Ticket taker", "She's counting up the receipts.", 0);
            Character stagehand = new Character("Stagehand", "He looks bored.", 0);

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
            Talking = false;
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
                if (item.Name == "Newspaper")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character != null)
                    {
                        CurrentPlayer.Inventory.Remove(item);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"Here.\"");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("STAGEHAND: \"Well, well, the late edition! I wonder if the Dodgers won? Watch the lights while I find out, okay?\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("The stagehand grabs the newspaper and walks out.");
                        CurrentRoom.Characters.Remove(character);
                    }
                }
                if (item.Name == "Magazine")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"Here.\"");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("STAGEHAND: \"No thanks! I read that thing years ago. I've still got my own copy.\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"I can't use that.\"");
                Console.ForegroundColor = ConsoleColor.Green;

            }
        }
    }
}