using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System;
using System.Linq;

namespace ConsoleShop.Controller
{
    /// <inheritdoc/>
    public class CartController : ICartController
    {
        /// <summary>
        /// An object that provides operations with Product entities
        /// </summary>
        private readonly IProductRepo _productRepo;

        /// <summary>
        /// An object that provides operations with Orders entities
        /// </summary>
        private readonly IOrderRepo _orderRepo;

        /// <summary>
        /// An object that represents current session
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// Initialize new instance of CartController
        /// </summary>
        /// <param name="productRepo">An object that implements possible operations with Product objects</param>
        /// <param name="orderRepo">An object that implements possible operations with Order objects</param>
        /// <param name="session">An object that provide session functions</param>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        public CartController (IProductRepo productRepo, IOrderRepo orderRepo, ISession session, IActionResultFactory actionResultFactory)
        {
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _session = session;
            _actionResultFactory = actionResultFactory;
        }

        /// <inheritdoc/>
        public IActionResult AddToCart(int productId, int quantity = 1)
        {   
            if(quantity <= 0)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning, "Quantity should be positive number");
            }
            var product = _productRepo.Find(productId);
            if( product != null)
            {
                _session.Cart.AddItem(product, quantity);
                return _actionResultFactory.GetResultRender(ActionResult.Succes, $"Thanks you dear {_session.User.Name}. You select {product.Name} x {quantity}");
            }
            else
            {
               return _actionResultFactory.GetResultRender(ActionResult.Warning, $"Please check productId, our shop didn't containt good with Id = {productId}");
            }
        }

        /// <inheritdoc/>
        public IActionResult ShowCart()
        {
            var lines = _session.Cart.Lines;
            if (lines.Any())
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Contents of your shopping cart", lines);
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    "Your cart is empty, but you always can Add something :)");
            }
        }

        /// <inheritdoc/>
        public IActionResult MakeOrder(string name, string address)
        {   
            if(address == String.Empty)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                    "Please specify address for delivery");
            }
            var lines = _session.Cart.Lines;
            if (lines.Any())
            {
                Order newOrder = new Order
                {
                    UserId = _session.User.Id,
                    Name = name,
                    Address = address,
                    Lines = lines.ToList()
                };

                int id = _orderRepo.Add(newOrder);

               _session.Cart.Clear();

                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    $"Your order #{id} is accepted, view status in order history!");
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    "Your cart is empty, but you always can Add something :)");
            }
        }

        /// <inheritdoc/>
        public IActionResult ClearCart()
        {
            if (_session.Cart.Lines.Any())
            {
                _session.Cart.Clear();
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Your Cart is Clear Now");
            }
            return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                "Your cart is already empty");
        }
    }
}
