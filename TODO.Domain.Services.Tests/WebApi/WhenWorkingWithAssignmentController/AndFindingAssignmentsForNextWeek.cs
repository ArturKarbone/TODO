using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.Tests.WebApi.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndFindingAssignmentsForNextWeek
    {
        public AssignmentControllerTestContext AssignmentControllerTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            AssignmentControllerTestContext = new AssignmentControllerTestContext();
        }

        [Test]
        public void AndThereAreNoAssignmentsAtAll_NotFoundResultShouldBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindForNextWeek();
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AndThereAreAssignmentsAndNoAssignmentsForNextWeek_NotFoundResultShouldBeReturned()
        {
            // Arrange
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(9), Name = "Cool stuff" });
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Cool stuff" });
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindForNextWeek();
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AndThereAreAssignmentsAndAssignmentsForNextWeek_OkNegotiatedContentResultShouldBeReturned()
        {
            // Arrange
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Cool stuff NW" });
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(4), Name = "Cool stuff NW" });
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(9), Name = "Cool stuff" });
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindForNextWeek();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
