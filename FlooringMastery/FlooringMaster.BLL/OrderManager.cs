using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMaster.BLL.Rules;
using FlooringMastery.UI.Data;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Interfaces;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMastery.UI.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;

        public OrderManager(IOrderRepository orderRepository, ITaxRepository taxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = taxRepository;
            _productRepository = productRepository;
        }

        public Response<Order> ReplaceOrder(Order order)
        {
            Response<Order> response = new Response<Order>();
            try
            {
                _orderRepository.UpdateOrder(order);
                _orderRepository.Save(response.Orders);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
        public Response<Order> RemoveOrder(DateTime orderDate, int id)
        {
            Response<Order> response = new Response<Order>();
            try
            {
                Order order = _orderRepository.GetOrderById(orderDate, id);
                _orderRepository.RemoveOrder(order.OrderDate, order.OrderNumber);
                _orderRepository.Save(response.Orders);
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
        public Response<Order> AddOrder(Order ord, DateTime orderDate)
        {
            Response<Order> response = new Response<Order>();
            try
            {
                _orderRepository.AddOrder(ord);
                _orderRepository.Save(response.Orders);
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public Response<IEnumerable<Order>> GetOrders(DateTime orderDate)
        {
            Response<IEnumerable<Order>> response = new Response<IEnumerable<Order>>();
            try
            {
                response.Data = _orderRepository.GetOrders(orderDate);
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public Response<Order> GetOrderById(DateTime d, int id)
        {
            Response<Order> result = new Response<Order>();
            try
            {
                result.Data = _orderRepository.GetOrderById(d, id);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
        public Response<IEnumerable<Order>> GetOrdersByDate(DateTime d)
        {
            Response<IEnumerable<Order>> result = new Response<IEnumerable<Order>>();
            try
            {
                result.Data = _orderRepository.GetOrdersByDate(d);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public List<Product> GetProducts()
        {
            List<Product> prod = (List<Product>) _productRepository.GetProducts();
            return prod;
        }

        public List<Tax> GetStates()
        {
            List<Tax> states = (List<Tax>)_taxRepository.GetTaxes();
            return states;
        }

        public Response<Order> AddSingleOrder(DateTime orderDate, string customerName, string state, string productType, decimal area)
        {
            Response<Order> r = new Response<Order>();
            Tax tax = new Tax();
            Product product = new Product();
            try
            {
                tax = _taxRepository.GetTaxesByState(state);
            }
            catch (Exception e)
            {
                r.Message += e.Message;
            }
            try
            {
                product = _productRepository.GetProductByName(productType);
            }
            catch(Exception e)
            {
                r.Message += e.Message;
            }
            int orderNumber = _orderRepository.GetOrders(orderDate).Count() + 1;
            OrderAddRules rule = new OrderAddRules();
            r = rule.Rules(orderDate, customerName, state, productType, area, tax, product, orderNumber);
            return r;
        }

        public Response<Order> EditSingleOrder(int id, DateTime orderDate, string customerName, string state, string productType, decimal area)
        {
            Response<Order> r = new Response<Order>();
            Tax tax = _taxRepository.GetTaxesByState(state);
            Product product = _productRepository.GetProductByName(productType);
            int orderNumber = id;
            OrderEditRules rule = new OrderEditRules();
            r = rule.Rules(orderDate, customerName, state, productType, area, tax, product, orderNumber);
            return r;
        }
    }
}
