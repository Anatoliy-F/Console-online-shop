using ClassLibrary1.Tests.ControllerTests;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos.Interfaces;
using ConsoleShop.Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ConsoleShop.Tests.ControllerTests
{
    public class CategoryControllerTests : BaseTest
    {
        [Fact]
        public void Return_All_Categories()
        {
            //Arrange
            var mockFactory = GetResultFactory();
            var mockFactory1 = GetResultFactory();

            Mock<ICategoryRepo> repoMock = new Mock<ICategoryRepo>();
            repoMock.Setup(m => m.GetAll()).Returns(new Category[] { 
                    new Category{Id = 1, Name = "Cat1"},
                    new Category{Id = 2, Name = "Cat2"},
                    new Category{Id = 3, Name = "Cat3"},
                    new Category{Id = 4, Name = "Tom"},
                    new Category{Id = 5, Name = "Jerry"},
                } 
            );

            Mock<ICategoryRepo> emptyRepo = new Mock<ICategoryRepo>();
            emptyRepo.Setup(m => m.GetAll()).Returns(new Category[] { });

            CategoryController cc = new CategoryController(repoMock.Object, mockFactory.Object);
            CategoryController emptyCc = new CategoryController(emptyRepo.Object, mockFactory1.Object);

            //Act
            IActionResult result1 = cc.ShowAll();
            IActionResult result2 = emptyCc.ShowAll();

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Equal(5, result1.ResultBody.Count());
            Assert.Null(result2.ResultBody);
        }

        [Fact]
        public void Show_By_Id()
        {
            //Arrange
            var mockFactory = GetResultFactory();
            var mockFactory1 = GetResultFactory();
            Mock<ICategoryRepo> repoMock = new Mock<ICategoryRepo>();
            repoMock.Setup(m => m.Find(It.Is<int>(i => i == 5))).Returns(
                    new Category{Id = 5, Name = "Jerry"}
            );

            //Act
            IActionResult result1 = new CategoryController(repoMock.Object, mockFactory.Object).ShowById(5);
            IActionResult result2 = new CategoryController(repoMock.Object, mockFactory1.Object).ShowById(2);

            //Assert
            Assert.Equal(ActionResult.Succes, result1.Result);
            Assert.Equal(ActionResult.NotFound, result2.Result);
            Assert.Equal("Jerry", ((Category)result1.ResultBody.First()).Name);
            Assert.Single(result1.ResultBody);
            Assert.Null(result2.ResultBody);

        }

        [Fact]
        public void Can_Add_Category()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();
            var mockFactory3 = GetResultFactory();
            Mock<ICategoryRepo> repoMock = new Mock<ICategoryRepo>(MockBehavior.Default);
            repoMock.Setup(m => m.Add(It.Is<Category>(c => c.Name != "" && c.Id == 0))).Returns(3);
            repoMock.Setup(m => m.Add(It.Is<Category>(c => c.Name == "same"))).Throws(new CustomRepoException("same"));
            repoMock.Setup(m => m.Find(It.Is<int>(i => i == 3))).Returns(new Category { Id = 3});

            //Act
            IActionResult result1 = new CategoryController(repoMock.Object, mockFactory1.Object).Add("");
            IActionResult result2 = new CategoryController(repoMock.Object, mockFactory2.Object).Add("same");
            IActionResult result3 = new CategoryController(repoMock.Object, mockFactory3.Object).Add("new");

            //Assert
            Assert.Equal(ActionResult.Warning, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Equal(ActionResult.Error, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Succes, result3.Result);
            Assert.NotEmpty(result3.ResultBody);

        }

        [Fact]
        public void Can_Update_Category()
        {
            //Arrange
            var mockFactory1 = GetResultFactory();
            var mockFactory2 = GetResultFactory();
            var mockFactory3 = GetResultFactory();
            Mock<ICategoryRepo> repoMock = new Mock<ICategoryRepo>(MockBehavior.Default);
            repoMock.Setup(m => m.Update(It.Is<int>(i => i == 3), It.IsAny<String>())).Returns(3);
            repoMock.Setup(m => m.Update(It.Is<int>(i => i != 3), It.IsAny<String>())).Throws(new NotFoundException("ex"));
            repoMock.Setup(m => m.Find(It.Is<int>(i => i == 3))).Returns(new Category { Id = 3, Name = "updated" });

            //Act
            IActionResult result1 = new CategoryController(repoMock.Object, mockFactory1.Object).Update(10, "");
            IActionResult result2 = new CategoryController(repoMock.Object, mockFactory2.Object).Update(11, "same");
            IActionResult result3 = new CategoryController(repoMock.Object, mockFactory3.Object).Update(3, "updated");

            //Assert
            Assert.Equal(ActionResult.Warning, result1.Result);
            Assert.Null(result1.ResultBody);
            Assert.Equal(ActionResult.Error, result2.Result);
            Assert.Null(result2.ResultBody);
            Assert.Equal(ActionResult.Succes, result3.Result);
            Assert.NotEmpty(result3.ResultBody);
        }
    }
}
