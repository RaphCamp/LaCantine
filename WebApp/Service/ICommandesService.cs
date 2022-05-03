using LaCantine.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Service
{
    public interface ICommandesService
    {
        Commandes GetCommandes(int id);
    }
}
