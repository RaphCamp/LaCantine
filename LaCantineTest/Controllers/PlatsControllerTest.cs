using LaCantine.Controllers;
using LaCantine.Data;
using LaCantine.Model;
using LaCantine.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LaCantineTest
{
    [TestClass]
    public class PlatsControllerTest
    {
        private LaCantineContext context;
        private PlatsController controller;

        public PlatsControllerTest()
        {
            var options =
                new DbContextOptionsBuilder<LaCantineContext>()
                    .UseInMemoryDatabase(databaseName: "PlatDb")
                    .Options;
            context = new LaCantineContext(options);

            context.Plats.Add(new Plats
            {
                id = 1,
                name = "steak",
                desc = "miam",
                photo = "",
                prix = 5
            });
            context.Plats.Add(new Plats
            {
                id = 2,
                name = "salade",
                desc = "berk",
                photo = "",
                prix = 2
            });

            controller = new PlatsController(context);
        }

        [TestMethod]
        public async Task Get_Plats()
        {

            var result = await controller.GetPlats();
            //vérification type de la réponse
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Plats>>));
            //vérification du contenu de la réponse
            Assert.AreEqual(context.Plats.Count(), result.Value.Count());
        }

        [TestMethod]
        public async Task Get_Plats_by_id()
        {
            var result = await controller.GetPlats(1);
            //vérification type de la réponse
            Assert.IsInstanceOfType(result, typeof(ActionResult<Plats>));
            //vérification du contenu de la réponse
            Assert.AreEqual("steak", result.Value.name);
        }

        [TestMethod]
        public async Task PUT_Plats()
        {
            var steak = await controller.GetPlats(1);
            steak.Value.name = "lasagnes";
            await controller.PutPlats(1,steak.Value);
            var result = await controller.GetPlats(1);

            //vérification type de la réponse
            Assert.IsInstanceOfType(result, typeof(ActionResult<Plats>));
            //vérification du contenu de la réponse
            Assert.AreEqual("lasagnes", result.Value.name);
        }
    }
}
