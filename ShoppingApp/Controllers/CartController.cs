using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Data;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;
using ShoppingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Net;
using System.Security.Principal;

namespace ShoppingApp.Contorllers;


    [Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext databaseContext;
    private readonly IRepoUtility repoUtility;

    public CartController(ApplicationDbContext databaseContext, IRepoUtility repoUtility)
    {
        this.databaseContext = databaseContext;
        this.repoUtility = repoUtility;

    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> AddItem(int itemId, int qty = 1, int redirect = 0)
    {
        
        string userId = repoUtility.GetUserId();

        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("user is not logged in");
        }
        using var transaction = databaseContext.Database.BeginTransaction();
        try
        {

            ShoppingCart cart = await GetUserCart();
            if (cart is null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    IsDeleted = false,
                };
                databaseContext.ShoppingCarts.Add(cart);
            }
            databaseContext.SaveChanges();

            CartItem firstCartItem = databaseContext.CartItems
                              .FirstOrDefault(entry => entry.ShoppingCartId == cart.Id && entry.ItemId == itemId);
            if (firstCartItem != null)
            {
                firstCartItem.Quantity += qty;
            }
            else
            {
                Item item = databaseContext.Items.Find(itemId);
                firstCartItem = new CartItem
                {
                    ItemId = itemId,
                    ShoppingCartId = cart.Id,
                    Quantity = qty,
                    UnitPrice = item.Price
                };
                databaseContext.CartItems.Add(firstCartItem);
            }
            databaseContext.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
        }
        int cartItemCount = await GetCartItemCount();
        if (redirect == 0)
        {
            return Ok(cartItemCount);
        }

        return RedirectToAction("ViewCart");
    }


    [Authorize]
    public async Task<IActionResult> RemoveItem(int itemId)
    {
        string userId = repoUtility.GetUserId();
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user is not logged in");
            }
            ShoppingCart cart = await GetUserCart();
            if (cart is null)
                throw new Exception("Invalid cart");
            // cart detail section
            var cartItem = databaseContext.CartItems
                              .FirstOrDefault(cartItem => cartItem.ShoppingCartId == cart.Id && cartItem.ItemId == itemId);
            if (cartItem is null)
                throw new Exception("No items in cart");
            else if (cartItem.Quantity == 1)
                databaseContext.CartItems.Remove(cartItem);
            else
                cartItem.Quantity = cartItem.Quantity - 1;
            databaseContext.SaveChanges();
        }
        catch (Exception ex)
        {

        }
        
        return RedirectToAction("ViewCart");
    }

    [Authorize]
    public async Task<ShoppingCart> GetUserCart()
    {
        string userId = repoUtility.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("Invalid user");
        }
        ShoppingCart cart = await databaseContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);

        return cart;
    }

    [Authorize]
    public async Task<IActionResult> ViewCart()
    {
        var userId = repoUtility.GetUserId();
        if (userId == null)
            throw new Exception("Invalid userid");
        ShoppingCart shoppingCart = await databaseContext.ShoppingCarts
                              .Include(cartTable => cartTable.CartItems)
                              .ThenInclude(cartItemTable => cartItemTable.Item)
                              .ThenInclude(itemTable => itemTable.Category)    
                              .Where(cart => cart.UserId == userId).FirstOrDefaultAsync();


        if (shoppingCart == null)
        {
            shoppingCart = new ShoppingCart
            {
                UserId = userId,
                IsDeleted = false,
            };

            databaseContext.ShoppingCarts.Add(shoppingCart);
            databaseContext.SaveChanges();
            shoppingCart = await databaseContext.ShoppingCarts.FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        if (shoppingCart == null) {
            throw new Exception("Database error");
        }

        IEnumerable<CartItem> cartItems = await databaseContext.CartItems
            .Where(cart => cart.ShoppingCartId == shoppingCart.Id).ToListAsync();


        IEnumerable<OrderType> orderTypes = await databaseContext.OrderTypes.ToListAsync();
        IEnumerable<PayType> payTypes = await databaseContext.PayTypes.ToListAsync();

        List<Store> stores = await databaseContext.Stores.ToListAsync();
        List<StoreItem> storeItems = await databaseContext.StoreItems.ToListAsync();

        List<StoreItem> matchingStoreItems = new List<StoreItem>();

        foreach (CartItem cartItem in cartItems)
        {
            if (stores.Count == 0)
            {
                break;
            }


            foreach (StoreItem storeItem in storeItems)
            {
     
                if (storeItem.ItemId == cartItem.ItemId)
                {
                    if (storeItem.Quantity > 0 && storeItem.Quantity >= cartItem.Quantity)
                    {
                        matchingStoreItems.Add(storeItem);
                        break;
                    } 
                }
            }
        }

        List<int> unmatchingIds = new List<int>();
       

        foreach (StoreItem matchedItem in matchingStoreItems)
        {
            if (stores.Count == 0)
            {
                break;
            }

            foreach (Store store in stores.ToList())
            {
                if (matchedItem.StoreId != store.Id)
                {
                    stores.Remove(store);
                }
            }
        }


        CartPageDisplayModel model = new CartPageDisplayModel
        {
            Cart = shoppingCart,
            OrdersTypes = orderTypes,
            PayTypes = payTypes,
            OrderTypeId = 0,
            PayTypeId = 0,
            Stores = stores,
        };
 
        return View(model);
    }

    [Authorize]
    public async Task<int> GetCartItemCount()
    {
        int cartCount = 0;

        string userId = repoUtility.GetUserId();
        if (string.IsNullOrEmpty(userId)) 
        {
            throw new Exception("Invalid user");
        }
        List<CartItem> cart = await (from shoppingCart in databaseContext.ShoppingCarts
                               join cartItem in databaseContext.CartItems
                               on shoppingCart.Id equals cartItem.ShoppingCartId
                               where shoppingCart.UserId == userId 
                               select new CartItem {
                                   Id = cartItem.Id,
                                   Quantity = cartItem.Quantity,
                               }
                    ).ToListAsync();
        foreach ( var cartItem in cart ) {
            cartCount += cartItem.Quantity; 
        }

        return cartCount;
    }


    [Authorize]
    public async Task<IActionResult> Checkout(int orderTypeId = 0, int payTypeId = 0, int storeId = 0)
    {
        if (orderTypeId == 0 || payTypeId == 0)
        {
            throw new Exception("Order type and/or Payment type not selected");
        }

        bool checkoutSuccess = false;
        Order order = null;

        double totalCost = 0;

        using IDbContextTransaction transaction = databaseContext.Database.BeginTransaction();
        try
        {
            string userId = repoUtility.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            ShoppingCart cart = await GetUserCart();
            if (cart is null)
                throw new Exception("Invalid cart");
            List<CartItem> cartItems = await databaseContext.CartItems
                                .Where(cartItem => cartItem.ShoppingCartId == cart.Id).ToListAsync();
            if (cartItems.Count() == 0)
                throw new Exception("Cart is empty");

            DateTime dateTimeNow = DateTime.UtcNow;

            order = new Order
            {
                UserId = userId,
                CreateDate = dateTimeNow,
                OrderStatusId = 1,
                OrderTypeId = orderTypeId,
                StoreId = storeId,
                PayTypeId = payTypeId,
                IsDeleted = false,
            };
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();

            
            order = databaseContext.Orders.FirstOrDefault(order => order.UserId == userId && order.CreateDate == dateTimeNow);
            
            foreach (var item in cartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.ItemId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                };
                databaseContext.OrderItems.Add(orderItem);

                totalCost += item.Quantity * item.UnitPrice;
            }
            databaseContext.SaveChanges();

            
            databaseContext.CartItems.RemoveRange(cartItems);
            databaseContext.SaveChanges();
            transaction.Commit();
            checkoutSuccess = true;
        }
        catch (Exception e)
        {
            checkoutSuccess = false;
            throw e;
        }


        if (!checkoutSuccess)
        {
            throw new Exception("Server Side Error");
        }
            
        

        if (payTypeId == 1)
        {
            return RedirectToAction("PaymentDetails", "Cart", new { orderId = order.Id, totalCost = totalCost, storeId = storeId});
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
  
    }


    [Authorize]
    public async Task<IActionResult> PaymentDetails(int orderId = 0, double totalCost = 0, int storeId = 0)
    {
        if (orderId == 0)
        {
            throw new Exception("Invalid Order Id " + orderId);
        }

        string userId = repoUtility.GetUserId();

        Order customerOrder = await databaseContext.Orders
                              .Include(orderTable => orderTable.OrderItems)
                              .ThenInclude(orderItemTable => orderItemTable.Item)
                              .Where(order => order.Id == orderId).FirstOrDefaultAsync();

        if (customerOrder == null)
        {
            throw new Exception("Invalid Order");
        }


        PaymentDisplayModel model = new PaymentDisplayModel
        {
            Order = customerOrder,
            TotalCost = totalCost,
            StoreId = storeId,
        };

        return View(model);
    }


    [Authorize]
    public async Task<IActionResult> OrderComplete(int orderId = 0, int zipCode = 0, string cardNumber = "", int securityNumber = 0, string customerAddress = "", int customerPhone = 0, double totalCost = 0, int storeId = 0) {

        if (orderId == 0)
        {
            throw new Exception("Invalid Order Id ");
        }

        if (zipCode == 0)
        {
            throw new Exception("Invalid ZipCode");
        }
        
        if (cardNumber.IsNullOrEmpty())
        {
            throw new Exception("Invalid Card Number");
        }

        if (securityNumber == 0)
        {
            throw new Exception("Invalid Card Security Number");
        }

        if (customerAddress.IsNullOrEmpty())
        {
            throw new Exception("Invalid Address");
        }

        if (customerPhone == 0)
        {
            throw new Exception("Invalid Phone Number");
        }

        long cardNumberToNum = Int64.Parse(cardNumber);

        Order customerOrder = await databaseContext.Orders.FindAsync(orderId);

        if (customerOrder == null)
        {
            throw new Exception("Invalid Order");
        }



        Store store = await databaseContext.Stores.FirstOrDefaultAsync(store => store.Id == storeId);
        string storeLocaton = "";

        if (store == null)
        {
            storeId = 0;
            storeLocaton = "";
        }
        else
        {
            storeLocaton = store.Location;
        }



        Transaction transaction;
        Transaction transactionInDatabase;

        List<OrderItem> orderItems;

        using IDbContextTransaction dataTransaction = databaseContext.Database.BeginTransaction();

        try
        {


            string userId = repoUtility.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Invalid user");
            }


            orderItems = await databaseContext.OrderItems
                              .Include(orderItemTable => orderItemTable.Item)
                              .Where(orderItemTable => orderItemTable.OrderId == orderId).ToListAsync();



            /*await (from order in databaseContext.Orders
                                            join orderItem in databaseContext.OrderItems
                                            on order.Id equals orderItem.OrderId
                                            where order.UserId == userId
                                            select new OrderItem
                                            {
                                                Id = orderItem.Id,
                                                Quantity = orderItem.Quantity,
                                                UnitPrice = orderItem.UnitPrice,
                                                ItemId = orderItem.ItemId,
                                                OrderId = orderItem.OrderId,
                                            }
                    ).ToListAsync();*/



            if (customerOrder.OrderTypeId == 2)
            {
                totalCost += 50;
            }

            /*foreach (var item in orderItems)
            {
                totalCost += item.Quantity * item.UnitPrice;
            }*/


            transaction = new Transaction
            {
                OrderId = customerOrder.Id,
                Amount = totalCost,
                ZipCode = zipCode,
                CardNumber = cardNumberToNum,
                CardSecurityNumber = securityNumber,
                CustomerAddress = customerAddress,
                CustomerPhone = customerPhone,
                Success = true,
                DateAndTime = DateTime.UtcNow,
            };

            
            customerOrder.isPayed = true;
            
            databaseContext.Transactions.Add(transaction);
            databaseContext.SaveChanges();
            dataTransaction.Commit();

            using IDbContextTransaction dataTransaction2 = databaseContext.Database.BeginTransaction();
            try
            {
                transactionInDatabase = await databaseContext.Transactions.FirstOrDefaultAsync(transaction => transaction.OrderId == customerOrder.Id);

                if (transactionInDatabase == null)
                {
                    throw new Exception("Server side error with selecting transaction from database");
                }

                
                databaseContext.Orders.Update(customerOrder);
                

                databaseContext.SaveChanges();
                dataTransaction2.Commit();

                Order customerOrderInDatabase = databaseContext.Orders.FirstOrDefault(order => order.Id == customerOrder.Id);
                if (transactionInDatabase == null)
                {
                    throw new Exception("Server side error with selecting order from database");
                }

                if (storeId > 0)
                {
                    foreach (OrderItem orderItem in orderItems)
                    {
                        using IDbContextTransaction dataTransaction3 = databaseContext.Database.BeginTransaction();
                        try
                        {

                            StoreItem storeItem = await databaseContext.StoreItems.FirstOrDefaultAsync(storeItem => storeItem.ItemId == orderItem.ItemId);
                            if (storeItem == null)
                            {
                                throw new Exception("Server side error with store items");
                            }

                            storeItem.Quantity = storeItem.Quantity - orderItem.Quantity;
                            if (storeItem.Quantity < 0)
                            {
                                throw new Exception("Server side error with store item quantity");
                            }
                            databaseContext.Update(storeItem);
                            databaseContext.SaveChanges();
                            dataTransaction3.Commit();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                

                OrderCompleteDisplayModel model = new OrderCompleteDisplayModel
                {
                    Order = customerOrderInDatabase,
                    OrderItems = orderItems,
                    Transaction = transactionInDatabase,
                    StoreLocation = storeLocaton
                };


                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
 
    }
}

    
