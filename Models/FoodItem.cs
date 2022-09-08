namespace FoodHub.Models
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }
        public string? FoodItemName { get; set; }
        public float Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
