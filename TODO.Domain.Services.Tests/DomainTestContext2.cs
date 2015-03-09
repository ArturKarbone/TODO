using TODO.Domain.Services.Assignments;
using TODO.Domain.Services.Validation.Assignments;

namespace TODO.Tests
{
    public class DomainTestContext2
    {
        // Services to test
        public AssignmentService AssignmentService { get; set; }

        // Assignments validators
        public CreateNewAssignmentValidator CreateNewAssignmentValidator { get; set; }
        public UpdateAssignmentValidator UpdateAssignmentValidator { get; set; }
        public IdValidator FindByIdValidator { get; set; }

        public DomainTestContext2()
        {
            var repository = new AssignmentRepositoryMock();

            // Services
            AssignmentService = new AssignmentService(repository.AssignmentRepository.Object);

            // Validators
            CreateNewAssignmentValidator = new CreateNewAssignmentValidator();
            UpdateAssignmentValidator = new UpdateAssignmentValidator(repository.AssignmentRepository.Object);
            FindByIdValidator = new IdValidator(repository.AssignmentRepository.Object);
        }
    }
}
