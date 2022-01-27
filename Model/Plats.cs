using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaCantine.Model
{
    public class Plats
    {
        public int ID { get; set; }
        
        public string Libelle { get; set; }

        public double Prix { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "Produits Allergenes")]
        public List<Produits_Allergenes> Produits_Allergenes { get; set; }

        public string Image { get; set; }
    }
}

