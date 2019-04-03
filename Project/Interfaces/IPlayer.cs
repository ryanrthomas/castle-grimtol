using System.Collections.Generic;
using indygame.Project.Models;

namespace indygame.Project.Interfaces
{
    public interface IPlayer
    {
        string PlayerName { get; set; }
        List<Item> Inventory { get; set; }
    }
}
