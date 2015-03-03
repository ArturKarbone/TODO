using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.WebApi.Models.Assignments;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndCreatingNewAssignment
    {
        public AssignmentControllerTestContext AssignmentControllerTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            AssignmentControllerTestContext = new AssignmentControllerTestContext();
        }

        [Test]
        public void AndAssignmentIsValid_OkResultMustBeReturned()
        {
            // Arrange
            var goodTask = new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today, Name = "DO SOME W3K!"};
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Create(goodTask);
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void AndAssignmentIsNull_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Create(null);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public void AndAssignmentIsInvalid_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            var badTasks = new List<CreateNewAssignmentViewModel>
            {
                new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today, Name = string.Empty},
                new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today, Name = null},
                new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today, Name = "    "},
                new CreateNewAssignmentViewModel {Done = false, DueDate = new DateTime(DateTime.Now.Year, DateTime.Today.Month - 1, DateTime.Today.Day ), Name = "Do some work"}
            };
            // Action
            var results = new List<IHttpActionResult>();
            foreach (var badTask in badTasks)
            {
                results.Add(AssignmentControllerTestContext.AssignmentController.Create(badTask));
            }
            // Assert
            foreach (var result in results)
            {
                Assert.IsInstanceOf<InvalidModelStateResult>(result);
            }
        }
    }
}
