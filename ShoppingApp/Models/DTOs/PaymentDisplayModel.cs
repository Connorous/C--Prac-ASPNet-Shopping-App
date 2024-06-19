namespace ShoppingApp.Models.DTOs
{
    public class PaymentDisplayModel
    {
        public Order Order { get; set; }

        public double TotalCost { get; set; }

        public int StoreId { get; set; }
    }
}
