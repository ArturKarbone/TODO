using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Validation.AndUsingAssignmentValidators
{
    [TestFixture]
    public class AndUsingCreateNewAssignmentValidator
    {
        public DomainTestContext DomainTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext = new DomainTestContext();
        }

        [Test]
        public void AndAssignmentIsNull()
        {
            // Arrange
            // Action
            var result = DomainTestContext.CreateNewAssignmentValidator.Validate(null);
            // Assert
            Assert.AreEqual("The task is null.", result.First());
        }

        [Test]
        public void AndAssignmentIsValid()
        {
            // Arrange
            var goodTask = new Assignment {Id = 0, Name = "Do some work", Done = false, DueDate = DateTime.Today};
            // Action
            var result = DomainTestContext.CreateNewAssignmentValidator.Validate(goodTask);
            // Assert
            Assert.IsEmpty(result);
        }


        [Test]
        public void AndAssignmentIdIsInvalid()
        {
            // Arrange
            var badTasks = new List<Assignment>
            {
                new Assignment {Id = int.MinValue, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = int.MaxValue, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = 985, Done = false, DueDate = DateTime.Today, Name = "Do some work"},
                new Assignment {Id = -6787, Done = false, DueDate = DateTime.Today, Name = "Do some work"}
            };

            // Action
            var results = new List<DomainValidationException>();
            foreach (var badTask in badTasks)
            {
                results.Add(new DomainValidationException{ValidationErrors = DomainTestContext.CreateNewAssignmentValidator.Validate(badTask).ToList()});
            }

            // Assert
            foreach (var result in results)
            {
                Assert.AreEqual("Id is invalid.", result.ValidationErrors.First());
            }
        }

        [Test]
        public void AndAssignmentNameIsEmptyOrNullOrWhiteSpace()
        {
            // Arrange
            var badTasks = new List<Assignment>
            {
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = string.Empty},
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = null},
                new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "    "},
            };

            // Action
            var results = new List<DomainValidationException>();
            foreach (var badTask in badTasks)
            {
                results.Add(new DomainValidationException { ValidationErrors = DomainTestContext.CreateNewAssignmentValidator.Validate(badTask).ToList() });
            }

            // Assert
            foreach (var result in results)
            {
                Assert.AreEqual("Name is invalid.", result.ValidationErrors.First());
            }
        }

        [Test]
        public void AndAssignmentDueDateIsInvalid()
        {
            // Arrange
            var badTask = new Assignment
            {
                Id = 0,
                Done = false,
                DueDate = new DateTime(DateTime.Now.Year, DateTime.Today.Month - 1, DateTime.Today.Day ),
                Name = "Do some work"
            };

            // Action
            var results = DomainTestContext.CreateNewAssignmentValidator.Validate(badTask);
            
            // Assert
            Assert.AreEqual("Date is invalid.", results.First());
        }
    }
}
