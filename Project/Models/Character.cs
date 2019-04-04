using System.Collections.Generic;
using indygame.Project.Interfaces;

namespace indygame.Project.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }



        public Character(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
}