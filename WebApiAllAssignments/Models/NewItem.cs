using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiAllAssignments.Models
{
    public class NewItem
    {
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        [Required]
        [Range(1, 80)]
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}