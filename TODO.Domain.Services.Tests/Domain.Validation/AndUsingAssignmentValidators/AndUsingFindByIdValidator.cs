using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Validation.AndUsingAssignmentValidators
{
    [TestFixture]
    public class AndUsingFindByIdValidator
    {
        public DomainTestContext DomainTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext = new DomainTestContext();
        }

        [Test]
        public void AndIdIsValid()
        {
            // Arrange
            // Action
            var result = DomainTestContext.FindByIdValidator.Validate(3);
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
                results.Add(new DomainValidationException {ValidationErrors = DomainTestContext.FindByIdValidator.Validate(id).ToList()});
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
            var result = DomainTestContext.FindByIdValidator.Validate(356);
            // Assert
            Assert.AreEqual("The id does not exist.", result.First());
        }
    }
}
