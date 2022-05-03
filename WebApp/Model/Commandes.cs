﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Model
{
    public class Commandes
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Statut { get; set; }
        public double PrixTotal { get; set; }
        public List<Menu> LesMenus { get; set; }
        public List<Plats> LesPlats { get; set; }
    }
}
