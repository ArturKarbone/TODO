using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndCreatingNewAssignment
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
            Assert.Throws<DomainValidationException>( () => DomainTestContext2.AssignmentService.Create(null) );
        }

        [Test]
        public void AndAssignmentIsValid_AssignmentRepositoryCreateShouldBeCalled()
        {
            // Arrange
            var goodTask = new Assignment {Id = 0, Name = "The new task FTW!", Done = false, DueDate = DateTime.Today};
            // Action
            DomainTestContext2.AssignmentService.Create(goodTask);
            // Assert
            DomainTestContext2.AssignmentRepositoryMock.AssignmentRepository.Verify(x => x.Create(It.IsAny<Assignment>()), Times.Once);
        }

        [Test]
        public void AndAssignmentIsInvalid_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            var badTasks = new List<Assignment>
            {
                new Assignment {Id = int.MinValue, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = int.MaxValue, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = 985, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = -6787, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = string.Empty},
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = null},
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "    "},
                new Assignment {Id = 0, Done = false, DueDate = new DateTime(DateTime.Now.Year, DateTime.Today.Month - 1, DateTime.Today.Day ), Name = "Do some work"},
                new Assignment {Id = 0, Done = false, Name = "Hallol", DueDate = new DateTime(2015, 3, 8, 23, 58, 58)}
            };
            // Action
            // Assert
            foreach (var badTask in badTasks)
            {
                Assert.Throws<DomainValidationException>(() => DomainTestContext2.AssignmentService.Create(badTask));
            }
        }
    }
}
