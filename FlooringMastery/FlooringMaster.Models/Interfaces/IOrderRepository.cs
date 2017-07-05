using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;

namespace FlooringMastery.UI.Data
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order UpdateOrder(Order order);
        Order UpdateEditedOrder(Order order);
        void RemoveOrder(DateTime orderDate, int orderToRemove);
        List<Order> GetOrders(DateTime orderDate);
        Order GetOrderById(DateTime d, int id);
        List<Order> GetOrdersByDate(DateTime d);
        void Save(List<Order> orders);
    }
}
