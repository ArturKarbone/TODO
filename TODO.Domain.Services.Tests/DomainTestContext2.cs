using TODO.Domain.Services.Assignments;
using TODO.Domain.Services.Validation.Assignments;
using TODO.WebApi.Controllers;

namespace TODO.Tests
{
    public class DomainTestContext2
    {
        // Mocked repository
        public AssignmentRepositoryMock AssignmentRepositoryMock { get; set; }

        // Services to test
        public AssignmentService AssignmentService { get; set; }

        // Controllers
        public AssignmentController AssignmentController { get; set; }

        // Assignments validators
        public CreateNewAssignmentValidator CreateNewAssignmentValidator { get; set; }
        public UpdateAssignmentValidator UpdateAssignmentValidator { get; set; }
        public IdValidator FindByIdValidator { get; set; }

        public DomainTestContext2()
        {
            AssignmentRepositoryMock = new AssignmentRepositoryMock();

            // Services
            AssignmentService = new AssignmentService(AssignmentRepositoryMock.AssignmentRepository.Object);

            // Controllers
            AssignmentController = new AssignmentController(AssignmentService);

            // Validators
            CreateNewAssignmentValidator = new CreateNewAssignmentValidator();
            UpdateAssignmentValidator = new UpdateAssignmentValidator(AssignmentRepositoryMock.AssignmentRepository.Object);
            FindByIdValidator = new IdValidator(AssignmentRepositoryMock.AssignmentRepository.Object);
        }
    }
}
