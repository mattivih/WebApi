using System.ComponentModel.DataAnnotations;

namespace WebApiAllAssignments.Models
{
    public class NewPlayer
    {
        [Required]
        public string Name { get; set; }
        [Range(1, 100)]
        public int Level { get; set; }
    }
}