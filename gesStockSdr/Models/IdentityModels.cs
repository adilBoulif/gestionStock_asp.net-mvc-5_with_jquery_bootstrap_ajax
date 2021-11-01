using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gesStockSdr.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string role { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Fournisseur> Fournisseurs { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Service> Services { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Provenances> Provenances { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Produits> Produits { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Commande> Commandes { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Panier> Paniers { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.GesUserViewModel> GesUserViewModels { get; set; }

        public System.Data.Entity.DbSet<gesStockSdr.Models.Historique> Historiques { get; set; }
    }
}