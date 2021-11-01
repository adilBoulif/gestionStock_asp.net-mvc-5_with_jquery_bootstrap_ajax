using gesStockSdr.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gesStockSdr.Startup))]
namespace gesStockSdr
{

    public partial class Startup
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void CreateDefaultUser()
        {

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = new ApplicationUser { UserName = "admin", role = "admin", Email = "email@gmaul.com" };
                var result =  userManager.CreateAsync(user, "@dminSdr2021");
               
            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
         
        
    }
}
