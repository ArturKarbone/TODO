using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndFindingAssignmentById
    {
        public AssignmentControllerTestContext AssignmentControllerTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            AssignmentControllerTestContext = new AssignmentControllerTestContext();
        }

        [Test]
        public void AndAssignmentExist_OkResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindById(2);
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<Assignment>>(result);
        }

        [Test]
        public void AndAssignmentDoesNotExist_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindById(66);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public void AndIdIsInvalid_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindById(-652);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

    }
}
