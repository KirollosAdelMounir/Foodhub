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
        public DateTime OrderDate { get; set; }
        public State State { get; set; }
        public int Quantity { get; set; }
        //----------------------------------------
            
            public ApplicationUser user { get; set; }
            [ForeignKey("user")]
            public string UserId { get; set; }
        //----------------------------------------

            public FoodItem fooditem { get; set; }
            [ForeignKey("fooditem")]
            public long fooditemId { get; set; }
        //----------------------------------------
    }

    public enum State
    {
        Pending,
        Cancelled,
        Delivered
    }
}
