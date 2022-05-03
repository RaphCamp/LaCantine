using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Data
{
    public class DBCommandesRepository : ICommandesRepository
    {
        private readonly LaCantineContext context;

        public DBCommandesRepository(LaCantineContext context)
        {
            this.context = context;
        }

        public Commandes GetCommandesForID(int id)
        {
            return context.Commandes.FirstOrDefault(f => f.ID == id);
        }
    }
    public class TestRepository : ICommandesRepository
    {
        public Commandes GetCommandesForID(int iD)
        {
            return new Commandes { ID = iD , Date = new DateTime(2020, 12, 01) };
        }
    }
}
