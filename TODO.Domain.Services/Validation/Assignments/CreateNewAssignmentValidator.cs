using System;
using System.Collections.Generic;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Validation.Assignments
{
    public class CreateNewAssignmentValidator
    {
        public IEnumerable<string> Validate(Assignment assignment)
        {
            if (assignment == null) yield return "The task is null.";
            if (assignment != null && assignment.Id != 0) yield return "Id is invalid.";
            if (assignment != null && string.IsNullOrWhiteSpace(assignment.Name)) yield return "Name is invalid.";
            if (assignment != null && assignment.DueDate < DateTime.Today) yield return "Date is invalid.";
        }
    }
}
