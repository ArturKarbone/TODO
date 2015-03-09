using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndUpdatingAssignment
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
        }

        [Test]
        public void AndAssignmentIsNull_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            // Action
            // Assert
            Assert.Throws<DomainValidationException>( () => DomainTestContext2.AssignmentService.Update(null));
        }

        [Test]
        public void AndAssignmentIsValid_AssignmentRepositoryUpdateShouldBeCalled()
        {
            // Arrange
            var goodTask = new Assignment{Id = 0, Done = true, DueDate = DateTime.Today, Name = "New Name"};
            DomainTestContext2.AssignmentService.Create(goodTask);
            // Action
            goodTask.Id = 1;
            goodTask.Name = "New New Name";
            DomainTestContext2.AssignmentService.Update(goodTask);
            // Assert
            DomainTestContext2.AssignmentRepositoryMock.AssignmentRepository.Verify(x => x.Update(It.IsAny<Assignment>()), Times.Once);
        }

        [Test]
        public void AndAssignmentIsInvalid_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            var badTasks = new List<Assignment>
            {
                new Assignment {Id = int.MinValue, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new Assignment {Id = int.MaxValue, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new Assignment {Id = 985, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new Assignment {Id = -6787, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new Assignment {Id = 1, Done = false, DueDate = DateTime.Today, Name = string.Empty},
                new Assignment {Id = 2, Done = false, DueDate = DateTime.Today, Name = null},
                new Assignment {Id = 3, Done = false, DueDate = DateTime.Today, Name = "    "},
                new Assignment {Id = 4, Done = false, DueDate = new DateTime(DateTime.Now.Year, DateTime.Today.Month, DateTime.Today.Day - 1), Name = "Bad Date"}
            };
            // Action
            // Assert
            foreach (var badTask in badTasks)
            {
                Assert.Throws<DomainValidationException>(() => DomainTestContext2.AssignmentService.Update(badTask));
            }
        }
    }
}
