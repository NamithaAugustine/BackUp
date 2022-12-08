using System.ComponentModel.DataAnnotations;

namespace CabManagementSystems.Models
{
    public class Place
    {
        public string Id { get; set; }

        [Required]
        public Location From { get; set; }

        [Required]
        public Location To { get; set; }

        [Required]
        public int Distance { get; set; }
    }
}
