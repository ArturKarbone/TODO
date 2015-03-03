using System.Collections.Generic;
using System.Linq;
using TODO.Data.Context;
using TODO.Domain.Core.Entities;

namespace TODO.Data.Assignments
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly DataDbContext _dataDbContext;

        public AssignmentRepository(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public void Create(Assignment assignment)
        {
            _dataDbContext.Assignments.Add(assignment);
            _dataDbContext.SaveChanges();
        }

        public void Update(Assignment assignment)
        {
            var oldAssignment = _dataDbContext.Assignments.Find(assignment.Id);
            oldAssignment.Name = assignment.Name;
            oldAssignment.Done = assignment.Done;
            oldAssignment.DueDate = assignment.DueDate;
            _dataDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var assignment = _dataDbContext.Assignments.Find(id);
            _dataDbContext.Assignments.Remove(assignment);
            _dataDbContext.SaveChanges();
        }

        public Assignment FindById(int id)
        {
            var assignment = _dataDbContext.Assignments.Find(id);
            if (assignment != null)
            {
                return assignment;
            }
            return null;
        }

        public List<Assignment> FindAll()
        {
            if (_dataDbContext.Assignments.Any())
            {
                return _dataDbContext.Assignments.ToList();
            }
            return null;;
        }
    }
}
