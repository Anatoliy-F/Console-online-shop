using ConsoleShop.Commands.Base;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using ConsoleShop.Dal;
using ConsoleShop.Dal.Repos;
using ConsoleShop.View.Factories;
using System.Collections.Generic;

namespace ConsoleShop.Config
{
    /// <summary>
    /// Returns controller objects by injecting dependencies through the constructor.
    /// Actually configures the application controllers.
    /// </summary>
    public class ControllerFactory
    {
        /// <summary>
        /// Data store <see cref="ConsoleShop.Dal.IShopContext"/>
        /// </summary>
        private readonly IShopContext _shopContext;

        /// <summary>
        /// Current user session <see cref="ConsoleShop.Controller.Base.ISession"/>
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// List of available commands <see cref="ConsoleShop.Commands.Base.BaseCommand"/>
        /// </summary>
        private readonly IEnumerable<ICommand> _commands;

        /// <summary>
        /// Initialize new instance of Controller factory
        /// </summary>
        /// <param name="shopContext">Data store <see cref="ConsoleShop.Dal.IShopContext"/></param>
        /// <param name="session">Current user session <see cref="ConsoleShop.Controller.Base.ISession"/></param>
        /// <param name="commands">List of available commands <see cref="ConsoleShop.Commands.Base.BaseCommand"/></param>
        public ControllerFactory(IShopContext shopContext, ISession session, IEnumerable<ICommand> commands)
        {
            _shopContext = shopContext;
            _session = session;
            _commands = commands;
        }

        /// <summary>
        /// Returns the configured ProductController
        /// </summary>
        /// <returns>ProductController instance <see cref="ConsoleShop.Controller.ProductController"/></returns>
        public IProductController GetProductController()
        {
            return new ProductController(new ProductRepo(_shopContext), new ProductViewFactory());
        }

        /// <summary>
        /// Returns the configured CartController
        /// </summary>
        /// <returns>CartController instance <see cref="Controller.CartController"/></returns>
        public ICartController GetCartController()
        {
            return new CartController(new ProductRepo(_shopContext), new OrderRepo(_shopContext), _session, new CartViewFactory());
        }

        /// <summary>
        /// Returns the configured UserController
        /// </summary>
        /// <returns>UserController instance <see cref="Controller.UserController"/></returns>
        public IUserController GetUserController()
        {
            return new UserController(new UserRepo(_shopContext), _session, new LoginViewFactory());
        }

        /// <summary>
        /// Returns the configured OrderController
        /// </summary>
        /// <returns>OrderController instance <see cref="Controller.OrderController"/></returns>
        public IOrderController GetOrderController()
        {
            return new OrderController(new OrderRepo(_shopContext), new OrderViewFactory(), _session);
        }

        /// <summary>
        /// Returns the configured ErrorController
        /// </summary>
        /// <returns>ErrorController instance <see cref="Controller.ErrorController"/></returns>
        public IErrorController GetErrorController()
        {
            return new ErrorController(new ErrorViewFactory());
        }

        /// <summary>
        /// Returns the configured HelpController
        /// </summary>
        /// <returns>HelpController instance <see cref="Controller.HelpController"/></returns>
        public IHelpController GetHelpController()
        {
            return new HelpController(_commands, _session, new HelpViewFactory());
        }

        /// <summary>
        /// Returns the configured CategoryController
        /// </summary>
        /// <returns>CategoryController instance <see cref="Controller.CategoryController"/></returns>
        public ICategoryController GetCategoryController()
        {
            return new CategoryController(new CategoryRepo(_shopContext), new CategoryViewFactory());
        }
    }
}
