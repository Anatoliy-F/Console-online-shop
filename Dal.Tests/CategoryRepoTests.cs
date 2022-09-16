using ConsoleShop.Dal;
using ConsoleShop.Dal.Exception;
using ConsoleShop.Dal.Repos;
using ConsoleShop.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleShop.Tests.Dal
{
    public class CategoryRepoTests
    {
        [Fact]
        public void Can_Find_By_Id()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category>
            {
                new Category {Id = 1, Name = "cat1"},
                new Category {Id = 2, Name = "cat2"},
                new Category {Id = 3, Name = "cat3"},
                new Category {Id = 4, Name = "cat4"},
            });

            CategoryRepo cr = new CategoryRepo(mock.Object);

            //Act
            Category c1 = cr.Find(1);
            Category c2 = cr.Find(1);
            Category c3 = cr.Find(3);
            Category c4 = cr.Find(4);
            Category c5 = cr.Find(5);

            //Assert
            Assert.Null(c5);
            Assert.Equal(c1, c2);
            Assert.Equal(3, c3.Id);
            Assert.Same(mock.Object.Categories.First(c => c.Id == 4), c4);
            Assert.Same(mock.Object.Categories[3], c4);
        }

        [Fact]
        public void Return_All_Catefories()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category>
            {
                new Category {Id = 1, Name = "cat1"},
                new Category {Id = 2, Name = "cat2"},
                new Category {Id = 3, Name = "cat3"},
                new Category {Id = 4, Name = "cat4"},
            });

            CategoryRepo cr = new CategoryRepo(mock.Object);

            //Act
            Category[] categories = cr.GetAll().ToArray();

            //Assert
            Assert.True(categories.Length == 4);
            Assert.Equal("cat1", categories[0].Name);
            Assert.Equal(2, categories[1].Id);
            Assert.Same(mock.Object.Categories[2], categories[2]);
            Assert.Same(mock.Object.Categories.First(c => c.Id == 4), categories[3]);
        }

        [Fact]
        public void Can_Update_Categories()
        {   
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category>
            {
                new Category {Id = 1, Name = "cat1"},
                new Category {Id = 2, Name = "cat2"},
                new Category {Id = 3, Name = "cat3"},
                new Category {Id = 4, Name = "cat4"},
            });

            CategoryRepo cr = new CategoryRepo(mock.Object);

            //Act
            cr.Update(1, "Tom");
            cr.Update(3, "Jerry");

            //Assert
            Assert.Equal("Tom", mock.Object.Categories[0].Name);
            Assert.Equal("Jerry", mock.Object.Categories[2].Name);
        }

        [Fact]
        public void Can_Add_Category()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category>
            {
                new Category {Id = 1, Name = "cat1"},
                new Category {Id = 2, Name = "cat2"},
            });

            CategoryRepo cr = new CategoryRepo(mock.Object);

            Category c1 = new Category { Name = "Tom" };
            Category c2 = new Category { Name = "Jerry" };
            
            //Act
            cr.Add(c1);
            cr.Add(c2);

            //Assert
            Assert.True(mock.Object.Categories.Count == 4);
            Assert.Equal(3, c1.Id);
            Assert.Equal(c2, mock.Object.Categories[3]);
        }

        [Fact]
        public void Add_Same_Category_Throw_Exception()
        {
            //Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category>
            {
                new Category {Id = 1, Name = "cat1"},
                new Category {Id = 2, Name = "cat2"},
            });

            CategoryRepo cr = new CategoryRepo(mock.Object);
            
            Category c1 = new Category { Name = "cat1" };

            //Assert
            Assert.Throws<CustomRepoException>(() => cr.Add(c1));
        }

        [Fact]
        public void Update_Not_Exist_Category_Throw_Exception()
        {
            // Arrange
            Mock<IShopContext> mock = new Mock<IShopContext>();
            mock.Setup(m => m.Categories).Returns(new List<Category>
            {
                new Category {Id = 1, Name = "cat1"},
                new Category {Id = 2, Name = "cat2"},
            });

            CategoryRepo cr = new CategoryRepo(mock.Object);

            //Assert
            Assert.Throws<NotFoundException>(() => { cr.Update(10, "Tom"); });
        }
    }
}
