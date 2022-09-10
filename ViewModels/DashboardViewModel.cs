using FoodHub.Models;

namespace FoodHub.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Order> orders { get; set; }
        public IEnumerable<Restaurant> restaurants { get; set; }
        public IEnumerable<FoodItem> foodItems { get; set; }
    }
}
