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
    public class CategoryController : ICategoryController
    {
        /// <summary>
        /// An object that implements possible operations with Category objects
        /// </summary>
        private readonly ICategoryRepo _repo;

        /// <summary>
        /// Object that instantiate IActionResult object
        /// </summary>
        private readonly IActionResultFactory _actionResultFactory;

        /// <summary>
        /// Initialize new instance of CategoryController
        /// </summary>
        /// <param name="repo">An object that implements possible operations with Category objects</param>
        /// <param name="actionResultFactory">Object that instantiate IActionResult object</param>
        public CategoryController(ICategoryRepo repo, IActionResultFactory actionResultFactory)
        {
            _repo = repo;
            _actionResultFactory = actionResultFactory;
        }

        /// <inheritdoc/>
        public IActionResult ShowAll()
        {
            var result = _repo.GetAll();
            if (result.Any())
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                "Categories list",
                result);
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                "Categories list");
            }
            
        }

        /// <inheritdoc/>
        public IActionResult ShowById(int id)
        {
            var result = _repo.Find(id);
            if(result != null)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                "Category",
                new[] { result });
            }
            else
            {
                return _actionResultFactory.GetResultRender(ActionResult.NotFound,
                "Category");
            }
            
        }

        /// <inheritdoc/>
        public IActionResult Add(string name)
        {
            if(name == String.Empty)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                    "Category name shouldn't be empty");
            }
            try
            {
                int id = _repo.Add(new Category { Name = name });
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Category add",
                    new[] { _repo.Find(id) });
            }
            catch (CustomRepoException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Error,
                    ex.Message);
            }
        }

        /// <inheritdoc/>
        public IActionResult Update(int id, string name)
        {
            if (name == String.Empty)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Warning,
                    "Category name shouldn't be empty");
            }
            try
            {
                int catId = _repo.Update(id, name);
                return _actionResultFactory.GetResultRender(ActionResult.Succes,
                    "Category add",
                    new[] { _repo.Find(catId) });
            }
            catch (NotFoundException ex)
            {
                return _actionResultFactory.GetResultRender(ActionResult.Error,
                    ex.Message);
            }
        }
    }
}
