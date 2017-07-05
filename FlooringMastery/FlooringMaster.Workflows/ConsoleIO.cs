using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Data;

namespace FlooringMastery.UI.Models
{
    public class ConsoleIO
    {
        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        public static string SeperatorBar = "******************************************************************";

        public static void Display(Order order)
        {
            Console.WriteLine("Order");
            Console.WriteLine(SeperatorBar);
            Console.WriteLine($"{order.OrderNumber} | {order.OrderDate:d}");
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"{order.State}");
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost:C2}");
            Console.WriteLine($"Labor: {order.LaborCost:C2}");
            Console.WriteLine($"Tax: {order.Tax:C2}");
            Console.WriteLine($"Total: {order.Total:C2}");
            Console.WriteLine($"******************************************************************");
        }

        public static void Display(List<Product> products)
        {
            products.ForEach(p => Console.WriteLine($"{p.ProductType}, "));
        }

        public static void Display(List<Tax> states)
        {
            states.ForEach(p => Console.WriteLine($"{p.StateName}, "));
        }

        public static string Prompt(string prompt)
        {
            while (true)
            {
                Display(prompt);
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Display("You must enter valid text.");
                    Display("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static decimal PromptDecimal(string prompt)
        {
            decimal d;
            Display(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out d))
            {
                Display("You must enter a valid decimal.");
                Display("Please try Again");
                Console.ReadKey();
            }
            return d;
        }

        public static int PromptInt(string prompt)
        {
            int number;
            Display(prompt);

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Display("Id not found. Please enter a valid ID!");
            }
            return number;
        }

        public static DateTime PromptDate(string message)
        {
            DateTime r = new DateTime();
            while (!DateTime.TryParse(Prompt(message), out r))
            {
                Display("Invalid Date. Please enter a valid date");
            }
            Console.WriteLine("\n");
            return r;
        }

        public static string PromptName(string message)
        {
            while (true)
            {
                Display(message);
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Display("You must enter valid text.");
                    Display("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static string PromptEdit(string prompt)
        {
            string input = "";
            while (true)
            {
                Display(prompt);
                ConsoleIO.Display("\nPress Any Key To Change\nPress Enter To Skip Change");
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key.Equals(ConsoleKey.Enter))
                {
                    break;
                }
                else
                {
                    ConsoleIO.Display("\nPlease enter a new name: ");
                    input = Console.ReadLine();

                }
                if (string.IsNullOrEmpty(input))
                {
                    Display("You must enter valid text.");
                    Display("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    break;
                }
            }
            return input;
        }

        public static decimal PromptDecimalEdit(string prompt)
        {
            decimal d;
            while (true)
            {
                Display(prompt);
                ConsoleIO.Display("\nPress Any Key To Change\nPress Enter To Skip Change");
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key.Equals(ConsoleKey.Enter))
                {
                    d = -1M;
                    return d;
                }
                ConsoleIO.Display("\nPlease enter a new Decimal: ");
                while (!decimal.TryParse(Console.ReadLine(), out d))
                {
                    Display("You must enter a valid decimal.");
                    Display("Press Any Key to Continue");
                    Console.ReadKey();
                }
                return d;
            }
        }
    }
}