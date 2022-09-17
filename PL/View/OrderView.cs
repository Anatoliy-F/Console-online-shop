using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View
{
    public class OrderView : BaseView
    {
        private readonly IEnumerable<Order> _orders;

        /// <summary>
        /// Initialize new instance of OrderView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public OrderView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody)
        {
            _orders = (IEnumerable<Order>)resultBody;
        }

        /// <summary>
        /// Displays collections of requested entities
        /// </summary>
        protected override void RenderBody()
        {
            var orders = _orders.ToList();
            var column1 = this.MakeColumn(orders.Select(o => "Id: " + o.Id.ToString() + "  "));
            var column2 = this.MakeColumn(orders.Select(o => $"[{o.Status}]"));
            var column3 = this.MakeColumn(orders.Select(o => $" [{o.Name}] "));
            var column4 = this.MakeColumn(orders.Select(o => "Address: " + o.Address));

            for (int i = 0; i < column1.Count; i++)
            {
                Console.Write(column1[i]);
                this.PrintColorizedMessageNoBreak(column2[i], ConsoleColor.Cyan);
                this.PrintColorizedMessageNoBreak(column3[i], ConsoleColor.Yellow);
                Console.WriteLine(column4[i]);

                if (orders[i].Lines != null && orders[i].Lines.Any())
                {
                    var inCol1 = this.MakeColumn(orders[i].Lines.Select(l => l.Product.Name));
                    var inCol2 = this.MakeColumn(orders[i].Lines.Select(l => l.Quantity.ToString()));

                    for (int j = 0; j < inCol1.Count; j++)
                    {
                        Console.Write($"\t-> {inCol1[j]} ");
                        Console.WriteLine(inCol2[j]);
                    }
                }

                if (i < column1.Count - 1) { this.PrintBorderLine('_'); }
            }
        }
    }
}
