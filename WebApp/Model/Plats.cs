using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaCantine.Model
{
    public class Plats
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string photo { get; set; }
        public double prix { get; set; }

        public virtual ICollection<Menu> menus { get; set; }
        public virtual ICollection<Commandes> commandes { get; set; }

    }
}

