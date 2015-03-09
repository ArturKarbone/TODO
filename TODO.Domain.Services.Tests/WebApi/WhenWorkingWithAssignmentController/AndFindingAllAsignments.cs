using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.Tests.WebApi.WhenWorkingWithAssignmentController
{
    public class AndFindingAllAsignments
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
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
                DomainTestContext2.AssignmentController.Create(assignment);
            }
            // Action
            var result = DomainTestContext2.AssignmentController.FindAll();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
