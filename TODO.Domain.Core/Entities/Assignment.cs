using System;

namespace TODO.Domain.Core.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Done { get; set; }
    }
}
