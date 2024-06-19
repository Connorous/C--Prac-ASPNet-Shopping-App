using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{

    [Authorize]
    public class DataController : Controller
    {
        private readonly ApplicationDbContext databaseContex;

        public DataController(ApplicationDbContext databaseContex)
        {
            this.databaseContex = databaseContex;
        }

        public void Index()
        {
            //return View();
        }


        


    }
}