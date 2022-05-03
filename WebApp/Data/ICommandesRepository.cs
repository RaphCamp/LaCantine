using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Data
{
    public interface ICommandesRepository
    {
        Commandes GetCommandesForID(int iD);
    }
}
