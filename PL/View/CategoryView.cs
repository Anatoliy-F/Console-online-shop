using ConsoleShop.Controller.Base;
using ConsoleShop.Model;
using ConsoleShop.Model.BaseEntity;
using ConsoleShop.View.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleShop.View
{
    /// <summary>
    /// Implement IActionResult for all operations with categories in shop <see cref="ConsoleShop.Model.Category"/>
    /// </summary>
    public class CategoryView : BaseView
    {
        private readonly IEnumerable<Category> _categories;

        /// <summary>
        /// Initialize new instance of CategoryView response object.
        /// </summary>
        /// <param name="result">Response result</param>
        /// <param name="message">Response messag</param>
        /// <param name="resultBody">Collection of requested entities</param>
        public CategoryView(ActionResult result, string message, IEnumerable<IEntity> resultBody = null) : base(result, message, resultBody)
        {
            _categories = (IEnumerable<Category>)resultBody;
        }

        /// <summary>
        /// Displays collections of requested entities
        /// </summary>
        protected override void RenderBody()
        {
            var column1 = this.MakeColumn(_categories.Select(c => c.Id.ToString()));
            var column2 = this.MakeColumn(_categories.Select(c => c.Name));

            for (int i = 0; i < column1.Count; i++)
            {
                this.PrintColorizedDefinition(column1[i], column2[i], ConsoleColor.Magenta);
            }
        }
    }
}
