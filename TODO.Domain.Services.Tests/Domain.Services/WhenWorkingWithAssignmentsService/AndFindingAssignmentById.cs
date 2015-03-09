using System;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingAssignmentById
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
        }

        [Test]
        public void AndIdIsValid_AssignmentMustBeReturned()
        {
            // Arrange
            var goodTask = new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "Do the dishes"};
            DomainTestContext2.AssignmentService.Create(goodTask);
            // Action
            var assignment = DomainTestContext2.AssignmentService.FindById(1);
            // Assert  
            Assert.AreEqual(goodTask, assignment);
        }

        [Test]
        public void AndIdIsInvalid_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            var ids = new[] { -1477, 0, int.MinValue };
            // Action
            // Assert 
            foreach (var id in ids)
            {
                Assert.Throws<DomainValidationException>(() => DomainTestContext2.AssignmentService.FindById(id));
            }
        }

        [Test]
        public void AndIdDoesNotExist_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            // Action
            // Assert 
            Assert.Throws<DomainValidationException>(() => DomainTestContext2.AssignmentService.FindById(876));
        }
    }
}
