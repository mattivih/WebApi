using System.ComponentModel.DataAnnotations;

namespace WebApiAllAssignments.Models
{
    public class ModifiedItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 80)]
        public int Level { get; set; }
    }
}