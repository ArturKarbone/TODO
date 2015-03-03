using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using TODO.Domain.Core.Entities;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    public class AndFindingAllAsignments
    {
        public AssignmentControllerTestContext AssignmentControllerTestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            AssignmentControllerTestContext = new AssignmentControllerTestContext();
        }

        [Test]
        public void AndThereAreSomeAssignments()
        {
            // Arrange
            // Action
            var result = AssignmentControllerTestContext.AssignmentController.FindAll();
            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<Assignment>>>(result);
        }
    }
}
