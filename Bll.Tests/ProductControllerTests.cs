using ClassLibrary1.Tests.ControllerTests;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using Moq;
using System.Linq;
using Xunit;

namespace ConsoleShop.Tests.ControllerTests
{
    public class ProductControllerTests : BaseTest
    {
        [Fact]
        public void Return_All_Products()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();

            Mock<IProductRepo> mockRepo1 = new Mock<IProductRepo>();
            mockRepo1.Setup(m => m.GetAll()).Returns(new[] { 
                new Product{Id = 1, Name = "Name1"},
                new Product{Id = 2, Name = "Name2"},
            });

            Mock<IProductRepo> emptyRepo = new Mock<IProductRepo>();
            emptyRepo.Setup(m => m.GetAll()).Returns(new Product[] {});

            //Act
            IActionResult result1 = new ProductController(mockRepo1.Object, mockFactory1.Object).ShowAll();
            IActionResult result2 = new ProductController(emptyRepo.Object, mockFactory2.Object).ShowAll();

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Equal(2, result1.ResultBody.Count());
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Show_By_Id()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();

            Mock<IProductRepo> repoMock = new Mock<IProductRepo>();
            repoMock.Setup(m => m.Find(It.Is<int>(i => i == 3))).Returns(new Product { Id = 3, Name = "p1" });

            //Act
            IActionResult result1 = new ProductController(repoMock.Object, mockFactory1.Object).ShowById(3);
            IActionResult result2 = new ProductController(repoMock.Object, mockFactory2.Object).ShowById(10);

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Equal("p1", ((Product)result1.ResultBody.First()).Name);
            Assert.Single(result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Can_Show_By_Name()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();

            var prod1 = new Product { Id = 4, Name = "Tom" };
            var prod2 = new Product { Id = 7, Name = "Tommy" };

            Mock<IProductRepo> repoMock = new Mock<IProductRepo>();
            repoMock.Setup(m => m.FindByName(It.Is<string>(s => s == "to"))).Returns(
                new Product[]{ prod1, prod2});

            //Act
            IActionResult result1 = new ProductController(repoMock.Object, mockFactory1.Object).ShowByName("to");
            IActionResult result2 = new ProductController(repoMock.Object, mockFactory2.Object).ShowByName("any");

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Equal(2, result1.ResultBody.Count());
            Assert.Contains(prod1, result1.ResultBody);
            Assert.Contains(prod2, result1.ResultBody);
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Can_Add_New()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();
            var mockFactory3 = GetResultFactory();
            var mockFactory4 = GetResultFactory();
            var mockFactory5 = GetResultFactory();
            var mockFactory6 = GetResultFactory();

            Mock<IProductRepo> repoMock = new Mock<IProductRepo>();
            repoMock.Setup(m => m.Add(It.Is<Product>(p => p.Id == 0))).Returns(3);
            repoMock.Setup(m => m.Add(It.Is<Product>(p => p.CategoryId == 100))).Throws(new NotFoundException("ex"));
            repoMock.Setup(m => m.Add(It.Is<Product>(p => p.CategoryId == 0))).Throws(new CustomRepoException("ex"));
            repoMock.Setup(m => m.Find(It.Is<int>(i => i == 3))).Returns(new Product { Id = 3 });

            //Act
            IActionResult result1 = new ProductController(repoMock.Object, mockFactory1.Object).
                AddNew(new Product { Description = "desc", Price = 10m, CategoryId = 3 });
            IActionResult result2 = new ProductController(repoMock.Object, mockFactory2.Object).
                AddNew(new Product { Name = "name", Price = 10m, CategoryId = 3 });
            IActionResult result3 = new ProductController(repoMock.Object, mockFactory3.Object).
                AddNew(new Product { Name = "name", Description = "desc", CategoryId = 3 });
            IActionResult result4 = new ProductController(repoMock.Object, mockFactory4.Object).
                AddNew(new Product { Name = "name", Description = "desc", Price = 10m });
            IActionResult result5 = new ProductController(repoMock.Object, mockFactory5.Object).
                AddNew(new Product { Name = "name", Description = "desc", Price = 10m, CategoryId = 100 });
            IActionResult result6 = new ProductController(repoMock.Object, mockFactory6.Object).
                AddNew(new Product { Name = "name", Description = "desc", Price = 10m, CategoryId = 3 });

            //Assert
            Assert.Equal(ActionResult.Warning, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Equal(ActionResult.Warning, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Warning, result3.Result);
            Assert.Null(result3.ResultBody);
            Assert.Equal(ActionResult.Error, result4.Result);
            Assert.Null(result4.ResultBody);
            Assert.Equal(ActionResult.NotFound, result5.Result);
            Assert.Null(result5.ResultBody);
            Assert.Equal(ActionResult.Succes, result6.Result);
            Assert.NotEmpty(result6.ResultBody);
        }

        [Fact]
        public void Can_Update_Product()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();
            var mockFactory3 = GetResultFactory();

            Mock<IProductRepo> repoMock = new Mock<IProductRepo>();
            repoMock.Setup(m => m.Update(It.IsAny<int>(), It.Is<Product>(p => p.CategoryId == 100)))
                .Throws(new CustomRepoException("ex"));
            repoMock.Setup(m => m.Update(It.Is<int>(i => i == 100), It.IsAny<Product>()))
                .Throws(new NotFoundException("ex"));
            repoMock.Setup(m => m.Update(It.Is<int>(i => i == 3), It.IsAny<Product>())).Returns(3);
            repoMock.Setup(m => m.Find(It.Is<int>(i => i == 3))).Returns(new Product { Id = 3 });

            //Act
            IActionResult result1 = new ProductController(repoMock.Object, mockFactory1.Object)
                .UpdateProduct(10, new Product { CategoryId = 100 });
            IActionResult result2 = new ProductController(repoMock.Object, mockFactory2.Object)
                .UpdateProduct(100, new Product { Description = "desc"});
            IActionResult result3 = new ProductController(repoMock.Object, mockFactory3.Object)
                .UpdateProduct(3, new Product { Name = "new"});

            //Assert
            Assert.Equal(ActionResult.Error, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Succes, result3.Result);
            Assert.NotEmpty(result3.ResultBody);
        }
    }
}
