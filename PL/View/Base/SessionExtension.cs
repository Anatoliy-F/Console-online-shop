using ConsoleShop.Controller.Base;
using System.Linq;

namespace ConsoleApp1.View.Base
{
    /// <summary>
    /// Creates extension methods for the session object
    /// </summary>
    public static class SessionExtension
    {
        /// <summary>
        /// Check is user logged in
        /// </summary>
        /// <param name="session">session object</param>
        /// <returns>true, if user logged in</returns>
        public static bool IsLoggedIn(this ISession session)
        {
            return session.User.Name != string.Empty;
        }

        /// <summary>
        /// Return name of logged in user or default name value
        /// </summary>
        /// <param name="session">session object</param>
        /// <returns>User.Name or "Guest" for unauthorized users</returns>
        public static string GetUserName(this ISession session)
        {
            return session.User.Name != string.Empty ? session.User.Name : "Guest";
        }

        /// <summary>
        /// Count item is shopping cart
        /// </summary>
        /// <param name="session">session object</param>
        /// <returns>Number of items in the cart</returns>
        public static int GetCartLinesCount(this ISession session)
        {
            return session.Cart.Lines.Count;
        }

        /// <summary>
        /// Counts the number of orders made by the user
        /// </summary>
        /// <param name="session">session object</param>
        /// <returns>Number of user's orders</returns>
        public static int GetOrdersCount(this ISession session)
        {
            return session.User?.Orders?.Count ?? 0;
        }

        /// <summary>
        /// Calculates the amount of money spent on purchases
        /// </summary>
        /// <param name="session">session object</param>
        /// <returns>The amount of money spent by the user</returns>
        public static decimal GetUserTotal(this ISession session)
        {
            return session?.User?.Orders?.Sum(o => o.Lines.Sum(l => l.Quantity * l.Product.Price)) ?? 0m;
        }
    }
}
