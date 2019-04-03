using System.Collections.Generic;
using indygame.Project.Interfaces;

namespace indygame.Project.Models
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        Dictionary<Direction, IRoom> NearbyRooms { get; set; }

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