using ConsoleShop.Model;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View
{
    /// <summary>
    /// Implement IActionResult for all operations with shopping cart <see cref="ConsoleShop.Bll.Model.ICart"/>
    /// </summary>
    public class CartView : BaseView
    {
        private readonly IEnumerable<CartLine> _cartLines;

        /// <summary>
        /// Initialize new instance of CartView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public CartView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody)
        {
            _cartLines = (IEnumerable<CartLine>)resultBody;
        }

        /// <summary>
        /// Displays collections of requested entities
        /// </summary>
        protected override void RenderBody()
        {
            var column1 = this.MakeColumn(_cartLines.Select(c => c.Product.Name));
            var column2 = this.MakeColumn(_cartLines.Select(c => c.Quantity.ToString() + " pcs"));

            for(int i = 0; i < column1.Count; i++)
            {
                this.PrintColorizedDefinition(column1[i], column2[i], ConsoleColor.Magenta);
                this.PrintBorderLine('-');
            }

            var toPay = _cartLines.Sum(l => l.Product.Price * l.Quantity);
            Console.Write($"\tTotal to pay: ");
            this.PrintColorizedMessage($"[${toPay}]", ConsoleColor.Yellow);
        }
    }
}
