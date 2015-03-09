using Moq;
using NUnit.Framework;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndDeletingAssignment
    {
        public DomainTestContext DomainTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext = new DomainTestContext();
        }

        [Test]
        public void AndIdIsValid_AssignmentRepositoryDeleteShouldBeCalled()
        {
            // Arrange
            // Action
            DomainTestContext.AssignmentService.Delete(2);
            // Assert  
            DomainTestContext.MockAssignmentRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
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
                Assert.Throws<DomainValidationException>(() => DomainTestContext.AssignmentService.Delete(id));
            }
        }

        [Test]
        public void AndIdDoesNotExist_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            // Action
            // Assert 
            Assert.Throws<DomainValidationException>(() => DomainTestContext.AssignmentService.Delete(796));
        }
    }
}
