using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        public int ZipCode { get; set; }

        public long CardNumber { get; set; }

        public int CardSecurityNumber { get; set; }

        public string CustomerAddress { get; set; }

        public int CustomerPhone { get; set; }

        public bool Success { get; set; } = false;

        public DateTime DateAndTime { get; set; } = DateTime.UtcNow;


        public int OrderId { get; set; }

        public Order Order { get; set; }


    }
}
