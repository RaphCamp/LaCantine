using LaCantine.Controllers;
using LaCantine.Data;
using LaCantine.Model;
using LaCantine.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LaCantineTest
{
    [TestClass]
    public class CommandesControllerTest
    {
        private LaCantineContext context;

        public CommandesControllerTest()
        {
            var options =
                new DbContextOptionsBuilder<LaCantineContext>()
                    .UseInMemoryDatabase(databaseName: "WeatherDb")
                    .Options;
            context = new LaCantineContext(options);
            var entity = new Commandes
            {
                Date = new DateTime(2001, 02, 03),

            };

            context.Commandes.Add(entity);
            context.SaveChanges();
        }

        [TestMethod]
        public async Task Getcommande_date_ok()
        {
            var repository = new DBCommandesRepository(context);
            var service = new CommandesService(repository);
            var controller = new CommandesController(null,service);

            var result = await controller.GetCommandes(1);
            Assert.IsInstanceOfType(result,typeof(ActionResult<Commandes>));
            var okObject = result as ActionResult<Commandes>;
          
            Assert.AreEqual(new DateTime(2001, 02, 03),okObject.Value.Date);
        }
    }
}
