using Eatable.Models;

namespace Eatable.ViewModels
{
    public class FoodItemCreateViewModel
    {
        public FoodItem foodItem { get; set; }
        public List<Restaurant> restaurants { get; set; }
    }
}
