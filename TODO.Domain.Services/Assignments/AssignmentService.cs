using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TODO.Data.Assignments;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Validation;
using TODO.Domain.Services.Validation.Assignments;

namespace TODO.Domain.Services.Assignments
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public void Create(Assignment assignment)
        {
            using (var transaction = new TransactionScope())
            {
                var validator = new CreateNewAssignmentValidator();
                var validationErrors = validator.Validate(assignment).ToList();
                if (validationErrors.Any()) throw new DomainValidationException {ValidationErrors = validationErrors};

                _assignmentRepository.Create(assignment);

                transaction.Complete();
            }
        }

        public void Update(Assignment assignment)
        {
            using (var transaction = new TransactionScope())
            {
                var validator = new UpdateAssignmentValidator(_assignmentRepository);
                var validationErrors = validator.Validate(assignment).ToList();
                if (validationErrors.Any()) throw new DomainValidationException { ValidationErrors = validationErrors };

                _assignmentRepository.Update(assignment);

                transaction.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var transaction = new TransactionScope())
            {
                var validator = new IdValidator(_assignmentRepository);
                var validationErrors = validator.Validate(id).ToList();
                if (validationErrors.Any()) throw new DomainValidationException { ValidationErrors = validationErrors };

                _assignmentRepository.Delete(id);

                transaction.Complete();
            }
        }

        public Assignment FindById(int id)
        {
            using (var transaction = new TransactionScope())
            {
                var validator = new IdValidator(_assignmentRepository);
                var validationErrors = validator.Validate(id).ToList();
                if (validationErrors.Any()) throw new DomainValidationException { ValidationErrors = validationErrors };

                transaction.Complete();
            }
            return _assignmentRepository.FindById(id);
        }

        public List<Assignment> FindAll()
        {
            return _assignmentRepository.FindAll();
        }

        public List<Assignment> FindForToday()
        {
            return _assignmentRepository.FindForToday();
        }

        public List<Assignment> FindForNextWeek()
        {
            return _assignmentRepository.FindForNextWeek();
        }

        public List<Assignment> FindDone()
        {
            return _assignmentRepository.FindDone();
        }

        public List<Assignment> FindUndone()
        {
            return _assignmentRepository.FindUndone();
        }
    }
}
