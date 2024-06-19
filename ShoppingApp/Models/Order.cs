using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Required]

        public bool IsDeleted { get; set; } = false;

        public bool isPayed { get; set; } = false;

        public int OrderTypeId { get; set; }

        public OrderType OrderType { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int PayTypeId { get; set; }

        public PayType PayType { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public int? StoreId { get; set; }

        
    }
}
