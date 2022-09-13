using FoodHub.Models;

namespace FoodHub.ViewModels
{
    public class UserOrders
    {
        public List<Order> orders { set; get; }
        public string userid { set; get; }
    }
}
