using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View
{
    public class ProductView : BaseView
    {
        private readonly IEnumerable<Product> _products;

        /// <summary>
        /// Initialize new instance of ProductView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public ProductView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody)
        {
            _products = (IEnumerable<Product>)resultBody;
        }

        /// <summary>
        /// Displays collections of requested entities
        /// </summary>
        protected override void RenderBody()
        {
            var column1 = this.MakeColumn(_products.Select(p => $"=> Id: #{p.Id}  "));
            var column2 = this.MakeColumn(_products.Select(p => $"[{p.Name}] "));
            var column3 = this.MakeColumn(_products.Select(p => p.CategoryNav.Name));
            var column4 = this.MakeColumn(_products.Select(p => p.Description));
            var column5 = this.MakeColumn(_products.Select(p => $"${p.Price}"));

            for (int i = 0; i < column1.Count; i++)
            {
                Console.Write(column1[i]);
                this.PrintColorizedMessageNoBreak(column2[i], ConsoleColor.Cyan);
                this.PrintColorizedMessage(column3[i], ConsoleColor.Yellow);
                Console.WriteLine($"\t{column4[i]}");
                this.PrintColorizedMessage($"\t{column5[i]}", ConsoleColor.Yellow);

                if (i < column1.Count - 1) { this.PrintBorderLine('_'); }
            }
        }
    }
}
