using ClassLibrary1.Tests.ControllerTests;
using ConsoleShop.Controller;
using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ConsoleShop.Tests.ControllerTests
{
    public class ErrorControllerTests : BaseTest
    {
        [Fact]
        public void Return_ErrorStatus_And_Message()
        {
            //Arrange
            Mock<IActionResultFactory> mockFactory = new Mock<IActionResultFactory>();
            Mock<IActionResult> mockResult = new Mock<IActionResult>();

            mockFactory.Setup(m => m.GetResultRender(It.Is<ActionResult>(val => val == ActionResult.Error),
                It.IsAny<string>(), It.IsAny<IEnumerable<IEntity>>())).Returns(
                    (ActionResult en, string str, IEnumerable<IEntity> body) => {
                        mockResult.Setup(m => m.Result).Returns(en);
                        mockResult.Setup(m => m.Message).Returns(str);
                        mockResult.Setup(m => m.ResultBody).Returns(body);
                        return mockResult.Object;
                    }
                );

            ErrorController ec = new ErrorController(mockFactory.Object);

            //Act
            IActionResult result = ec.ShowError("test");

            //Assert
            Assert.Equal(ActionResult.Error, result.Result);
            Assert.Equal("test", result.Message);
            Assert.Null(result.ResultBody);
        }
    }
}
