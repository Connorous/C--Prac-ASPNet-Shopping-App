using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        public Order Order { get; set; }

        public Item Item { get; set; }

    }
}
