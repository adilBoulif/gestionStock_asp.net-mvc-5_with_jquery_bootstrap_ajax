
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gesStockSdr.Models
{
    public class Produits
    {
        public int Id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public int num_article { get; set; }
        [Required]
        public int quantite { get; set; }
        [Required]
        public DateTime date { get; set; }
        public int categoryId { get; set; }
        public virtual Category category { get; set; }
        public int provenancesId { get; set; }
        public virtual Provenances provenances { get; set; }
        public int fournisseurId { get; set; }
        public virtual Fournisseur fournisseur { get; set; }
        public virtual ICollection<Panier> panier { get; set; }

    }
}