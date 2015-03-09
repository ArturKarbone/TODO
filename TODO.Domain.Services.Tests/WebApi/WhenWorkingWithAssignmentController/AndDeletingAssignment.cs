using System;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.WebApi.Models.Assignments;

namespace TODO.Tests.WebApi.WhenWorkingWithAssignmentController
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
        public void AndAssignmentExist_OkResultMustBeReturned()
        {
            // Arrange
            DomainTestContext2.AssignmentController.Create(new CreateNewAssignmentViewModel
            {
                Done = false,
                DueDate = DateTime.Today,
                Name = "Some simple task for today"
            });
            // Action
            var result = DomainTestContext2.AssignmentController.Delete(1);
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void AndAssignmentDoesNotExist_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = DomainTestContext2.AssignmentController.Delete(66);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public void AndIdIsInvalid_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = DomainTestContext2.AssignmentController.Delete(-23);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }
    }
}
