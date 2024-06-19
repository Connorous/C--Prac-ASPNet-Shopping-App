namespace ShoppingApp.Models.DTOs
{
    public class OrderCompleteDisplayModel
    {
        public Order Order { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public Transaction Transaction { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public string StoreLocation { get; set; }

    }
}
