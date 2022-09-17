using ConsoleApp1.Config;
using ConsoleShop.Commands.Base;
using ConsoleShop.Controller.Base;
using ConsoleShop.Dal;
using ConsoleShop.Dal.Exception;
using ConsoleShop.DataSeed;
using ConsoleShop.Services;
using ConsoleShop.View;
using ConsoleShop.View.Base;
using ConsoleShop.View.Factories;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    ///  Console application .NET Core that simulates the work of an online store
    /// </summary>
    public static class Program
    {
        static void Main(string[] args)
        {
            //init dependencies
            IShopContext shopContext = DataSeeder.InitContext();
            ISession session = new ShopSession();

            List<ICommand> commands = new List<ICommand>();
            ControllerFactory factory = new ControllerFactory(shopContext, session, commands);
            commands.AddRange(ShopCommands.Init(factory, new ErrorViewFactory()));

            RequestHelper requestHelper = new RequestHelper(commands);

            Console.Title = "Console EShop | Fedorchenko Anatolii | .Net Design and Architecture";

            string request = "help";

            while (request != "exit")
            {
                IActionResult result;

                try
                {
                    try
                    {
                        (ICommand command, string commandArgs) = requestHelper.ReadRequest(request);

                        result = command.Execute(session.User, commandArgs);
                    }
                    catch (CustomRepoException ex)
                    {
                        result = new ErrorView(ActionResult.Error, ex.Message, null);
                    }
                    catch (NotFoundException ex)
                    {
                        result = new ErrorView(ActionResult.Error, ex.Message, null);
                    }

                    ((BaseView)result).Render(session);

                }
                catch (Exception)
                {
                    Console.WriteLine("Sorry, something go wrong, please repeat your request");
                }

                request = Console.ReadLine();
            }
        }
    }
}
