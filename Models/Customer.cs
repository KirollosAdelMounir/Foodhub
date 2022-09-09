using WebApplication2.Entity;

namespace FoodHub.Models
{
    public class Customer:Entity<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
    }
}
