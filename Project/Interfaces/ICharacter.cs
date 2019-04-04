using System.Collections.Generic;
using indygame.Project.Models;

namespace indygame.Project.Interfaces
{
    public interface ICharacter
    {
        string CharacterName { get; set; }
        List<Item> Inventory { get; set; }
    }
}