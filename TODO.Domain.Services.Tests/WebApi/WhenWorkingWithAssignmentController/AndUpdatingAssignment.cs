using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.WebApi.Models.Assignments;

namespace TODO.Domain.Services.Tests.WebApi.WhenWorkingWithAssignmentController
{
    [TestFixture]
    public class AndUpdatingAssignment
    {
        public AssignmentControllerTestContext AssignmentControllerTestContext { get; set; }

        [SetUp]
        public void Setup()
        {
            AssignmentControllerTestContext = new AssignmentControllerTestContext();
        }

        [Test]
        public void AndAssignmentIsNull_InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Update(null);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public void AndAssignmentIsValid_OkResultMustBeReturned()
        {
            // Arrange
            AssignmentControllerTestContext.AssignmentController.Create(new CreateNewAssignmentViewModel {Done = false, DueDate = DateTime.Today, Name = "Some name"});
            var goodTask = new UpdateAssignmentViewModel {Id = 1, Name = "DONE!", Done = true, DueDate = DateTime.Today};
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Update(goodTask);
            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void AndAssignmentIsInvalid__InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            var badTasks = new List<UpdateAssignmentViewModel>
            {
                new UpdateAssignmentViewModel {Id = int.MaxValue, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new UpdateAssignmentViewModel {Id = -6787, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new UpdateAssignmentViewModel {Id = 0, Done = false, DueDate = DateTime.Today, Name = "Bad Id"},
                new UpdateAssignmentViewModel {Id = 1, Done = false, DueDate = DateTime.Today, Name = string.Empty},
                new UpdateAssignmentViewModel {Id = 2, Done = false, DueDate = DateTime.Today, Name = null},
                new UpdateAssignmentViewModel {Id = 3, Done = false, DueDate = DateTime.Today, Name = "    "},
                new UpdateAssignmentViewModel {Id = 4, Done = false, DueDate = new DateTime(DateTime.Now.Year, DateTime.Today.Month, DateTime.Today.Day - 1), Name = "Bad Date"}
            };
            // Action
            var results = new List<IHttpActionResult>();
            foreach (var badTask in badTasks)
            {
                results.Add(AssignmentControllerTestContext.AssignmentController.Update(badTask));
            }
            // Assert
            foreach (var result in results)
            {
                Assert.IsInstanceOf<InvalidModelStateResult>(result);
            }
        }

        [Test]
        public void AndAssignmentDoesNotExist__InvalidModelStateResultMustBeReturned()
        {
            // Arrange
            var badTask = new UpdateAssignmentViewModel
            {
                Id = 985,
                Done = false,
                DueDate = DateTime.Today,
                Name = "Bad Id"
            };
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.Update(badTask);
            // Assert
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }
    }
}
