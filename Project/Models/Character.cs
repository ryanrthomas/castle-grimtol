using System.Collections.Generic;
using indygame.Project.Interfaces;

namespace indygame.Project.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TalkedTo {get;set;} = 0;
        public List<Choice> Choices { get; set; }

        public List<string> Dialogue { get; set; }

        public Character(string name, string desc, int talkedTo)
        {
            Name = name;
            Description = desc;
            TalkedTo = talkedTo;
        }
    }
}