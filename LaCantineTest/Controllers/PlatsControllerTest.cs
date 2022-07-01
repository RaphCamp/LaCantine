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
    public class PlatsControllerTest
    {
        private LaCantineContext context;

        public PlatsControllerTest()
        {
            var options =
                new DbContextOptionsBuilder<LaCantineContext>()
                    .UseInMemoryDatabase(databaseName: "WeatherDb")
                    .Options;
            context = new LaCantineContext(options);
            var entity = new Plats
            {
                
            };

            context.Plats.Add(entity);
            context.SaveChanges();
        }

/*        [TestMethod]
        public async Task Getplat_date_ok()
        {
            var repository = new DBPlatsRepository(context);
            var service = new PlatsService(repository);
            var controller = new PlatsController(null,service);

            var result = await controller.GetPlats(1);
            Assert.IsInstanceOfType(result,typeof(ActionResult<Plats>));
            var okObject = result as ActionResult<Plats>;
          
            Assert.AreEqual(new DateTime(2001, 02, 03),okObject.Value.Date);
        }*/
    }
}
