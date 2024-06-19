using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required]
        public string? StatusName { get; set; } = "none";

        public List<Order> Orders { get; set; }
    }
}
