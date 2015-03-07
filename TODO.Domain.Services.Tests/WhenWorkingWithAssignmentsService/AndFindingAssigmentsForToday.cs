using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Tests.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingAssigmentsForToday
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
            var assignments = DomainTestContext2.AssignmentService.FindForToday();
            // Assert
            Assert.IsNull(assignments);
        }

        [Test]
        public void AndThereAreAssignmentsAndNoAssignmentsForToday_NullShouldBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentService.Create(new Assignment {Done = false, DueDate = DateTime.Today.AddDays(1), Name = "Do the dishes"});
            // Action
            var assignments = DomainTestContext2.AssignmentService.FindForToday();
            // Assert
            Assert.IsNull(assignments);
        }

        [Test]
        public void AndThereAreAssignmentsOnlyForToday_ListOfAssignmentsShouldBeReturned()
        {
            // Arrange
            var todo = new List<Assignment>
            {
                new Assignment { Done = false, DueDate = DateTime.Today, Name = "FOR TODAY FIRST" },
                new Assignment { Done = false, DueDate = DateTime.Today, Name = "FOR TODAY SECOND"}
            };
            foreach (var assignment in todo)
            {
                DomainTestContext2.AssignmentService.Create(assignment);
            }
            // Action
            var assignments = DomainTestContext2.AssignmentService.FindForToday();
            // Assert
            Assert.AreEqual(todo, assignments);
        }

        [Test]
        public void AndThereAreAssignmentsAndAssignmentsForToday_ListOfAssignmentsShouldBeReturned()
        {
            // Arrange
            var todo = new List<Assignment>
            {
                new Assignment { Done = false, DueDate = DateTime.Today, Name = "FOR TODAY" },
                new Assignment { Done = false, DueDate = DateTime.Today.AddDays(1), Name = "Task 4 tmrw"},
                new Assignment { Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Task 4 tmrw + 1"},
                new Assignment { Done = false, DueDate = DateTime.Today.AddDays(3), Name = "Task 4 tmrw + 2"}
            };
            foreach (var assignment in todo)
            {
                DomainTestContext2.AssignmentService.Create(assignment);
            }
            // Action
            var assignments = DomainTestContext2.AssignmentService.FindForToday();
            // Assert
            Assert.AreEqual(new List<Assignment> { todo.First(x => x.Name == "FOR TODAY")}, assignments);
        }
    }
}
