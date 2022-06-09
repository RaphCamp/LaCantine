using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Data
{
    public class DBMenuRepository : IMenusRepository
    {
        private readonly LaCantineContext context;
        public DBMenuRepository(LaCantineContext context)
        {
            this.context = context;
        }

        public Task<Menu> GetMenuForID(int id)
        {
            return Task.FromResult(context.Menu.FirstOrDefault(f => f.id == id));
        }
    }
}
