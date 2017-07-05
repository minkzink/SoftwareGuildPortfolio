using FlooringMaster.Data;
using FlooringMastery.UI.BLL;
using FlooringMastery.UI.Data;
using FlooringMastery.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMaster.BLL
{
    [TestFixture]
    public class FlooringMaster
    {
        [Test]
        public void CanReadDataFromFile()
        {
            DateTime orderDate = new DateTime(1987, 08, 24);
            OrderManager manager = OrderManagerFactory.Create(orderDate);
            Response<IEnumerable<Order>> response = manager.GetOrdersByDate(orderDate);

            Assert.AreEqual("Wise", response.Data.FirstOrDefault(o => o.OrderNumber == 2).CustomerName);
            Assert.AreEqual("OH", response.Data.FirstOrDefault(o => o.OrderNumber == 2).State);
            Assert.AreEqual(6.25M, response.Data.FirstOrDefault(o => o.OrderNumber == 2).TaxRate);
            Assert.AreEqual("Wood", response.Data.FirstOrDefault(o => o.OrderNumber == 2).ProductType);
            Assert.AreEqual(100.00M, response.Data.FirstOrDefault(o => o.OrderNumber == 2).Area);
            Assert.AreEqual(5.15M, response.Data.FirstOrDefault(o => o.OrderNumber == 2).CostPerSquareFoot);
            Assert.AreEqual(4.75M, response.Data.FirstOrDefault(o => o.OrderNumber == 2).LaborCostPerSquareFoot);
        }

        [Test]
        public void CanAddOrder()
        {

            DateTime orderDate = new DateTime(2017, 08, 24);
            string customerName = "Bob";
            string state = "Ohio";
            string productType = "Wood";
            decimal area = 160M;

            OrderManager manager = OrderManagerFactory.Create(orderDate);
            Response<Order> response = new Response<Order>();
            response = manager.AddSingleOrder(orderDate, customerName, state, productType, area);

            Assert.AreEqual(response.Success, true);

            manager.AddOrder(response.Data, orderDate);

            Response<IEnumerable<Order>> response2 = manager.GetOrdersByDate(orderDate);

            Assert.AreEqual("Bob", response2.Data.FirstOrDefault(o => o.OrderNumber == 5).CustomerName);
            Assert.AreEqual("OH", response2.Data.FirstOrDefault(o => o.OrderNumber == 5).State);
            Assert.AreEqual("Wood", response2.Data.FirstOrDefault(o => o.OrderNumber == 5).ProductType);
            Assert.AreEqual(160M, response2.Data.FirstOrDefault(o => o.OrderNumber == 5).Area);
        }

        [Test]
        public void CanEditOrder()
        {
            DateTime orderDate = new DateTime(1987, 08, 24);
            OrderManager manager = OrderManagerFactory.Create(orderDate);
            int id = 1;

            Response<Order> response = manager.GetOrderById(orderDate, id);
            response.Data.CustomerName = "AbCD Inc";
            response.Data.State = "MI";
            response.Data.ProductType = "Tile";
            response.Data.Area = 120M;

            response = manager.EditSingleOrder(id, orderDate, response.Data.CustomerName, response.Data.State,
                response.Data.ProductType, response.Data.Area);

            Assert.AreEqual(response.Success, true);

            manager.ReplaceOrder(response.Data);

            Response<IEnumerable<Order>> response2 = manager.GetOrdersByDate(orderDate);

            Assert.AreEqual("AbCD Inc", response2.Data.FirstOrDefault(o => o.OrderNumber == 1).CustomerName);
            Assert.AreEqual("MI", response2.Data.FirstOrDefault(o => o.OrderNumber == 1).State);
            Assert.AreEqual("Tile", response2.Data.FirstOrDefault(o => o.OrderNumber == 1).ProductType);
            Assert.AreEqual(120M, response2.Data.FirstOrDefault(o => o.OrderNumber == 1).Area);
        }

        [Test]
        public void CanRemoveOrder()
        {
            DateTime orderDate = new DateTime(1987, 08, 24);
            int id = 1;
            OrderManager manager = OrderManagerFactory.Create(orderDate);
            Response<Order> response = manager.GetOrderById(orderDate, id);

            string originalName = response.Data.CustomerName;

            Assert.AreEqual(response.Success, true);

            Order order = response.Data;
            response = manager.RemoveOrder(orderDate, order.OrderNumber);
            manager.RemoveOrder(orderDate, id);

            Response<Order> response2 = manager.GetOrderById(orderDate, id);

            Assert.AreEqual(response.Data, response2.Data);
        }
    }
}
