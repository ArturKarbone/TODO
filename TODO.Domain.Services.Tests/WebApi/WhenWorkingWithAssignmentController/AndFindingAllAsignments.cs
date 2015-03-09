using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.Domain.Services.Tests.WebApi.WhenWorkingWithAssignmentController
{
    public class AndFindingAllAsignments
    {
        public AssignmentControllerTestContext AssignmentControllerTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            AssignmentControllerTestContext = new AssignmentControllerTestContext();
        }

        [Test]
        public void AndThereAreSomeAssignments()
        {
            // Arrange
            var assignments = new List<CreateNewAssignmentViewModel>
            {
                new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today, Name = "Do some work One"},
                new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Do some work Two"},
                new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today, Name = "Do some work Three"},
                new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today, Name = "Do some work Four"}
            };
            foreach (var assignment in assignments)
            {
                AssignmentControllerTestContext.AssignmentController.Create(assignment);
            }
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindAll();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
