using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI;
using FlooringMastery.UI.BLL;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class DisplayWorkflow: IWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            ConsoleIO.Display("Display Orders");
            ConsoleIO.Display(ConsoleIO.SeperatorBar);
            DateTime orderDate = ConsoleIO.PromptDate("Please enter a valid date (MM/DD/YYYY): ");
            OrderManager manager = OrderManagerFactory.Create(orderDate);
            Response<IEnumerable<Order>> response = manager.GetOrdersByDate(orderDate);
            if (response.Success)
            {
                Console.Clear();
                foreach (var order in response.Data.OrderBy(O => O.OrderNumber))
                {
                    ConsoleIO.Display(order);
                    Console.WriteLine("\n");
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
