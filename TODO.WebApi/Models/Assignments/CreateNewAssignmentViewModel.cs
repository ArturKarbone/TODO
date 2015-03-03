using System;

namespace TODO.WebApi.Models.Assignments
{
    public class CreateNewAssignmentViewModel
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Done { get; set; }
    }
}