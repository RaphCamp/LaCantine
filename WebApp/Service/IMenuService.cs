using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Service
{
    public interface IMenuService
    {
       Task<Menu>GetMenu(int id);
    }
}
