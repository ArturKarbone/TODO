using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TODO.Data.Assignments;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Assignments;
using TODO.WebApi.Controllers;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    public class AssignmentControllerTestContext
    {
        // In-memory list of data
        public List<Assignment> Assignments { get; set; }

        // Mock Repositories
        public Mock<IAssignmentRepository> MockAssignmentRepository { get; set; }

        // Services to test
        public AssignmentService AssignmentService { get; set; }

        // Controllers
        public AssignmentController AssignmentController { get; set; }

        public AssignmentControllerTestContext()
        {
            // Data
            Assignments = new List<Assignment>
            {
                new Assignment {Id = 1, Done = false, DueDate = DateTime.Today, Name = "Do some work One"},
                new Assignment {Id = 2, Done = false, DueDate = DateTime.Today.AddDays(2), Name = "Do some work Two"},
                new Assignment {Id = 3, Done = false, DueDate = DateTime.Today, Name = "Do some work Three"},
                new Assignment {Id = 4, Done = false, DueDate = DateTime.Today, Name = "Do some work Four"}
            };
            // Repos
            MockAssignmentRepository = new Mock<IAssignmentRepository>();

            // Some db behavior
            MockAssignmentRepository.Setup(x => x.FindById(It.IsAny<int>()))
                .Returns((int i) => Assignments.Find(x => x.Id == i));

            MockAssignmentRepository.Setup(x => x.FindAll()).Returns(Assignments.ToList());

            // Services
            AssignmentService = new AssignmentService(MockAssignmentRepository.Object);

            // Controllers
            AssignmentController = new AssignmentController(AssignmentService);
        }
    }
}

