using System;
using System.Collections.Generic;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Tests.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingAssignmentsForNextWeek
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
        }

        [Test]
        public void AndThereAreNoAssignmentsAtAll_NullShouldBeReturned()
        {
            // Arrange
            // Action
            // Assert
            Assert.IsNull(DomainTestContext2.AssignmentService.FindForNextWeek());
        }

        [Test]
        public void AndThereAreAssignmentsAndNoAssignmentsForNextWeek_NullShouldBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentService.Create(new Assignment { Done = false, DueDate = DateTime.Today.AddDays(9), Name = "Task long away"});
            DomainTestContext2.AssignmentService.Create(new Assignment { Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" });
            // Action
            // Assert
            Assert.IsNull(DomainTestContext2.AssignmentService.FindForNextWeek());
        }

        [Test]
        public void AndThereAreAssignmentsAndAssignmentsForNextWeek_ListOfAssignmentsShouldBeReturned()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment {Done = false, DueDate = DateTime.Today.AddDays(77), Name = "Cool name"},
                new Assignment {Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Cool name NW"},
                new Assignment {Done = false, DueDate = DateTime.Today.AddDays(7), Name = "Cool name NW"},
                new Assignment {Done = false, DueDate = DateTime.Today.AddDays(5), Name = "Cool name NW"}
            };
            foreach (var assignment in assignments)
            {
                DomainTestContext2.AssignmentService.Create(assignment);
            }
            // Action
            var assignmentsForNextWeek = DomainTestContext2.AssignmentService.FindForNextWeek();
            // Assert
            Assert.IsTrue(assignmentsForNextWeek.Count == 3);
            Assert.AreEqual(assignments.FindAll(x => x.Name == "Cool name NW"), assignmentsForNextWeek);
        }
    }
}
