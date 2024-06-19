using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("Developer")]
    public class Developer
    {
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string DeveloperName { get; set; }

        public List<Item> Items { get; set; }
    }
}
