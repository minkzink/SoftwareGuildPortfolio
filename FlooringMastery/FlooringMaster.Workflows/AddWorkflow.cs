using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlooringMaster.BLL.Rules;
using FlooringMastery.UI.BLL;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class AddWorkflow : IWorkflow
    {
        public void Execute()
        {
            while(true)
            {
                Console.Clear();
                Order order = new Order();
                ConsoleIO.Display("Add Order");
                ConsoleIO.Display(ConsoleIO.SeperatorBar);

                DateTime orderDate = ConsoleIO.PromptDate("Please enter a future date: ");
                OrderManager manager = OrderManagerFactory.Create(orderDate);

                Console.Clear();
                string customerName = ConsoleIO.PromptName("Please enter a customer name: ");

                Console.Clear();
                ConsoleIO.Display("Here are the States that we currently Ship to:");
                ConsoleIO.Display(manager.GetStates());
                ConsoleIO.Display("\n");
                string state = ConsoleIO.Prompt($"Please enter a valid state: ");

                Console.Clear();
                ConsoleIO.Display("Here are the products that we currently offer:");
                ConsoleIO.Display(manager.GetProducts());
                ConsoleIO.Display("\n");
                string productType = ConsoleIO.Prompt($"Please enter a valid product");

                Console.Clear();
                decimal area = ConsoleIO.PromptDecimal("Please enter the area (Min. order size: 100 sq. ft.): ");
                Response<Order> response = new Response<Order>();

                    response = manager.AddSingleOrder(orderDate, customerName, state, productType, area);

                if (response.Success)
                {
                    if (response.Success)
                    {
                        ConsoleIO.Display(response.Data);
                        if (ConsoleIO.Prompt("Do you want to add this order? Y/N").ToUpper() == "Y")
                        {
                            manager.AddOrder(response.Data, orderDate);
                            ConsoleIO.Display("The order has been added!");
                            Thread.Sleep(1200);
                            Console.Clear();
                            break;
                        }
                    }
                    else
                    {
                        ConsoleIO.Display(response.Message);
                    }
                }
                else
                {
                    Console.Clear();
                    ConsoleIO.Display(
                        $"An error has occured\n{response.Message}\nPress Any Key To Continue Editing\nPress Enter to return to the main menu.");
                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key.Equals(ConsoleKey.Enter))
                    {
                        Console.Clear();
                        break;
                    }
                }
            }
        }
    }
}
