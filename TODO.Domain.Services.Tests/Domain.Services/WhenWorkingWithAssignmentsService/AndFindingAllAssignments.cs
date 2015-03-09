using System.Collections.Generic;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Tests.Domain.Services.WhenWorkingWithAssignmentsService
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
