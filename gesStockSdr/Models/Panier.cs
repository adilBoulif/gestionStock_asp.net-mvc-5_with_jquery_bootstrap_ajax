using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gesStockSdr.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int quantite { get; set; }
        public int ProduitsId { get; set; }
        public virtual Produits produits { get; set; }
        public int CommandeId { get; set; }
        public virtual Commande commande { get; set; }
    }
}