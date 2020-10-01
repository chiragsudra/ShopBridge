using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridgeApp.Controllers;
using ShopBridgeApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopBridgeApp.Tests.Controllers
{
    [TestClass]
    public class ItemMasterControllerTest
    {
        private IEnumerable<ItemMaster> _db = new List<ItemMaster>();

        public ItemMasterControllerTest()
        {
            InitiateContext();
        }


        private void InitiateContext()
        {
            List<ItemMaster> items = new List<ItemMaster>();
            for (int i = 1; i <= 50; i++)
            {
                items.Add(new ItemMaster { ItemID = i, ItemName = "Item " + i, Description = "Description " + i, Price = i + 10 });
            }

            _db = items;
        }

        [TestMethod]
        public void Index()
        {
            ItemMasterController controller = new ItemMasterController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            ItemMasterController controller = new ItemMasterController();

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

    }
}
