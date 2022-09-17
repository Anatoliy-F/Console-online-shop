using ConsoleShop.Commands;
using ConsoleShop.Commands.Base;
using ConsoleShop.Commands.ViewModels;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Config
{   
    /// <summary>
    /// Return list of configured commands, available in application.
    /// Actualy configures commands available via UI
    /// </summary>
    public static class ShopCommands
    {
        /// <summary>
        /// Return list of configured commands
        /// </summary>
        /// <param name="factory">Instance of ControllerFactory, that provide access to configured controllers</param>
        /// <param name="errorFactory">Instance of IActionResult factory, 
        /// which provides a response object in case of an error while executing the command</param>
        /// <returns>List&lt;ICommand&gt;</returns>
        public static List<ICommand> Init(ControllerFactory factory, IActionResultFactory errorFactory)
        {
            List<ICommand> commands = new List<ICommand>();

            //Set error factory
            BaseCommand.ErrorViewFactory = errorFactory;

            commands.AddRange(new ICommand[]
            {
                new Command(
                    "ShowAll",
                    "View all available items. Pattern: [Showall]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    () =>
                    {
                        return factory.GetProductController().ShowAll();
                    }
                    ),
                new CommandStrArg(
                    "ByName",
                    "Search item by name, type command then name of searching item. Pattern: [ByName Name<word>]",
                    new[] {UserRole.Guest, UserRole.RegisteredUser, UserRole.Administrator},
                    (str) =>
                    {
                        return factory.GetProductController().ShowByName(str);
                    }
                    ),
                new CommandStrArg(
                    string.Empty,
                    string.Empty,
                    new[] {UserRole.Guest, UserRole.RegisteredUser, UserRole.Administrator},
                    (str) =>
                    {
                        return factory.GetErrorController().ShowError(str);
                    }
                    ),
                new Command2WordArg(
                    "Login",
                    "Login and by best goods ever! Pattern: [Login Username<word> Password<word>]",
                    new[] {UserRole.Guest},
                    (w1, w2) =>
                    {
                        return factory.GetUserController().Login(w1, w2);
                    }
                    ),
                new Command(
                    "Logout",
                    "Terminate session, we are looking to see you soon! Pattern: [Logout]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    () =>
                    {
                        return factory.GetUserController().LogOut();
                    }
                    ),
                new Command(
                    "Help",
                    "Show all available commands in system. Pattern: [Help]",
                    new[] {UserRole.Guest, UserRole.RegisteredUser, UserRole.Administrator},
                    () =>
                    {
                        return factory.GetHelpController().ViewCommands();
                    }
                    ),
                new CommandStrArg(
                    "ShowCommand",
                    "Show concrete command. Pattern: [ShowCommand Command<word>]",
                    new[] {UserRole.Guest, UserRole.RegisteredUser, UserRole.Administrator},
                    (str) =>
                    {
                        return factory.GetHelpController().ShowCommand(str);
                    }
                    ),
                new Command3WordArg(
                    "Register",
                    "Register in best E-Shop! Pattern: [Register Name<word> Password<word> Repeatpassword<word>]",
                    new[] {UserRole.Guest},
                    (w1, w2, w3) =>
                    {
                        return factory.GetUserController().RegisterAcount(w1, w2, w3);
                    }
                    ),
                new Command1NumArg(
                    "OneToCart",
                    "Add product to Cart! GoodDeal! Pattern: [OneToCart ProductId<int>]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    (id) =>
                    {
                        return factory.GetCartController().AddToCart(id);
                    }
                    ),
                new Command2NumArg(
                    "AddToCart",
                    "Add product to Cart! Good deal! Pattern: [AddToCart ProductId<int> Quantity<int>]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    (id, q) =>
                    {
                        return factory.GetCartController().AddToCart(id, q);
                    }
                    ),
                new Command(
                    "ShowCart",
                    "Show contents of your shopping cart. Pattern: [ShowCart]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    () =>
                    {
                        return factory.GetCartController().ShowCart();
                    }
                    ),
                new Command(
                    "ClearCart",
                    "Clear your cart. Pattern: [ClearCart]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    () =>
                    {
                        return factory.GetCartController().ClearCart();
                    }
                    ),
                new Command(
                    "ShowOrders",
                    "Show list of your orders. Pattern: [ShowOrders]",
                    new[] {UserRole.RegisteredUser},
                    () =>
                    {
                        return factory.GetOrderController().ShowOrders();
                    }
                    ),
                new Command2StrArg(
                    "MakeOrder",
                    "Make order. Pattern: [MakeOrder OrderName<\"string\"> Adress<\"string\">]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    (str1, str2) =>
                    {
                        return factory.GetCartController().MakeOrder(str1, str2);
                    }
                    ),
                new Command1NumArg(
                    "ConfirmReceipt",
                    "Confirm your order reception. Pattern: [ConfirmReceipt OrderId<int>]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    (id) =>
                    {
                        return factory.GetOrderController().ConfirmReceipt(id);
                    }
                    ),
                new Command2WordArg(
                    "ChangeName",
                    "Change your name. Pattern: [ChangeName NewName<word>, Password<word>]",
                    new[] {UserRole.RegisteredUser},
                    (w1, w2) =>
                    {
                        return factory.GetUserController().ChangeName(w1, w2);
                    }
                    ),
                new Command3WordArg(
                    "ChangePassword",
                    "Change your password. Pattern: [ChangePassword Password<word>, NewPassword<word>, RepeatNewPassword<word>]",
                    new[] {UserRole.RegisteredUser},
                    (w1, w2, w3) =>
                    {
                        return factory.GetUserController().ChangePassword(w1, w2, w3);
                    }
                    ),
                new Command1NumArg(
                    "CancelOrder",
                    "Cancel your order. Pattern: [CancelOrder OrderId<int>]",
                    new[] {UserRole.RegisteredUser, UserRole.Administrator},
                    (id) =>
                    {
                        return factory.GetOrderController().CancellOrder(id);
                    }
                    ),
                new Command(
                    "ShowAllUsers",
                    "Show all users registered in store. Pattern: [ShowAllUsers]",
                    new[] {UserRole.Administrator},
                    () =>
                    {
                        return factory.GetUserController().ShowAllUsers();
                    }
                    ),
                new Command1NumArg(
                    "ShowUserById",
                    "Show information about user with specified id. Pattern: [ShowUserById UserId<int>]",
                    new[] {UserRole.Administrator},
                    (id) =>
                    {
                        return factory.GetUserController().ShowUserById(id);
                    }
                    ),
                new Command(
                    "AddProduct",
                    "Add new Product in store. Pattern: [AddProduct]",
                    new[] { UserRole.Administrator },
                    () =>
                    {
                        IEnumerable<Category> categories = (IEnumerable<Category>)factory.GetCategoryController().ShowAll().ResultBody;
                        var arg = ProductViewModel.CreateProduct(categories);
                        return factory.GetProductController().AddNew(arg);
                    }
                    ),
                new Command1NumArg(
                    "UpdateProduct",
                    "Update product properties. Pattern: [UpdateProduct ProductId<int>]",
                    new[] { UserRole.Administrator },
                    (id) =>
                    {
                        Product product =  (Product)factory.GetProductController().ShowById(id).ResultBody.ElementAtOrDefault(0);
                        if(product != null){
                            IEnumerable<Category> categories = (IEnumerable<Category>)factory.GetCategoryController().ShowAll().ResultBody;
                            var arg = ProductViewModel.ChangeProduct(product, categories);
                            return factory.GetProductController().UpdateProduct(id, arg);
                        }
                        else
                        {
                            return factory.GetErrorController().ShowError("Product not found");
                        }
                    }
                    ),
                new Command(
                    "AllOrders",
                    "Show all orders in shop. Pattern: [AllOrders]",
                     new[] { UserRole.Administrator },
                     () =>
                     {
                         return factory.GetOrderController().ShowAllOrders();
                     }
                    ),
                new Command1NumArg(
                    "ChangeOrderStatus",
                    "Change order status. Pattern: [ChangeOrderStatus OrderId<int>]",
                     new[] {UserRole.Administrator },
                     (id) =>
                     {
                         var status = OrderStatusViewModel.SelectOrderStatus();
                         return factory.GetOrderController().ChangeOrderStatus(id, status);
                     }
                    ),
                new Command2WordArg(
                    "ChangeUserName",
                    "Change name of selected user. Pattern: [ChangeUserName userName<word> newUserName<word>]",
                    new[] {UserRole.Administrator },
                    (w1, w2) =>
                    {
                        return factory.GetUserController().ChangeUserName(w1, w2);
                    }
                    ),
                new Command3WordArg(
                    "ChangeUserPassword",
                    "Change password of selected user. Pattern: [ChangeUserPassword userName<word> password<word> repeatPassword<word>]",
                    new[] {UserRole.Administrator },
                    (w1, w2, w3) =>
                    {
                        return factory.GetUserController().ChangeUserPassword(w1, w2, w3);
                    }
                    ),
                new Command(
                    "ShowCategories",
                    "Show all categories. Pattern: [ShowCategories]",
                    new[] {UserRole.Administrator },
                    () =>
                    {
                        return factory.GetCategoryController().ShowAll();
                    }
                    )
            });

            return commands;
        }
    }
}
