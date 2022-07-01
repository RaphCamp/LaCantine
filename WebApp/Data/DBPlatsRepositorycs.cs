using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Data
{
    public class DBPlatsRepository : IPlatsRepository
    {
        private readonly LaCantineContext context;

        public DBPlatsRepository(LaCantineContext context)
        {
            this.context = context;
        }

        public Plats GetPlatsForID(int id)
        {
            return context.Plats.FirstOrDefault(f => f.id == id);
        }
    }
    public class TestPlatsRepository : IPlatsRepository
    {
        public Plats GetPlatsForID(int id)
        {
            return new Plats { id = id };
        }
    }
}
