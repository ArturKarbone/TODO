using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Tests.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingDoneAssignments
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
            Assert.IsNull(DomainTestContext2.AssignmentService.FindDone());
        }

        [Test]
        public void AndThereAreAssignmentsAndNoDoneAssignments_NullShouldBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentService.Create(new Assignment { Done = false, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" });
            DomainTestContext2.AssignmentService.Create(new Assignment { Done = false, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" });
            // Action
            // Assert
            Assert.IsNull(DomainTestContext2.AssignmentService.FindDone());
        }

        [Test]
        public void AndThereAreOnlyDoneAssignments_ListOfAssignmentsShouldBeReturned()
        {
            // Arrange
            var todo = new List<Assignment>
            {
                new Assignment { Done = true, DueDate = DateTime.Today.AddDays(9), Name = "Task long away" },
                new Assignment { Done = true, DueDate = DateTime.Today.AddDays(8), Name = "Task not so long away" }
            };
            foreach (var assignment in todo)
            {
                DomainTestContext2.AssignmentService.Create(assignment);
            }
            // Action
            var assignments = DomainTestContext2.AssignmentService.FindDone();
            // Assert
            Assert.AreEqual(todo, assignments);
        }

        [Test]
        public void AndThereAreAssignmentsAndDoneAssignments_ListOfAssignmentsShouldBeReturned()
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
            var assignments = DomainTestContext2.AssignmentService.FindDone();
            // Assert
            Assert.IsTrue(assignments.Count() == 1);
            Assert.AreEqual(todo.First(), assignments.First());
        }
    }
}
