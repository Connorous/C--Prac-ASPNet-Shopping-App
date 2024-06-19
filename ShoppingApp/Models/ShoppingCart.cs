using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<CartItem> CartItems { get; set; }

    }
}
