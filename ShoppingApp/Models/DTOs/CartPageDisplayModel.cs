namespace ShoppingApp.Models.DTOs
{
    public class CartPageDisplayModel
    {
        public ShoppingCart Cart;

        public IEnumerable<OrderType> OrdersTypes;

        public IEnumerable<PayType> PayTypes;

        public int OrderTypeId;

        public int PayTypeId;

        public IEnumerable<Store> Stores;

        public int StoreId;

    }
}
