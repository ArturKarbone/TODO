using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TODO.Data.Assignments;
using TODO.Domain.Core.Entities;

namespace TODO.Tests
{
    public class AssignmentRepositoryMock
    {
        public Mock<IAssignmentRepository> AssignmentRepository { get; set; }

        public List<Assignment> Assignments { get; set; }

        public AssignmentRepositoryMock()
        {
            Assignments = new List<Assignment>();

            AssignmentRepository = new Mock<IAssignmentRepository>();

            // Some db behavior
            // FindById
            AssignmentRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) =>
            {
                if (Assignments.Any()) return Assignments.Find(x => x.Id == id);
                return null;
            });
            // FindAll
            AssignmentRepository.Setup(x => x.FindAll()).Returns(() =>
            {
                if (Assignments.Any()) return Assignments.ToList();
                return null;
            });
            // FindForToday
            AssignmentRepository.Setup(x => x.FindForToday())
                .Returns(() =>
                {
                    if (Assignments.Any())
                    {
                        var assignments = Assignments.Where(x => x.DueDate == DateTime.Today).Where(x => !x.Done);
                        if (assignments.Any()) return assignments.ToList();
                        return null;
                    }
                    return null;
                });
            // FindForNextWeek
            AssignmentRepository.Setup(x => x.FindForNextWeek()).Returns(() =>
            {
                if (Assignments.Any())
                {
                    var assignments = Assignments.Where(x => x.DueDate < DateTime.Today.AddDays(7)).Where(x => !x.Done);
                    if (assignments.Any()) return assignments.ToList();
                    return null;
                }
                return null;
            });
            // Find Done
            AssignmentRepository.Setup(x => x.FindDone()).Returns(() =>
            {
                if (Assignments.Any())
                {
                    var assignments = Assignments.Where(x => x.Done);
                    if (assignments.Any()) return assignments.ToList();
                    return null;
                }
                return null;
            });
            // Find undone
            AssignmentRepository.Setup(x => x.FindUndone()).Returns(() =>
            {
                if (Assignments.Any())
                {
                    var assignments = Assignments.Where(x => !x.Done);
                    if (assignments.Any()) return assignments.ToList();
                    return null;
                }
                return null;
            });
            // Update
            AssignmentRepository.Setup(x => x.Update(It.IsAny<Assignment>())).Callback((Assignment assignment) =>
            {
                var assignmentDb = Assignments.First(x => x.Id == assignment.Id);
                Assignments.Remove(assignmentDb);
                assignmentDb.Name = assignment.Name;
                assignmentDb.Done = assignment.Done;
                assignmentDb.DueDate = assignment.DueDate;
                Assignments.Add(assignmentDb);
            });
            // Delete
            AssignmentRepository.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var assignment = Assignments.First(x => x.Id == id);
                Assignments.Remove(assignment);
            });
            // Create
            AssignmentRepository.Setup(x => x.Create(It.IsAny<Assignment>())).Callback(
                (Assignment assignment) =>
                {
                    assignment.Id = Assignments.Count + 1;
                    Assignments.Add(assignment);
                });
        }
    }
}
