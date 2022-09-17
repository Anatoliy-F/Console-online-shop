using ConsoleShop.Dal.Exception;
using ConsoleShop.Model;
using System;

namespace ConsoleShop.Commands.ViewModels
{
    /// <summary>
    /// Implements order status selection
    /// </summary>
    public static class OrderStatusViewModel
    {
        /// <summary>
        /// Select order status via console UI
        /// </summary>
        /// <returns>OrderStatus</returns>
        /// <exception cref="NotFoundException">Thrwon when user choose unexisting order status</exception>
        public static OrderStatus SelectOrderStatus()
        {
            Console.WriteLine("Choose status number, and type it");
            Console.WriteLine("Cancelled by administrator: 1");
            Console.WriteLine("PaymentRecieved: 2");
            Console.WriteLine("Sent: 3");
            Console.WriteLine("Received: 4");
            Console.WriteLine("Completed: 5");
            string statusString = Console.ReadLine();
            var orderStatus = statusString switch
            {
                "1" => OrderStatus.CancelledByAdministrator,
                "2" => OrderStatus.PaymentRecieved,
                "3" => OrderStatus.Sent,
                "4" => OrderStatus.Received,
                "5" => OrderStatus.Completed,
                _ => throw new NotFoundException("No such status in orderStatusEnum"),
            };
            return orderStatus;
        } 
    }
}
