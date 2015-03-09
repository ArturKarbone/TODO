using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.Domain.Services.Tests.WebApi.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndFindingAssignmentsForToday
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
            var result = AssignmentControllerTestContext.AssignmentController.FindForToday();
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AndThereAreAssignmentsAndNoAssignmentsForToday_NotFoundResultShouldBeReturned()
        {
            // Arrange
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(1), Name = "Cool stuff"});
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Sniff sniff"});
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindForToday();
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AndThereAreAssignmentsOnlyForToday_OkNegotiatedContentResultShouldBeReturned()
        {
            // Arrange
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today, Name = "Cool stuff for today" });
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Sniff sniff" });
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindForToday();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
