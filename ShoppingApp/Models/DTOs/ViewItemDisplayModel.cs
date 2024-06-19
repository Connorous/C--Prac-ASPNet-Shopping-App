namespace ShoppingApp.Models.DTOs
{
    public class ViewItemDisplayModel
    {
        public Item Item { get; set; }

        public List<StoreItem> StoreItems { get; set; }

        public bool hasStoreStock { get; set; } = false;
    }
}
