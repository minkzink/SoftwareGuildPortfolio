using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMaster.Data;
using FlooringMastery.UI.Data;

namespace FlooringMastery.UI.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create(DateTime d)
        {
            string mode = ConfigurationManager.AppSettings["Directory"];
            OrderManager result;
            switch (mode)
            {
                case "Test":
                    result = new OrderManager(new TestOrderRepository(), new TestTaxRepository(), new TestProductRepository());
                    break;
                case "Prod":
                    string filename = ConfigurationManager.AppSettings["Filepath"];
                    result = new OrderManager(new ProdOrderRepository($"{filename}Orders_{d:MMddyyyy}.txt"), new ProdTaxRepository($"{filename}taxes.txt"), new ProdProductRepository($"{filename}Products.txt"));
                    break;
                default:
                    result = new OrderManager(new TestOrderRepository(), new TestTaxRepository(), new TestProductRepository());
                    break;
            }
            return result;
        }
    }
}
