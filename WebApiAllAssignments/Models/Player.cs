using System;
using System.Collections.Generic;

namespace WebApiAllAssignments.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<Item> Items { get; set; }
    }
}