using ConsoleShop.Controller.Base;
using ConsoleShop.Model.BaseEntity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Tests.Helper
{
    public abstract class BaseTest
    {
        protected Mock<IActionResultFactory> getResultFactory()
        {
            Mock<IActionResultFactory> mockFactory = new Mock<IActionResultFactory>();
            Mock<IActionResult> mockResult = new Mock<IActionResult>();

            mockFactory.Setup(m => m.GetResultRender(It.IsAny<ActionResult>(),
                It.IsAny<string>(), It.IsAny<IEnumerable<IEntity>>())).Returns(
                    (ActionResult en, string str, IEnumerable<IEntity> body) =>
                    {
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
