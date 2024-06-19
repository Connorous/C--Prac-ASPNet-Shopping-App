using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("StoreItem")]
    public class StoreItem
    {
        public int Id { get; set; }

        [Required]
        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; } = 0;


    }
}
