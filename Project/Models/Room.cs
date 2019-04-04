using System.Collections.Generic;
using indygame.Project.Interfaces;

namespace indygame.Project.Models
{
    class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public List<Character> Characters { get; set; }
        public Dictionary<Direction, IRoom> NearbyRooms { get; set; }

        public void AddNearbyRooms(Direction direction, IRoom room)
        {
            NearbyRooms.Add(direction, room);
        }

        public IRoom GoToRoom(Direction dir)
        {
            if (NearbyRooms.ContainsKey(dir))
            {
                return NearbyRooms[dir];
            }
            System.Console.WriteLine("Indy: \"I can't go that way.\"");
            return (IRoom)this;
        }
        public Room(string name, string desc)
        {
            NearbyRooms = new Dictionary<Direction, IRoom>();
            Name = name;
            Description = desc;
            Items = new List<Item>();
            Characters = new List<Character>();
        }
    }
    public enum Direction
    {
        north,
        south,
        west,
        east
    }
}