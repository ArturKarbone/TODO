using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TODO.Data.Assignments;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Assignments;
using TODO.Domain.Services.Validation.Assignments;

namespace TODO.Domain.Services.Tests
{
    public class DomainTestContext2
    {
        public List<Assignment> Assignments { get; set; }

        // Mock Repositories
        public Mock<IAssignmentRepository> MockAssignmentRepository { get; set; }

        // Services to test
        public AssignmentService AssignmentService { get; set; }

        // Assignments validators
        public CreateNewAssignmentValidator CreateNewAssignmentValidator { get; set; }
        public UpdateAssignmentValidator UpdateAssignmentValidator { get; set; }
        public IdValidator FindByIdValidator { get; set; }

        public DomainTestContext2()
        {
            Assignments = new List<Assignment>();

            // Repos
            MockAssignmentRepository = new Mock<IAssignmentRepository>(MockBehavior.Strict);

            // Some db behavior
            // FindById
            MockAssignmentRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) =>
            {
                if (Assignments.Any()) return Assignments.Find(x => x.Id == id);
                return null;
            });
            // FindAll
            MockAssignmentRepository.Setup(x => x.FindAll()).Returns(() =>
            {
                if (Assignments.Any()) return Assignments.ToList();
                return null;
            });
            // FindForToday
            MockAssignmentRepository.Setup(x => x.FindForToday())
                .Returns(() =>
                {
                    if (!Assignments.Any()) return null;
                    var assignments = Assignments.Where(x => x.DueDate.ToShortDateString() == DateTime.Today.ToShortDateString());
                    return assignments.Any() ? assignments.ToList() : null;
                });
            // FindForNextWeek
            MockAssignmentRepository.Setup(x => x.FindForNextWeek()).Returns(() =>
            {
                if (Assignments.Any())
                {
                    var assignments = Assignments.Where(x => x.DueDate <= DateTime.Today.AddDays(7));
                    if (assignments.Any()) return assignments.ToList();
                    return null;
                }
                return null;
            });
            // Delete
            MockAssignmentRepository.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var assignment = Assignments.First(x => x.Id == id);
                Assignments.Remove(assignment);
            });
            // Create
            MockAssignmentRepository.Setup(x => x.Create(It.IsAny<Assignment>())).Callback(
                (Assignment assignment) =>
                {
                    assignment.Id = Assignments.Count + 1;
                    Assignments.Add(assignment);
                });
            // Services
            AssignmentService = new AssignmentService(MockAssignmentRepository.Object);


            // Validators
            CreateNewAssignmentValidator = new CreateNewAssignmentValidator();
            UpdateAssignmentValidator = new UpdateAssignmentValidator(MockAssignmentRepository.Object);
            FindByIdValidator = new IdValidator(MockAssignmentRepository.Object);
        }
    }
}
