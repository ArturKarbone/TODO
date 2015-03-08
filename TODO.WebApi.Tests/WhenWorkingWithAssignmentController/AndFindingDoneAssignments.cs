using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndFindingDoneAssignments
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
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(AssignmentControllerTestContext.AssignmentController.FindDone());
        }

        [Test]
        public void AndThereAreAssignmentsAndNoDoneAssignments_NotFoundResultShouldBeReturned()
        {
            // Arrange
            var todo = new List<CreateNewAssignmentViewModel>
            {
                new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today.AddDays(9), Name = "Task long away"},
                new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away"}
            };
            foreach (var createNewAssignmentViewModel in todo)
            {
                AssignmentControllerTestContext.AssignmentController.Create(createNewAssignmentViewModel);
            }
            // Action
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(AssignmentControllerTestContext.AssignmentController.FindDone());
        }

        [Test]
        public void AndThereAreOnlyDoneAssignments_OkNegotiatedContentResultShouldBeReturned()
        {
            // Arrange
            var todo = new List<CreateNewAssignmentViewModel>
            {
                new CreateNewAssignmentViewModel { Done = true, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" },
                new CreateNewAssignmentViewModel { Done = true, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" }
            };
            foreach (var assignment in todo)
            {
                AssignmentControllerTestContext.AssignmentController.Create(assignment);
            }
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindDone();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }

        [Test]
        public void AndThereAreAssignmentsAndDoneAssignments_OkNegotiatedContentResultShouldBeReturned()
        {
            // Arrange
            var todo = new List<CreateNewAssignmentViewModel>
            {
                new CreateNewAssignmentViewModel { Done = true, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" },
                new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" }
            };
            foreach (var assignment in todo)
            {
                AssignmentControllerTestContext.AssignmentController.Create(assignment);
            }
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindDone();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
