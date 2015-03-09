using System;
using System.Collections.Generic;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingAllAssignments
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
            DomainTestContext2.AssignmentService.Create(new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "Do the dishes."});
            // Action
            var result = DomainTestContext2.AssignmentService.FindAll();

            // Assert
            Assert.IsInstanceOf<List<Assignment>>(result);
        }
    }
}
