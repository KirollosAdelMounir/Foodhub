using FoodHub.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public State State { get; set; }
        public float TotalPrice { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }

    public enum State
    {
        Pending,
        Cancelled,
        Delivered
    }
}
