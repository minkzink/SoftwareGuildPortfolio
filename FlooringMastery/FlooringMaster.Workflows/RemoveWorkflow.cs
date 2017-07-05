using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.BLL;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveWorkflow : IWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            ConsoleIO.Display("Remove Order");
            ConsoleIO.Display(ConsoleIO.SeperatorBar);
            DateTime orderDate = ConsoleIO.PromptDate("Please enter a valid date: ");
            int id = ConsoleIO.PromptInt("Please enter a valid Order Number");
            OrderManager manager = OrderManagerFactory.Create(orderDate);
            Response<Order> response = manager.GetOrderById(orderDate, id);
            if (response.Success)
            {
                Console.Clear();
                Order order = response.Data;
                ConsoleIO.Display(order);
                if (ConsoleIO.Prompt("Are you sure you want to delete this order? Y/N").ToUpper() == "Y")
                {
                    response = manager.RemoveOrder(orderDate, order.OrderNumber);
                    if (response.Success)
                    {
                        manager.RemoveOrder(orderDate, id);
                        ConsoleIO.Display("The order has been deleted");
                    }
                    else
                    {
                        ConsoleIO.Display(response.Message);
                    }
                }
            }
            else
            {
                ConsoleIO.Display($"An error has occured\n{response.Message}");
            }
            ConsoleIO.Display("Press Enter To Continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
            Console.Clear();
        }
    }
}
