using System;

namespace TODO.WebApi.Models.Assignments
{
    public class UpdateAssignmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Done { get; set; }
    }
}