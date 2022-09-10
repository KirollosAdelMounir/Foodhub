using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Models
{
    public class Order_FoodItem
    {
        public Order Order { get; set; }
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        public FoodItem FoodItem { get; set; }
        [ForeignKey("FoodItem")]
        public long FoodItemId { get; set; }
    }
}
