using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Interfaces;

namespace FlooringMastery.UI.Data
{
    public class ProdProductRepository : IProductRepository
    {
        private string _filename;
        private static List<Product> _products = new List<Product>();

        public ProdProductRepository(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> results;
            results = Load();
            return results;
        }

        public Product GetProductByName(string name)
        {
            Product result;
            List<Product> products = Load();
            result = products.FirstOrDefault(o => o.ProductType == name);
            if (result == null)
            {
                throw new Exception("Product does not exist!");
            }
            return result;
        }

        private List<Product> Load()
        {
            List<Product> products = new List<Product>();
            using (StreamReader sr = new StreamReader(_filename))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("|"))
                    {
                        string[] fields = line.Split(',');
                        Product product = new Product();
                        product.ProductType = fields[0];
                        product.CostPerSquareFoot = decimal.Parse(fields[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(fields[2]);
                        products.Add(product);
                    }
                    else
                    {
                        string[] fields = line.Split(',');
                        Product product = new Product();
                        product.ProductType = fields[0];
                        product.CostPerSquareFoot = decimal.Parse(fields[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(fields[2]);
                        products.Add(product);
                    }
                }
                _products = products;
                return products;
            }
        }
    }
}
