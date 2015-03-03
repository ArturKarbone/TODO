using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Tests.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingAllAssignments
    {
        public DomainTestContext DomainTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext = new DomainTestContext();
        }

        [Test]
        public void AndThereAreSomeAssignments()
        {
            // Arrange
            // Action
            var result = DomainTestContext.AssignmentService.FindAll();

            // Assert
            Assert.IsInstanceOf<List<Assignment>>(result);
        }
    }
}
