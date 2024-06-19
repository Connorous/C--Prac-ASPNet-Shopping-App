using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data;
using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;
using System.Diagnostics;
using System.IO;
using ShoppingApp.Repositories;
using static System.Reflection.Metadata.BlobBuilder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext databaseContext;
        private readonly IRepoUtility repoUtility;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext databaseContext, IRepoUtility repoUtility)
        {
            _logger = logger;
            this.databaseContext = databaseContext;
            this.repoUtility = repoUtility; 
        }

        public async Task<IActionResult> Index(string searchTerm = "", int categoryId = 0, int brandId = 0, int developerId = 0, int platformId = 0)
        {
            string searchTermToLower = searchTerm.ToLower();
            IEnumerable<Item> items;

            
            items = await (from item in databaseContext.Items

                           join category in databaseContext.Categories
                           on item.CategoryId equals category.Id

                           join brand in databaseContext.Brands
                           on item.BrandId equals brand.Id

                           orderby item.Id

                           where string.IsNullOrWhiteSpace(searchTermToLower) || (item != null && item.ItemName.ToLower().StartsWith(searchTermToLower))
                           select new Item
                           {
                               Id = item.Id,
                               ItemName = item.ItemName,
                               Image = item.Image,
                               BrandId = item.BrandId,
                               DeveloperId = item.DeveloperId,
                               PlatformId = item.PlatformId,
                               CategoryId = item.CategoryId,
                               Description = item.Description,
                               Price = item.Price,
                               BrandName = brand.BrandName,
                               CategoryName = category.CategoryName,
                           }
                         ).ToListAsync();
            if (categoryId > 0)
            {
                items = items.Where(item => item.CategoryId == categoryId).ToList();
            }
            if (brandId > 0)
            {
                items = items.Where(item => item.BrandId == brandId).ToList();
            }
            if (developerId > 0)
            {
                items = items.Where(item => item.DeveloperId == developerId).ToList();
            }
            if (platformId > 0)
            {
                items = items.Where(item => item.PlatformId == platformId).ToList();
            }


            foreach (Item item in items) { 
                if (item.DeveloperId != null)
                {
                    Developer itemDeveloper = await databaseContext.Developers.FindAsync(item.DeveloperId);
                    item.DeveloperName = itemDeveloper.DeveloperName;
                }

                if (item.PlatformId != null)
                {
                    Platform itemPlatform = await databaseContext.Platforms.FindAsync(item.PlatformId);
                    item.PlatformName = itemPlatform.PlatformName;
                }

                
            }


            IEnumerable<Category> categories = await databaseContext.Categories.ToListAsync();
            IEnumerable<Brand> brands = await databaseContext.Brands.ToListAsync();
            IEnumerable<Developer> developers = await databaseContext.Developers.ToListAsync();
            IEnumerable<Platform> platforms = await databaseContext.Platforms.ToListAsync();


            ItemDisplayModel itemModel = new ItemDisplayModel
            {
                Items = items,
                Categories = categories,
                Brands = brands,
                Developers = developers,
                Platforms = platforms,
                CategoryId = categoryId,
                BrandId = brandId,
                DeveloperId = developerId,
                PlatformId = platformId,
                SearchTerm = searchTerm,
            };
            return View(itemModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        
        public async Task<IActionResult> ViewItem(int itemId)
        {
            Item item = await databaseContext.Items.FindAsync(itemId);


            if (item.CategoryId != null)
            {
                Category itemCategory = await databaseContext.Categories.FindAsync(item.CategoryId);
                item.CategoryName = itemCategory.CategoryName;
            }
            if (item.BrandId != null)
            {
                Brand itemBrand = await databaseContext.Brands.FindAsync(item.BrandId);
                item.BrandName = itemBrand.BrandName;
            }
            if (item.DeveloperId != null)
            {
                Developer itemDeveloper = await databaseContext.Developers.FindAsync(item.DeveloperId);
                item.DeveloperName = itemDeveloper.DeveloperName;
            }

            if (item.PlatformId != null)
            {
                Platform itemPlatform = await databaseContext.Platforms.FindAsync(item.PlatformId);
                item.PlatformName = itemPlatform.PlatformName;
            }

            List<StoreItem> storeItems = await databaseContext.StoreItems
                .Include(storeItem=> storeItem.Store)
                .Where(storeItem => storeItem.ItemId == item.Id).ToListAsync();

            ViewItemDisplayModel viewItemDisplayModel = new ViewItemDisplayModel()
            {
                Item = item,
                StoreItems = storeItems
            };

            return View(viewItemDisplayModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        public async Task<IActionResult> MyOrders()
        {
            string userId = repoUtility.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            List<Order> orders = await databaseContext.Orders
                            .Include(order => order.OrderStatus)
                            .Include(order => order.OrderItems)
                            .ThenInclude(orderItem => orderItem.Item)
                            .ThenInclude(Item => Item.Category)
                            .Where(order => order.UserId == userId)
                            .ToListAsync();


            List<OrderType> orderTypes = await databaseContext.OrderTypes.ToListAsync();
            List<OrderStatus> orderStatuses = await databaseContext.OrderStatuses.ToListAsync();
            List<PayType> payTypes = await databaseContext.PayTypes.ToListAsync();
            List<Store> stores = await databaseContext.Stores.ToListAsync();


            OrderDisplayModel orderDisplayModel = new OrderDisplayModel
            {
                Orders = orders,
                OrdersType = orderTypes,
                OrdersStatus = orderStatuses,
                PayTypes = payTypes,
                Stores = stores
            };
            
            return View(orderDisplayModel);
        }
    }


}
