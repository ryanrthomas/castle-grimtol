using System.Collections.Generic;
using indygame.Project.Interfaces;

namespace indygame.Project.Models
{
    public class Player
    {
        public List<Item> Inventory { get; set; } = new List<Item>();
    }
}