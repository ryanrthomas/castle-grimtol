using System.Collections.Generic;
using indygame.Project.Models;

namespace indygame.Project.Interfaces
{
    public interface IRoom
    {
        string Name { get; set; }
        string Description { get; set; }
        List<Item> Items { get; set; }
        Dictionary<Direction, IRoom> NearbyRooms { get; set; }
    }
}
