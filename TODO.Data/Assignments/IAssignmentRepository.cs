using System.Collections.Generic;
using TODO.Domain.Core.Entities;

namespace TODO.Data.Assignments
{
    public interface IAssignmentRepository
    {
        void Create(Assignment assignment);
        void Update(Assignment assignment);
        void Delete(int id);

        Assignment FindById(int id);
        List<Assignment> FindAll();
        List<Assignment> FindForToday();
        List<Assignment> FindForNextWeek();
    }
}
