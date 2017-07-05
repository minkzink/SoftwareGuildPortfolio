using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMastery.UI.Data
{
    public class TestOrderRepository : IOrderRepository
    {
        private static List<Order> _orders = new List<Order>();
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;

        public TestOrderRepository()
        {
            if (!_orders.Any())
            {
                _orders.AddRange(new List<Order>()
                {
                    new Order
                    {
                        OrderDate = new DateTime(1987, 08, 24),
                        OrderNumber = 2,
                        CustomerName = "Wise",
                        State = "OH",
                        TaxRate = 6.25M,
                        ProductType = "Wood",
                        Area = 100.00M,
                        CostPerSquareFoot = 5.15M,
                        LaborCostPerSquareFoot = 4.75M,
                    },
                    new Order
                    {
                        OrderDate = new DateTime(1987, 08, 24),
                        OrderNumber = 1,
                        CustomerName = "Abco",
                        State = "OH",
                        TaxRate = 6.25M,
                        ProductType = "Laminate",
                        Area = 100.00M,
                        CostPerSquareFoot = 1.75M,
                        LaborCostPerSquareFoot = 2.10M
                    },
                    new Order
                    {
                        OrderDate = new DateTime(1987, 08, 19),
                        OrderNumber = 3,
                        CustomerName = "Gojo",
                        State = "PA",
                        TaxRate = 5.75M,
                        ProductType = "Wood",
                        Area = 120.00M,
                        CostPerSquareFoot = 5.35M,
                        LaborCostPerSquareFoot = 4.75M,
                    },
                    new Order
                    {
                        OrderDate = new DateTime(1987, 08, 19),
                        OrderNumber = 4,
                        CustomerName = "Sikja, PTY.",
                        State = "PA",
                        TaxRate = 5.75M,
                        ProductType = "Steel",
                        Area = 100.00M,
                        CostPerSquareFoot = 5.55M,
                        LaborCostPerSquareFoot = 7.75M,
                    },

                });
            }
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
            List<Order> orders = Load(d).Where(o => o.OrderDate == d).ToList() ;
            if (orders.Count() == 0)
            {
                throw new Exception("No orders exist for this date.");
            }
            return orders;
        }

        private List<Order> Load(DateTime orderDate)
        {
            List<Order> orders = new List<Order>();
            orders = _orders;
            return orders;
        }

        public void Save(List<Order> orders)
        {
            orders = _orders;
        }
    }
}
