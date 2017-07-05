using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlooringMastery.UI.BLL;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class EditWorkflow : IWorkflow
    {
        public void Execute()
        {
            while (true)
            {

                Console.Clear();
                Order order = new Order();
                ConsoleIO.Display("Edit Order");
                ConsoleIO.Display(ConsoleIO.SeperatorBar);

                DateTime orderDate = ConsoleIO.PromptDate("Please enter a valid date: ");
                OrderManager manager = OrderManagerFactory.Create(orderDate);

                int id = ConsoleIO.PromptInt("Please enter a valid Order Number");

                Response<Order> response = manager.GetOrderById(orderDate, id);

                if (response.Success)
                {

                    //Get new Customer Name
                    Console.Clear();
                    string originalCustomerName = $"{response.Data.CustomerName}";
                    ConsoleIO.Display($"Current Customer Name: {response.Data.CustomerName}");
                    response.Data.CustomerName = ConsoleIO.PromptEdit("Please Enter a New Customer Name: ");
                    if (string.IsNullOrEmpty(response.Data.CustomerName))
                    {
                        response.Data.CustomerName = originalCustomerName;
                    }
                    ConsoleIO.Display($"New Current Customer Name: {response.Data.CustomerName}");
                    Thread.Sleep(0800);

                    //Get new State
                    Console.Clear();
                    string originalState = $"{response.Data.State}";
                    ConsoleIO.Display($"Current State: {response.Data.State}");
                    response.Data.State = ConsoleIO.PromptEdit("Please Enter a New State: ");
                    if (string.IsNullOrEmpty(response.Data.State))
                    {
                        response.Data.State = originalState;
                    }
                    ConsoleIO.Display($"New Current State: {response.Data.State}");
                    Thread.Sleep(0800);

                    //Get new ProductType
                    Console.Clear();
                    string originalProduct = $"{response.Data.ProductType}";
                    ConsoleIO.Display($"Current Product: {response.Data.ProductType}");
                    Thread.Sleep(1200);
                    ConsoleIO.Display("\nCurrently Offered Products:");
                    ConsoleIO.Display(manager.GetProducts());
                    response.Data.ProductType = ConsoleIO.PromptEdit("\nPlease Enter a New Product Type: ");
                    if (string.IsNullOrEmpty(response.Data.ProductType))
                    {
                        response.Data.ProductType = originalProduct;
                    }
                    ConsoleIO.Display($"New Current Product: {response.Data.ProductType}");
                    Thread.Sleep(0800);

                    //Get new Area
                    Console.Clear();
                    decimal originalArea = response.Data.Area;
                    ConsoleIO.Display($"Current Area: {response.Data.Area}");
                    response.Data.Area = ConsoleIO.PromptDecimalEdit("\nPlease Enter a New Area: ");
                    if (response.Data.Area < 0M)
                    {
                        response.Data.Area = originalArea;
                    }
                    ConsoleIO.Display($"New Current Area: {response.Data.Area}");
                    Thread.Sleep(0800);
                    Console.Clear();

                    //Get Edited Order
                    try
                    {
                        response = manager.EditSingleOrder(id, orderDate, response.Data.CustomerName,
                            response.Data.State, response.Data.ProductType, response.Data.Area);
                    }
                    catch (Exception e)
                    {
                        response.Success = false;
                        ConsoleIO.Display(e.Message);
                    }
                    if (response.Success)
                    {
                        if (response.Success)
                        {
                            Console.Clear();
                            ConsoleIO.Display(response.Data);
                            if (ConsoleIO.Prompt("Do you want to save this edited order? Y/N").ToUpper() == "Y")
                            {
                                manager.ReplaceOrder(response.Data);
                                ConsoleIO.Display("The order has been edited!");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            break;
                        }
                    }
                }
                else
                {

                    ConsoleIO.Display(
                        $"An error has occured\n{response.Message}\n\nPress Any Key To Go Back to The Add Screen\nPress Enter to return to the main menu.");
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
