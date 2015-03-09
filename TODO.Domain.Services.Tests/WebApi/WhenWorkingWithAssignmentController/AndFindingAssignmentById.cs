using System;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.Tests.WebApi.WhenWorkingWithAssignmentController
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
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today, Name = "Task for today"});
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindById(1);
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
