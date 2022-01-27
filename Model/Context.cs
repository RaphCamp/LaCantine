using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCantine.Model;
using Microsoft.EntityFrameworkCore;

namespace LaCantine.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Commandes> Table_Commandes { get; set; }
        public DbSet<Menu> Table_Menu { get; set; }
        public DbSet<Plats> Table_Plats { get; set; }
        public DbSet<Produits_Allergenes> Table_Produits_Allergenes { get; set; }
        public DbSet<Utilisateur> Table_Utilisateur { get; set; }
    }
}