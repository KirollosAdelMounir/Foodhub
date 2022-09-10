using FoodHub.Models;

namespace FoodHub.ViewModels
{
    public class FoodItemCreateViewModel
    {
        public FoodItem foodItem { get; set; }
        public List<Restaurant> restaurants { get; set; }
    }
}
