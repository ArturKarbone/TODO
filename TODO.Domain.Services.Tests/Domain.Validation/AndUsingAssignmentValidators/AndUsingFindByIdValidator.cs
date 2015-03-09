using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Validation.AndUsingAssignmentValidators
{
    [TestFixture]
    public class AndUsingFindByIdValidator
    {
        public DomainTestContext2 DomainTestContext2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext2 = new DomainTestContext2();
        }

        [Test]
        public void AndIdIsValid()
        {
            // Arrange
            DomainTestContext2.AssignmentService.Create(new Assignment {Id = 0, Done = false, DueDate = DateTime.Today, Name = "Do the dishes."});
            // Action
            var result = DomainTestContext2.FindByIdValidator.Validate(1);
            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void AndIdIsInvalid()
        {
            // Arrange
            var ids = new[] {-250, 0, int.MinValue};
            // Action
            var results = new List<DomainValidationException>();
            foreach (var id in ids)
            {
                results.Add(new DomainValidationException {ValidationErrors = DomainTestContext2.FindByIdValidator.Validate(id).ToList()});
            }
            // Assert
            foreach (var result in results)
            {
                Assert.AreEqual("Id is invalid.", result.ValidationErrors.First());
            }
        }

        [Test]
        public void AndIdDoesNotExist()
        {
            // Arrange
            // Action
            var result = DomainTestContext2.FindByIdValidator.Validate(356);
            // Assert
            Assert.AreEqual("The id does not exist.", result.First());
        }
    }
}
