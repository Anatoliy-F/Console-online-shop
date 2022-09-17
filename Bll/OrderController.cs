using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.Controller
{
    /// <inheritdoc/>
    public class OrderController : IOrderController
    {
        /// <summary>
        /// An object that implements possible operations with Order objects
        /// </summary>
        private readonly IOrderRepo _orderRepo;

        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// An object that provide session functions
        /// </summary>
        private readonly ISession _session;

        /// <summary>
        /// Initialize new instance of OrderController
        /// </summary>
        /// <param name="orderRepo">An object that implements possible operations with Order objects</param>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        /// <param name="session">An object that provide session functions</param>
        public OrderController(IOrderRepo orderRepo, IActionResultFactory actionResultFactory, ISession session)
        {
            _orderRepo = orderRepo;
            _actionResultFactory = actionResultFactory;
            _session = session;
        }

        /// <inheritdoc/>
        public IActionResult ShowOrders()
        {
            var orders = _orderRepo.FindByUser(_session.User.Id);
            if (orders.Any())
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Your order history",
                    orders);
            } else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    "You dont have any orders yet");
            }
        }

        /// <inheritdoc/>
        public IActionResult ShowAllOrders()
        {
            var orders = _orderRepo.GetAll();
            if (orders.Any())
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "List of all orders",
                    orders);
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound, "no orders in shop yet");
            }
        }

        /// <inheritdoc/>
        public IActionResult ConfirmReceipt(int orderId)
        {
            var order = _orderRepo.Find(orderId);

            if(order != null && order.UserId == _session.User.Id)
            {
                try
                {
                    _orderRepo.SetStatus(order.Id, OrderStatus.Received);

                    return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Thanks you!",
                    new[] { order });
                }
                catch (NotFoundException ex)
                {
                    return _actionResultFactory.GetResultRender(ActionResult.Error,
                        ex.Message);
                }
                
            }
            return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                $"You didn't have order with id: {orderId}");
        }

        /// <inheritdoc/>
        public IActionResult CancellOrder(int orderId)
        {
            var order = _orderRepo.Find(orderId);

            if (order != null && order.UserId == _session.User.Id)
            {
                if(order.Status == OrderStatus.Received)
                {
                    return _actionResultFactory.GetResultRender(ActionResult.Warning,
                    "You can't cancel received order");
                }

                try
                {
                    _orderRepo.SetStatus(order.Id, OrderStatus.CancelledByUser);

                    return _actionResultFactory.GetResultRender(ActionResult.Succes,
                            "Order cancelled",
                            new[] { order });
                }
                catch (NotFoundException ex)
                {
                    return _actionResultFactory.GetResultRender(ActionResult.Error,
                        ex.Message);
                }
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
               $"You didn't have order with id: {orderId}");
            }
        }

        /// <inheritdoc/>
        public IActionResult ChangeOrderStatus(int orderId, OrderStatus status)
        {   
            if(status != OrderStatus.CancelledByUser && status != OrderStatus.New)
            {
                try
                {
                    _orderRepo.SetStatus(orderId, status);
                    return _actionResultFactory.GetResultRender(ActionResult.Succes,
                        "Status changed", new[] { _orderRepo.Find(orderId) }
                        /*(IEnumerable<Order>)_orderRepo.Find(orderId)*/);
                }
                catch (NotFoundException ex)
                {
                    return _actionResultFactory.GetResultRender(ActionResult.Error,
                        ex.Message);
                }
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                        $"You can't set this status: {status}");
            }
        }
    }
}
