
namespace FoodHub.Models
{
    public class Order
    {
        public FoodItem foodItem { get; set; }
        public int foodItemId { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }

        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

    }
}
