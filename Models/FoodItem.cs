using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Models
{
    public class FoodItem
    {
        [Key]
        public long FoodItemId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string FoodItemName { get; set; }
        [Required]
        public float Price { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
    }
}
