using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Interfaces;

namespace FlooringMaster.Data
{
    public class TestProductRepository : IProductRepository
    {
        private static List<Product> _products = new List<Product>();

        public TestProductRepository()
        {
            if (!_products.Any())
            {
                _products.AddRange(new List<Product>()
                {
                    new Product()
                    {
                        ProductType = "Carpet",
                        CostPerSquareFoot = 2.25M,
                        LaborCostPerSquareFoot = 2.10M
                    },
                    new Product()
                    {
                        ProductType = "Laminate",
                        CostPerSquareFoot = 1.75M,
                        LaborCostPerSquareFoot = 2.10M
                    },
                    new Product()
                    {
                        ProductType = "Tile",
                        CostPerSquareFoot = 3.50M,
                        LaborCostPerSquareFoot = 4.15M
                    },
                    new Product()
                    {
                        ProductType = "Wood",
                        CostPerSquareFoot = 5.15M,
                        LaborCostPerSquareFoot = 4.75M
                    }
                });
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> results;
            results = Load();
            return results;
        }

        public Product GetProductByName(string productName)
        {
            Product result;
            List<Product> products = Load();
            result = products.FirstOrDefault(o => o.ProductType == productName);
            if (result == null)
            {
                throw new Exception("Product does not exist!");
            }
            return result;
        }

        private List<Product> Load()
        {
            List<Product> products = new List<Product>();
            products = _products;
            return products;
        }
    }
}
