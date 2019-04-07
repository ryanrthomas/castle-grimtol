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
                case "lookat":
                case "examine":
                    LookAt(option);
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
                case "give":
                    GiveItem(option);
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
            Console.WriteLine("This is a text-based adventure game which relies heavily on text commands.");
            Console.WriteLine("- Use cardinal directions (NORTH, SOUTH, EAST, WEST) to move from place to place.");
            Console.WriteLine("- LOOK to get the room description again.");
            Console.WriteLine("- LOOKAT at everything (lookat statue).");
            Console.WriteLine("- GET everything you can (get bottle).");
            Console.WriteLine("- TALK to everyone you can (talk soldier). You may get different results from talking to people repeatedly.");
            Console.WriteLine("- USE things (use switch).");
            Console.WriteLine("- GIVE things away that are in your inventory (give present).");
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
                            Console.WriteLine("SOPHIA: \"Here, my friends, is ATLANTIS, as it might have appeared in its heyday. Glorious, prosperous, socially and technically advanced beyond our wildest dreams! 5,000 years ago, while everyone else still wore animal skins...the mighty spirits of Atlantis dared to build a city where knowledge and power were united in true happiness. Centuries later, the famous philosopher Plato wrote about it. He placed Atlantis on a continent out in the deep ocean, and described how it was divided into three circular parts, such as you see here...\"");
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
                            Console.WriteLine("SOPHIA: \"What befell this serene city? We may never know for sure. Was it the sea level, slowly creeping higher? Or the Earth itself, suddenly shifting? However it happened, panic must have gripped the citizens on that fateful day when proud Atlantis sank beneath the waves... Or... perhaps it was a volcanic eruption, and SOMETHING remains, even now. On some questions, the Great Spirit who guides my thoughts... The all-seeing NUR-AB-SAL is silent.\"");
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
                            Console.WriteLine("INDY: \"What if I give you something to read?\"");
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
                    else if (characterName == "sophia")
                    {
                        if (character.TalkedTo < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Hold on! She's still talking. Don't try that again... I've got my eye on you...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Wait just a minute... You're not the doorman! How'd you get in?\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("Indy is kicked out of the theater. There's no chance he'll get back in.");
                            GameOver();
                            break;
                        }

                    }
                    else if (characterName == "biff")
                    {
                        if (character.TalkedTo == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("BIFF: \"Whaddya want, pal? This ain't no ticket office.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (character.TalkedTo == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("BIFF: \"You again? Now what?\"");
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
            Console.WriteLine($"{CurrentRoom.Description}");
            GetUserInput();
        }

        public void LookAt(string name)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Character character = CurrentRoom.Characters.Find(i => name.ToLower() == i.Name.ToLower());
            Item item = CurrentRoom.Items.Find(i => name.ToLower() == i.Name.ToLower());
            Item invItem = CurrentPlayer.Inventory.Find(i => name.ToLower() == i.Name.ToLower());
            if (character != null)
            {
                Console.WriteLine($"{character.Description}");
            }
            else if (item != null)
            {
                Console.WriteLine($"{item.Description}");
            }
            else if (invItem != null)
            {
                Console.WriteLine($"{invItem.Description}");
            }
            else
            {
                Console.WriteLine("INDY: \"There's nothing to look at.\"");
            }
            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("GAME OVER. Come back and try again soon!");
            Console.ForegroundColor = ConsoleColor.Green;
            Playing = false;
        }

        public void Reset()
        {
            StartGame();
        }

        public void Setup()
        {
            // Create all rooms
            Room boxoffice = new Room("THEATER BOX OFFICE", "Indy is in front of the theater where Sophia's show is taking place. The theatre has DOORS that lead in with a large MARQUEE hanging above. A TICKET TAKER sits in a box office. The only route is to the SOUTH down the street to an ALLEYWAY.");
            Room alleyway = new Room("ALLEYWAY", "Indy is at the corner of the theater. There is a closed newspaper stand nearby with today's NEWSPAPER available, along with a PHONE BOOTH adjacent to it. Around the corner to the EAST lies the back of the theater.");
            Room backdoor = new Room("BACK DOOR OF THEATER", "Indy is at the back of the theater with a DOOR in front of you - it looks like it may lead BACKSTAGE. To the west is the ALLEYWAY. To the east is an area with many BOXES.");
            Room fireescape = new Room("FIRE ESCAPE", "Past the back door, Indy sees a fire escape LADDER. However, there are dozens of LARGE BOXES in the way.");
            Room backstage = new Room("BACKSTAGE", "Indy is in the side wing of the stage-left side of the theater. Indy sees Sophia giving her presentation to a packed audience. There is a STAGEHAND watching closely nearby next to a machine, attached to a ghost prop, with a LEFT LEVER, MIDDLE LEVER, RIGHT LEVER and a BUTTON.");

            // Create all items
            Item magazine = new Item("Magazine", "INDY: \"It's an old copy of National Archaeology. This photo with Sophia was taken a long time ago, when I thought we might like each other,\" you say to yourself.", true, 0);
            Item newspaper = new Item("Newspaper", "INDY: \"It's today's paper.\"", true, 0);
            Item leftLever = new Item("Left lever", "INDY: \"This is the left lever.\"", false, 0);
            Item middleLever = new Item("Middle lever", "INDY: \"This is the middle lever.\"", false, 0);
            Item rightLever = new Item("Right lever", "INDY: \"This is the right lever.\"", false, 0);
            Item button = new Item("Button", "INDY: \"This button operates the machine.\"", false, 0);
            Item marquee = new Item("Marquee", "INDY: \"It reads: MADAME SOPHIA TONIGHT. Sophia always did want her name in lights.\"", false, 0);
            Item phonebooth = new Item("Phone booth", "INDY: \"It's just a phone booth.\"", false, 0);
            Item ladder = new Item("Ladder", "INDY: \"Looks like it might lead backstage.\"", false, 0);
            Item doors = new Item("Doors", "INDY: \"They're the front doors to the theater.\"", false, 0);


            // Create all npcs
            Character tickettaker = new Character("Ticket taker", "INDY: \"She's counting up the receipts.\"", 0);
            Character stagehand = new Character("Stagehand", "INDY: \"He looks bored.\"", 0);
            Character sophia = new Character("Sophia", "INDY: \"Still beautiful, still impossible.\"", 0);
            Character biff = new Character("Biff", "INDY: \"The bigger they are, well... you know.\"", 0);

            // Establish relationships
            // ROOMS
            boxoffice.AddNearbyRooms(Direction.south, alleyway);
            alleyway.AddNearbyRooms(Direction.north, boxoffice);
            alleyway.AddNearbyRooms(Direction.east, backdoor);
            backdoor.AddNearbyRooms(Direction.east, fireescape);
            backdoor.AddNearbyRooms(Direction.north, backstage);
            backdoor.AddNearbyRooms(Direction.west, alleyway);
            fireescape.AddNearbyRooms(Direction.north, backstage);
            fireescape.AddNearbyRooms(Direction.west, backdoor);
            backstage.AddNearbyRooms(Direction.south, backdoor);
            // ITEMS
            boxoffice.Items.Add(marquee);
            boxoffice.Items.Add(doors);
            alleyway.Items.Add(newspaper);
            alleyway.Items.Add(phonebooth);
            backstage.Items.Add(leftLever);
            backstage.Items.Add(middleLever);
            backstage.Items.Add(rightLever);
            backstage.Items.Add(button);
            fireescape.Items.Add(ladder);
            // CHARACTERS
            boxoffice.Characters.Add(tickettaker);
            backstage.Characters.Add(stagehand);
            backdoor.Characters.Add(biff);
            backstage.Characters.Add(sophia);

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
                if (item.CanTake == true)
                {
                    CurrentPlayer.Inventory.Add(item);
                    CurrentRoom.Items.Remove(item);
                    System.Console.WriteLine($"Indy picks up the {itemName}.");
                    if (itemName.ToLower() == "newspaper")
                    {
                        CurrentRoom.Description = "Indy is at the corner of the theater. There is a closed newspaper stand, along with a PHONE BOOTH adjacent to it. Around the corner to the EAST lies the back of the theater.";
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.WriteLine("INDY: \"I can't pick that up.\"");
                    Console.ForegroundColor = ConsoleColor.Green;
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"There's nothing to pick up here.\"");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        public void GiveItem(string itemName)
        {
            Console.Clear();
            Item item = CurrentPlayer.Inventory.Find(i => itemName.ToLower() == i.Name.ToLower());
            if (item != null)
            {
                if (item.Name == "Newspaper")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character != null && character.TalkedTo > 2)
                    {
                        CurrentPlayer.Inventory.Remove(item);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"Here.\"");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("STAGEHAND: \"Well, well, the late edition! I wonder if the Dodgers won? Watch the lights while I find out, okay?\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("The stagehand grabs the newspaper and walks out.");
                        CurrentRoom.Characters.Remove(character);
                        CurrentRoom.Description = "Indy is in the side wing of the stage-left side of the theater. Indy sees Sophia giving her presentation to a packed audience. Nearby is a machine, attached to a ghost prop, with a LEFT LEVER, MIDDLE LEVER, RIGHT LEVER and a BUTTON.";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"Excuse me--\"");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("STAGEHAND: \"Stop pestering me and watch the show, will ya?\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                if (item.Name == "Magazine")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character != null && character.TalkedTo > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"Here.\"");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("STAGEHAND: \"No thanks! I read that thing years ago. I've still got my own copy.\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"Excuse me--\"");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("STAGEHAND: \"Stop pestering me and watch the show, will ya?\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("INDY: \"I can't give that away.\"");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        public void UseItem(string itemName)
        {
            Console.Clear();
            Item item = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
            if (item != null)
            {

                if (itemName.ToLower() == "button")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character == null)
                    {
                        Item leftlever = CurrentRoom.Items.Find(i => i.Name == "Left lever");
                        Item middlelever = CurrentRoom.Items.Find(i => i.Name == "Middle lever");
                        Item rightlever = CurrentRoom.Items.Find(i => i.Name == "Right lever");
                        if (leftlever.IsOn == 1 && middlelever.IsOn == 0 && rightlever.IsOn == 1)
                        {
                            Console.WriteLine("The ghost prop begins to glow and quickly moves along a wire to the stage.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"There it goes.\"");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"...and I still feel the presence of Atlantis, through...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The ghost catches Sophia by surprise.");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"... err... May I present NUR-AB-SAL!... the great Atlantean god of... of...\"");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Deceit!\"");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"...Deceit!! Thanks, Indy.\"");
                            Console.WriteLine("SOPHIA: \"INDIANA JONES!? You've got some nerve! Go back, you big jack-o'-lantern!\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The ghost prop malfunctions and burns up to nothing.");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"Oh, great... G'night, folks...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Sophia leaves the podium and angrily approaches Indy.");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("SOPHIA: \"C'mon, mister! I've got a few words to mince with you!\"");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"I'd say it's about time!\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("To be continued...");
                            Console.WriteLine("YOU WIN! THANKS FOR PLAYING!");
                            Playing = false;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("INDY: \"Nothing happened. Maybe I have to arrange the levers in a specific sequence.\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                    }
                    else
                    {
                        if (character.TalkedTo < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Hold on! She's still talking. Don't try that again... I've got my eye on you...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Wait just a minute... You're not the doorman! How'd you get in?\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("Indy is kicked out of the theater. There's no chance he'll get back in.");
                            GameOver();
                        }
                    }
                }
                else if (itemName.ToLower() == "left lever")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character == null)
                    {
                        Item leftlever = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
                        if (leftlever != null)
                        {
                            System.Console.WriteLine("Indy moves the left lever.");
                            if (leftlever.IsOn == 0)
                            {
                                leftlever.IsOn = 1;
                                System.Console.WriteLine("The left lever is activated.");
                            }
                            else
                            {
                                leftlever.IsOn = 0;
                                System.Console.WriteLine("The left lever is not activated.");
                            }
                        }
                    }
                    else
                    {
                        if (character.TalkedTo < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Hold on! She's still talking. Don't try that again... I've got my eye on you...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Wait just a minute... You're not the doorman! How'd you get in?\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("Indy is kicked out of the theater. There's no chance he'll get back in.");
                            GameOver();
                        }
                    }
                }
                else if (itemName.ToLower() == "middle lever")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character == null)
                    {
                        Item middlelever = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
                        if (middlelever != null)
                        {
                            System.Console.WriteLine("Indy moves the middle lever.");
                            if (middlelever.IsOn == 0)
                            {
                                middlelever.IsOn = 1;
                                System.Console.WriteLine("The middle lever is activated.");
                            }
                            else
                            {
                                middlelever.IsOn = 0;
                                System.Console.WriteLine("The middle lever is not activated.");
                            }
                        }
                    }
                    else
                    {
                        if (character.TalkedTo < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Hold on! She's still talking. Don't try that again... I've got my eye on you...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Wait just a minute... You're not the doorman! How'd you get in?\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("Indy is kicked out of the theater. There's no chance he'll get back in.");
                            GameOver();
                        }
                    }
                }
                else if (itemName.ToLower() == "right lever")
                {
                    Character character = CurrentRoom.Characters.Find(c => c.Name == "Stagehand");
                    if (character == null)
                    {
                        Item rightlever = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
                        if (rightlever != null)
                        {
                            System.Console.WriteLine("Indy moves the right lever.");
                            if (rightlever.IsOn == 0)
                            {
                                rightlever.IsOn = 1;
                                System.Console.WriteLine("The right lever is activated.");
                            }
                            else
                            {
                                rightlever.IsOn = 0;
                                System.Console.WriteLine("The right lever is not activated.");
                            }
                        }
                    }
                    else
                    {
                        if (character.TalkedTo < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Hold on! She's still talking. Don't try that again... I've got my eye on you...\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            character.TalkedTo++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("STAGEHAND: \"Wait just a minute... You're not the doorman! How'd you get in?\"");
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("Indy is kicked out of the theater. There's no chance he'll get back in.");
                            GameOver();
                        }
                    }
                }
                else if (itemName.ToLower() == "phone booth")
                {
                    Item phonebooth = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
                    if (phonebooth != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("INDY: \"I can't make a call. I'm out of nickels.\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                else if (itemName.ToLower() == "doors")
                {
                    Item doors = CurrentRoom.Items.Find(i => itemName.ToLower() == i.Name.ToLower());
                    if (doors != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("TICKET TAKER: \"The doors are locked, sir.\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.WriteLine("INDY: \"I can't move it.\"");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
        }
    }
}