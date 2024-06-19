namespace ShoppingApp.Models.DTOs
{
    public class OrderDisplayModel
    {

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<OrderType> OrdersType { get; set;}

        public IEnumerable<OrderStatus> OrdersStatus { get; set;}

        public IEnumerable<PayType> PayTypes { get; set;}

        public IEnumerable<Store> Stores { get; set; }
    }
}
