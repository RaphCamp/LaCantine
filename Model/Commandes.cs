using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Model
{
    public class Commandes
    {
        

        public int ID { get => ID; set => ID = value; }
        public DateTime Date { get => Date; set => Date = value; }
        public string Statut { get => Statut; set => Statut = value; }
        public double PrixTotal { get => PrixTotal; set => PrixTotal = value; }
        public List<Menu> LesMenus { get => LesMenus; set => LesMenus = value; }
        public List<Plats> LesPlats { get => LesPlats; set => LesPlats = value; }
    }
}
