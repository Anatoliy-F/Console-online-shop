using ConsoleShop.Controller.Base;
using ConsoleShop.Controller.Interfaces;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using System;
using System.Linq;

namespace ConsoleShop.Controller
{
    /// <inheritdoc/>
    public class ProductController : IProductController
    {
        /// <summary>
        /// An object that implements possible operations with Product objects
        /// </summary>
        private readonly IProductRepo _repo;

        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// Initialize new instance of ProductController
        /// </summary>
        /// <param name="repo">An object that implements possible operations with Product objects</param>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        public ProductController(IProductRepo repo, IActionResultFactory actionResultFactory)
        {
            _repo = repo;
            _actionResultFactory = actionResultFactory;
        }

        /// <inheritdoc/>
        public IActionResult ShowAll()
        {
            var products = _repo.GetAll();
            if (products.Any())
            {
                return _actionResultFactory.GetResultRender(
                ActionResult.Succes,
                "Product List", products);
            }
            else
            {
                return _actionResultFactory.GetResultRender(
                ActionResult.NotFound,
                "No products in out store");
            }

        }

        /// <inheritdoc/>
        public IActionResult ShowById(int id)
        {
            var product = _repo.Find(id);
            if (product != null)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes, "Product",
                    new[] { product });
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound, "Product");
            }
        }

        /// <inheritdoc/>
        public IActionResult ShowByName(string name)
        {
            var products = _repo.FindByName(name);
            if (products.Any())
            {
                return _actionResultFactory.GetResultRender(
                    ActionResult.Succes,
                    "Searching result",
                    products);
            }
            else
            {
                return _actionResultFactory.GetResultRender(
                    ActionResult.NotFound,
                    "Sorry, we didn't found this product"
                    );
            }
        }

        /// <inheritdoc/>
        public IActionResult AddNew(Product product)
        {

            if (product.Name == String.Empty)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                    "Product must have name");
            }

            if (product.Description == String.Empty)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                   "Product must have description");
            }

            if (product.Price <= 0m)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                   "Product must have greater than zero price");
            }
            try
            {
                int id = _repo.Add(product);
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "New product added to database",
                    new[] { _repo.Find(id) }
                    );
            }
            catch (CustomRepoException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Error,
                    ex.Message);
            }
            catch (NotFoundException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    ex.Message);
            }
        }

        /// <inheritdoc/>
        public IActionResult UpdateProduct(int id, Product product)
        {
            try
            {
                _repo.Update(id, product);
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    $"Product #{id} updated",
                    new[] { _repo.Find(id) });
            }
            catch (CustomRepoException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Error,
                    ex.Message);
            }
            catch (NotFoundException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                    ex.Message);
            }
        }
    }
}
