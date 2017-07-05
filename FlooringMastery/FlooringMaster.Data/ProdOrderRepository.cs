using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;
using FlooringMastery.UI;
using System.IO;
using FileHelpers;

namespace FlooringMastery.UI.Data
{
    public class ProdOrderRepository : IOrderRepository
    {
        private string _filename;
        private static List<Order> _orders = new List<Order>();

        public ProdOrderRepository(string filename)
        {
            _filename = filename;
        }

        public void AddOrder(Order order)
        {
            order.OrderNumber = _orders.Count()+1;
            _orders.Add(order);
        }

        public void EditOrder(Order order)
        {
            order.OrderNumber = order.OrderNumber;
            _orders.Add(order);
        }

        public void RemoveOrder(DateTime orderDate, int orderToRemove)
        {
            _orders.RemoveAll(o => o.OrderNumber == orderToRemove);
        }

        public Order UpdateOrder(Order order)
        {
            RemoveOrder(order.OrderDate, order.OrderNumber);
            EditOrder(order);
            return order;
        }

        public Order UpdateEditedOrder(Order order)
        {
            RemoveOrder(order.OrderDate, order.OrderNumber);
            AddOrder(order);
            return order;
        }

        public List<Order> GetOrders(DateTime orderDate)
        {
            List<Order> results;
            results = Load(orderDate);
            return results;
        }

        public Order GetOrderById(DateTime d, int id)
        {
            Order result;
            List<Order> orders = Load(d);
            result = orders.FirstOrDefault(o => o.OrderNumber == id);
            if (result == null)
            {
                throw new Exception("Order Id does not exist!");
            }
            return result;
        }

        public List<Order> GetOrdersByDate(DateTime d)
        {
            List<Order> orders = Load(d).Where(o => o.OrderDate == d).ToList();
            if (orders.Count() == 0)
            {
                throw new Exception("No orders exist for this date.");
            }
            return orders;
        }

        private List<Order> Load(DateTime orderDate)
        {
            List<Order> orders = new List<Order>();
            if (File.Exists(_filename))
            {
                using (StreamReader sr = new StreamReader(_filename))
                {
                    sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("|"))
                        {
                            string[] fields = line.Split('|');
                            Order order = new Order();
                            order.OrderDate = orderDate;
                            order.OrderNumber = int.Parse(fields[0]);
                            order.CustomerName = fields[1];
                            order.State = fields[2];
                            order.TaxRate = decimal.Parse(fields[3]);
                            order.ProductType = fields[4];
                            order.Area = decimal.Parse(fields[5]);
                            order.CostPerSquareFoot = decimal.Parse(fields[6]);
                            order.LaborCostPerSquareFoot = decimal.Parse(fields[7]);
                            orders.Add(order);
                        }
                        else
                        {
                            string[] fields = line.Split(',');
                            Order order = new Order();
                            order.OrderDate = orderDate;
                            order.OrderNumber = int.Parse(fields[0]);
                            order.CustomerName = fields[1];
                            order.State = fields[2];
                            order.TaxRate = decimal.Parse(fields[3]);
                            order.ProductType = fields[4];
                            order.Area = decimal.Parse(fields[5]);
                            order.CostPerSquareFoot = decimal.Parse(fields[6]);
                            order.LaborCostPerSquareFoot = decimal.Parse(fields[7]);
                            orders.Add(order);
                        }

                    }
                }
                _orders = orders;
            }
            return orders;
        }

        public void Save(List<Order> orders)
        {
            if (File.Exists(_filename))
                File.Delete(_filename);
            orders = _orders;
            using (StreamWriter sw = new StreamWriter(_filename))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                foreach (var order in orders)
                {
                    sw.WriteLine($"{order.OrderNumber}|{order.CustomerName}|{order.State}|{order.TaxRate}|{order.ProductType}|{order.Area}|{order.CostPerSquareFoot}|{order.LaborCostPerSquareFoot}|{order.MaterialCost}|{order.LaborCost}|{order.Tax}|{order.Total}");
                }

            }
        }
    }
}
