﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TODO.Data.Assignments;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Assignments;
using TODO.WebApi.Controllers;

namespace TODO.WebApi.Tests.WhenWorkingWithAssignmentController
{
    public class AssignmentControllerTestContext
    {
        // In-memory list of data
        public List<Assignment> Assignments { get; set; }

        // Mock Repositories
        public Mock<IAssignmentRepository> MockAssignmentRepository { get; set; }

        // Services to test
        public AssignmentService AssignmentService { get; set; }

        // Controllers
        public AssignmentController AssignmentController { get; set; }

        public AssignmentControllerTestContext()
        {
            // Data
            Assignments = new List<Assignment>();

            // Repos
            MockAssignmentRepository = new Mock<IAssignmentRepository>();

            // Some db behavior
            // FindById
            MockAssignmentRepository.Setup(x => x.FindById(It.IsAny<int>())).Returns((int id) =>
            {
                if (Assignments.Any()) return Assignments.Find(x => x.Id == id);
                return null;
            });
            // FindAll
            MockAssignmentRepository.Setup(x => x.FindAll()).Returns(() =>
            {
                if (Assignments.Any()) return Assignments.ToList();
                return null;
            });
            // FindForToday
            MockAssignmentRepository.Setup(x => x.FindForToday())
                .Returns(() =>
                {
                    if (!Assignments.Any()) return null;
                    var assignments = Assignments.Where(x => x.DueDate.ToShortDateString() == DateTime.Today.ToShortDateString());
                    return assignments.Any() ? assignments.ToList() : null;
                });
            // Delete
            MockAssignmentRepository.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var assignment = Assignments.First(x => x.Id == id);
                Assignments.Remove(assignment);
            });
            // Create
            MockAssignmentRepository.Setup(x => x.Create(It.IsAny<Assignment>())).Callback(
                (Assignment assignment) =>
                {
                    assignment.Id = Assignments.Count + 1;
                    Assignments.Add(assignment);
                });

            // Services
            AssignmentService = new AssignmentService(MockAssignmentRepository.Object);

            // Controllers
            AssignmentController = new AssignmentController(AssignmentService);
        }
    }
}

