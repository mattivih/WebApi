using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiAllAssignments.Models
{
    public class ModifiedPlayer
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 100)]
        public int Level { get; set; }

        public ModifiedPlayer(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}