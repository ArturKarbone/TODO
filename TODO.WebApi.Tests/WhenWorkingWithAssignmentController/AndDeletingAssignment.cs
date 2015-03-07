using System;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.WebApi.Models.Assignments;

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
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel
            {
                Done = false,
                DueDate = DateTime.Today,
                Name = "Some simple task for today"
            });
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Delete(1);
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
