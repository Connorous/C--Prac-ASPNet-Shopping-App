using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{

    [Table("PayType")]
    public class PayType
    {

        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string PayTypeName {  get; set; }

        public List<Order> Orders { get; set; }
    }
}
