using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatable.Models
{
    public class Restaurant
    {
        [Key]
        public long RestaurantId { get; set; }
        [Required]
        [Display(Name = "Restaurant Name")]
        public string RestaurantName { get; set; }
        [Required]
        public string Category { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
    }
}
