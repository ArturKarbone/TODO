using System;
using Moq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndDeletingAssignment
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
        }

        [Test]
        public void AndIdIsValid_AssignmentRepositoryDeleteShouldBeCalled()
        {
            // Arrange
            DomainTestContext2.AssignmentService.Create(new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "Do the dishes."});
            // Action
            DomainTestContext2.AssignmentService.Delete(1);
            // Assert  
            DomainTestContext2.AssignmentRepositoryMock.AssignmentRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void AndIdIsInvalid_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            var ids = new[] { -322, 0, int.MinValue };
            // Action
            // Assert 
            foreach (var id in ids)
            {
                Assert.Throws<DomainValidationException>(() => DomainTestContext2.AssignmentService.Delete(id));
            }
        }

        [Test]
        public void AndIdDoesNotExist_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            // Action
            // Assert 
            Assert.Throws<DomainValidationException>(() => DomainTestContext2.AssignmentService.Delete(796));
        }
    }
}
