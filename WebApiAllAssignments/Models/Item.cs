using System;

namespace WebApiAllAssignments.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid Id { get; set; }
        
    }
}