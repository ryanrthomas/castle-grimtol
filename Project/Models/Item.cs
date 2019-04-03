using System.Collections.Generic;
using indygame.Project.Interfaces;

namespace indygame.Project.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        List<string> Inventory { get; set; }
        public void AddToInventory(string item)
        {

        }
        public void ShowInventory()
        {
            System.Console.WriteLine("Inventory:");
            Inventory.ForEach(item =>
            {
                System.Console.WriteLine(item);
            });
        }

        public Item(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

    }
}