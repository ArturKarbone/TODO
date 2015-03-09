using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.WebApi.Models.Assignments;

namespace TODO.Tests.WebApi.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndFindingAssignmentsForToday
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
        }

        [Test]
        public void AndThereAreNoAssignmentsAtAll_NotFoundResultShouldBeReturned()
        {
            // Arrange
            // Action
            var result = DomainTestContext2.AssignmentController.FindForToday();
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AndThereAreAssignmentsAndNoAssignmentsForToday_NotFoundResultShouldBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(1), Name = "Cool stuff"});
            DomainTestContext2.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Sniff sniff"});
            // Action
            var result = DomainTestContext2.AssignmentController.FindForToday();
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AndThereAreAssignmentsOnlyForToday_OkNegotiatedContentResultShouldBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today, Name = "Cool stuff for today" });
            DomainTestContext2.AssignmentController.Create(new CreateNewAssignmentViewModel { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Sniff sniff" });
            // Action
            var result = DomainTestContext2.AssignmentController.FindForToday();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
