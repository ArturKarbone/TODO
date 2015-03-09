using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingUndoneAssignments
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
            Assert.IsNull(DomainTestContext2.AssignmentService.FindUndone());
        }

        [Test]
        public void AndThereAreAssignmentsAndNoUndoneAssignments_NullShouldBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentService.Create(new Assignment { Done = true, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" });
            DomainTestContext2.AssignmentService.Create(new Assignment { Done = true, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" });
            // Action
            // Assert
            Assert.IsNull(DomainTestContext2.AssignmentService.FindUndone());
        }

        [Test]
        public void AndThereAreOnlyUndoneAssignments_ListOfAssignmentsShouldBeReturned()
        {
            // Arrange
            var todo = new List<Assignment>
            {
                new Assignment { Done = false, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" },
                new Assignment { Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" }
            };
            foreach (var assignment in todo)
            {
                DomainTestContext2.AssignmentService.Create(assignment);
            }
            // Action
            var assignments = DomainTestContext2.AssignmentService.FindUndone();
            // Assert
            Assert.AreEqual(todo, assignments);
        }

        [Test]
        public void AndThereAreAssignmentsAndUndoneAssignments_ListOfAssignmentsShouldBeReturned()
        {
            // Arrange
            var todo = new List<Assignment>
            {
                new Assignment { Done = true, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" },
                new Assignment { Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" }
            };
            foreach (var assignment in todo)
            {
                DomainTestContext2.AssignmentService.Create(assignment);
            }
            // Action
            var assignments = DomainTestContext2.AssignmentService.FindUndone();
            // Assert
            Assert.IsTrue(assignments.Count() == 1);
            Assert.AreEqual(todo.First(x => !x.Done), assignments.First());
        }
    }
}
