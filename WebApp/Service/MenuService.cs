using LaCantine.Data;
using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenusRepository repository;

        public MenuService(IMenusRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Menu> GetMenu(int id)
        {
            var Menu = await repository.GetMenuForID(id);
            return Menu;
        }
    }
}
