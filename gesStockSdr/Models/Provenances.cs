using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gesStockSdr.Models
{
    public class Provenances
    {
        public int Id { get; set; }
        [Required]
        public string nom { get; set; }


        public virtual ICollection<Produits> produites { get; set; }

    }
}