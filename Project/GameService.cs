using System.Collections.Generic;
using indygame.Project.Interfaces;
using indygame.Project.Models;

namespace indygame.Project
{
    public class GameService
    {
        public IRoom CurrentLocation { get; set; }
        public bool Playing { get; set; }
        public void Initialize()
        {

            // Create all rooms
            Room boxoffice = new Room("Box office", "You are in front of the theatre where Sophia's psychic show is taking place. A ticket taker sits in a box office. The only exit is to the south down the street to an alleyway.");
            Room alleyway = new Room("Alleyway", "You are at the corner of the theater. There is a newspaper stand nearby with a phone booth adjacent to it. Around the corner lies the back of the theater.");

            // Create all items
            Item magazine = new Item("National Archaeology", "You flip through the pages, looking at a photo of you and Sophia. \"This was taken a long time ago, when I thought we might like each other,\" you say to yourself.");
            Item newspaper = new Item("Newspaper", "It's today's paper.");

            // Establish relationships
            // boxoffice.AddNearbyRooms(Direction.south, alleyway);

            // CurrentLocation = boxoffice;
            Playing = true;
        }

        public void Play()
        {
            Initialize();
            while (Playing)
            {
                System.Console.WriteLine($"{CurrentLocation.Name}: {CurrentLocation.Description}");
                HandleRoomInput();
                
                Playing = false;
            }
        }
        private void HandleRoomInput() {
            System.Console.WriteLine("Where would you like to go?");
            string choice = System.Console.ReadLine();
        }
    }
}