using System.Web.Http.Results;
using NUnit.Framework;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndDeletingAssignment
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
            var result = AssignmentControllerTestContext.AssignmentController.Delete(2);
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void AndAssignmentDoesNotExist_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Delete(66);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public void AndIdIsInvalid_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Delete(-23);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }
    }
}
