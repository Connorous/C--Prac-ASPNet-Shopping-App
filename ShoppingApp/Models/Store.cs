using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("Store")]
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        public bool IsOpen { get; set; } = false;

        public List<StoreItem> StoreItems { get; set; }

       

    }
}
