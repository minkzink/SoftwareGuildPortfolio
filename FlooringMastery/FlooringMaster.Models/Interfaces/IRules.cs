using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMaster.Models.Interfaces
{
    public interface IRules
    {
        Response<Order> Rules(DateTime orderDate, string customerName, string state, string productType, decimal area, Tax tax,
            Product product, int orderNumber);
    }
}
