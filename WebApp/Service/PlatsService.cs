using LaCantine.Data;
using LaCantine.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Service
{
    public class PlatsService : IPlatsService
    {
        private readonly IPlatsRepository repository;

        public PlatsService(IPlatsRepository repository)
        {
            this.repository = repository;
        }
        public Plats GetPlats(int id)
        {
            var plats = repository.GetPlatsForID(id);
            return plats;
        }
    }
}
