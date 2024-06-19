using Humanizer.Localisation;
using System.Drawing;

namespace ShoppingApp.Models.DTOs
{
    public class ItemDisplayModel
    {
        public IEnumerable<Item> Items { get; set; }

        //public Dictionary<int, Image> ItemImages { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Brand> Brands { get; set; }

        public IEnumerable<Developer> Developers { get; set; }

        public IEnumerable<Platform> Platforms { get; set; }
        public string SearchTerm { get; set; } = "";

        public int CategoryId { get; set; } = 0;
        public int BrandId { get; set; } = 0;
        public int DeveloperId { get; set; } = 0;
        public int PlatformId { get; set; } = 0;
    }
}
