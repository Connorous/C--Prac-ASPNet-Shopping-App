using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    [Table("Platform")]
    public class Platform
    {
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string PlatformName { get; set; }

        public List<Item> Items { get; set; }
    }
}
