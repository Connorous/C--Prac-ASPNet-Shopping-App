using Microsoft.AspNetCore.Identity;
using ShoppingApp.Data;

namespace ShoppingApp.Repositories;


using Microsoft.AspNetCore.Identity;

 

    public class RepoUtility : IRepoUtility
    {

        ApplicationDbContext databaseContext;
        IHttpContextAccessor  httpContextAccessor;
        UserManager<IdentityUser> userManager;

        public RepoUtility(ApplicationDbContext databaseContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this.databaseContext = databaseContext;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal);
            return userId;
        }
    }

