using System.Collections.Generic;
using TODO.Domain.Core.Entities;

namespace TODO.Domain.Services.Assignments
{
    public interface IAssignmentService
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
