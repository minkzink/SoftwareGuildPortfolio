using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlooringMaster.Models.Interfaces;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMaster.BLL.Rules
{
    public class OrderEditRules : IRules
    {
        public Response<Order> Rules(DateTime orderDate, string customerName, string state, string productType, decimal area, Tax tax, Product product, int orderNumber)
        {
            Response<Order> response = new Response<Order>();
            response.Message = "";
            response.Success = true;

            if (customerName == null)
            {
                response.Success = false;
                response.Message += "Customer Name Must Contain Characters.[a-z][0-9] Allowed.\n";
                return response;
            }

            bool regEx = new Regex("^[a-zA-Z0-9,\\. ]*$").IsMatch(customerName);
            if (regEx == false)
            {
                response.Success = false;
                response.Message += "Customer name can only contain characters A-Z, ,. or 0-9.\n";
            }

            if (string.IsNullOrEmpty(tax.StateAbbreviation) || string.IsNullOrEmpty(tax.StateName))
            {
                response.Success = false;
                response.Message += "We Do Not Currently Ship to This State.\n";
            }

            if (string.IsNullOrEmpty(product.ProductType))
            {
                response.Success = false;
                response.Message += "We Do Not Currently Offer This Product.\n";
            }

            if (area < 100)
            {
                response.Success = false;
                response.Message += "The minimum area per order is 100 sq. ft.\n";
            }
            Order order = new Order();
            order.OrderDate = orderDate;
            order.OrderNumber = orderNumber;
            order.CustomerName = customerName;
            order.State = tax.StateAbbreviation;
            order.TaxRate = tax.TaxRate;
            order.ProductType = productType;
            order.Area = area;
            order.CostPerSquareFoot = product.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            response.Data = order;
            return response;
        }
    }
}
