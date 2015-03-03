using System.Collections.Generic;
using TODO.Data.Assignments;

namespace TODO.Domain.Services.Validation.Assignments
{
    public class IdValidator
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public IdValidator(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public IEnumerable<string> Validate(int id)
        {
            if (id <= 0) yield return "Id is invalid.";
            if (id > 0 && _assignmentRepository.FindById(id) == null) yield return "The id does not exist.";
        }
    }
}
