using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gesStockSdr.Models
{
    public class Commande
    {
        public int Id { get; set; }
        [DefaultValue(false)]
        public bool status { get; set; }
        public int serviceId { get; set; }
        public virtual Service service { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        Dictionary<Produits, int> Comd = new Dictionary<Produits, int>();
        public virtual ICollection<Panier> panier { get; set; }
        public void Set(Dictionary<Produits, int> comd)
        {
            Comd = comd;

        }

        public Dictionary<Produits, int> Get()
        {

           

            return Comd;
        }
    }
}