using System;
using System.Collections.Generic;
using TODO.Data.Assignments;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Validation.Assignments
{
    public class UpdateAssignmentValidator
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public UpdateAssignmentValidator(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public IEnumerable<string> Validate(Assignment assignment)
        {
            if (assignment == null) yield return "The task is null.";
            if (assignment != null && assignment.Id <= 0) yield return "Id is invalid.";
            if (assignment != null && string.IsNullOrWhiteSpace(assignment.Name)) yield return "Name is invalid.";
            if (assignment != null && assignment.DueDate < DateTime.Today) yield return "Date is invalid.";
            if (assignment != null && assignment.Id > 0)
            {
                if (_assignmentRepository.FindById(assignment.Id) == null) yield return "There's no task with this id.";
            }
        }
    }
}
