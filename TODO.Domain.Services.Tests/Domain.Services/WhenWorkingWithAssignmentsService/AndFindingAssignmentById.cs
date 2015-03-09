using NUnit.Framework;
using TODO.Domain.Services.Validation;

namespace TODO.Tests.Domain.Services.WhenWorkingWithAssignmentsService
{
    [TestFixture]
    public class AndFindingAssignmentById
    {
        public DomainTestContext DomainTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            DomainTestContext = new DomainTestContext();
        }

        [Test]
        public void AndIdIsValid_AssignmentMustBeReturned()
        {
            // Arrange
            // Action
            var assignment = DomainTestContext.AssignmentService.FindById(2);
            // Assert  
            Assert.AreEqual(DomainTestContext.Assignments.Find(x => x.Id == 2), assignment);
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
                Assert.Throws<DomainValidationException>(() => DomainTestContext.AssignmentService.FindById(id));
            }
        }

        [Test]
        public void AndIdDoesNotExist_DomainValidationExceptionShouldBeRaised()
        {
            // Arrange
            // Action
            // Assert 
            Assert.Throws<DomainValidationException>(() => DomainTestContext.AssignmentService.FindById(876));
        }
    }
}
