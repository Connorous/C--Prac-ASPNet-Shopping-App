using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("OrderType")]
    public class OrderType
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Type { get; set; } = "none";

        public List<Order> Orders { get; set; }
    }
}
