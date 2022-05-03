using LaCantine.Data;
using LaCantine.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Service
{
    public class CommandesService : ICommandesService
    {
        private readonly ICommandesRepository repository;

        public CommandesService (ICommandesRepository repository)
        {
            this.repository = repository;
        }
        public Commandes GetCommandes(int id)
        {
            var commandes = repository.GetCommandesForID(id);
            return commandes;
        }
    }
}
