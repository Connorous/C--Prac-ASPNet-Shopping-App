using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    [Table("Item")]
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string? ItemName { get; set; }


        public int? BrandId { get; set; }

        public Brand? Brand { get; set; }


        public int? DeveloperId { get; set; }

        public Developer? Developer { get; set; }


        public int? PlatformId { get; set; }

        public Platform? Platform { get; set; }

        [Required]
        public double Price { get; set; }

        public string? Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string? Description { get; set; }

        public List<OrderItem> OrderItem { get; set; }

        public List<CartItem> CartItem { get; set; }

        public List<StoreItem> StoreItem { get; set; }

        [NotMapped]
        public string? CategoryName { get; set; }

        [NotMapped]
        public string? BrandName { get; set; }

        [NotMapped]
        public string? DeveloperName { get; set; }

        [NotMapped]
        public string? PlatformName { get; set; }

    }
}
