using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaCantine.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LaCantine.Data
{
    public class LaCantineContext : IdentityDbContext<IdentityUser>
    {
        public LaCantineContext (DbContextOptions<LaCantineContext> options)
            : base(options)
        {
        }

        public DbSet<Commandes> Commandes { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Plats> Plats { get; set; }
        public DbSet<Produits_Allergenes> Produits_Allergenes { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
    }
}
