using Eatable.Models;

namespace Eatable.ViewModels
{
    public class UserOrders
    {
        public List<Order> orders { set; get; }
        public string userid { set; get; }
    }
}
