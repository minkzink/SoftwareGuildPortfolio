using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;
using FlooringMastery.UI.Workflows;

namespace FlooringMastery.UI
{
    public class Menu
    {
        public static void Start()
        {
            System.Console.OutputEncoding = Encoding.UTF8;
            IWorkflow workflow = null;
            bool programRunning = true;
            while (programRunning)
            {
                string menu = "1. Display Orders\n2. Add an Order\n3. Edit an Order\n4. Remove an Order\n5. Quit";
                ConsoleIO.Display("Flooring Master Program - SWC™ Corp");
                ConsoleIO.Display(@"© 2017 SWC™ Corp. All Rights Reserved.");
                ConsoleIO.Display(ConsoleIO.SeperatorBar);
                string choice = ConsoleIO.Prompt(menu);
                switch (choice)
                {
                    case "1":
                        workflow = new DisplayWorkflow();
                        break;
                    case "2":
                        workflow = new AddWorkflow();
                        break;
                    case "3":
                        workflow = new EditWorkflow();
                        break;
                    case "4":
                        workflow = new RemoveWorkflow();
                        break;
                    case "5":
                        programRunning = false;
                        workflow = null;
                        break;
                }
                workflow?.Execute();
            }
        }
    }
}
