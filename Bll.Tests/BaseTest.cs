using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using Moq;
using System.Collections.Generic;

namespace ClassLibrary1.Tests.ControllerTests
{
    public abstract class BaseTest
    {
        protected Mock<IActionResultFactory> GetResultFactory()
        {
            Mock<IActionResultFactory> mockFactory = new Mock<IActionResultFactory>();
            Mock<IActionResult> mockResult = new Mock<IActionResult>();

            mockFactory.Setup(m => m.GetResultRender(It.IsAny<ActionResult>(),
                It.IsAny<string>(), It.IsAny<IEnumerable<IEntity>>())).Returns(
                    (ActionResult en, string str, IEnumerable<IEntity> body) => {
                        mockResult.Setup(m => m.Result).Returns(en);
                        mockResult.Setup(m => m.Message).Returns(str);
                        mockResult.Setup(m => m.ResultBody).Returns(body);
                        return mockResult.Object;
                    }
                );

            return mockFactory;
        }
    }
}
