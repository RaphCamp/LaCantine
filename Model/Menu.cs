using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LaCantine.Model
{
    public class Menu
    {

        public int ID { get; set; }

        public string Libelle { get; set; }

        public int Prix { get; set; }

        public string Date { get; set; }

        public List<Plats> LesPlats {get; set; }
    }
}
