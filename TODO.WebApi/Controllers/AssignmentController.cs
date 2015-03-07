using System.Linq;
using System.Web.Http;
using AutoMapper;
using TODO.Domain.Core.Entities;
using TODO.Domain.Services.Assignments;
using TODO.Domain.Services.Validation;
using TODO.WebApi.Models.Assignments;

namespace TODO.WebApi.Controllers
{
    [RoutePrefix("api/tasks")]
    public class AssignmentController : ApiController
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(CreateNewAssignmentViewModel model)
        {
            try
            {
                Mapper.CreateMap<CreateNewAssignmentViewModel, Assignment>();
                var assignment = Mapper.Map<Assignment>(model);
                _assignmentService.Create(assignment);
                return Ok();
            }
            catch (DomainValidationException validationException)
            {
                foreach (var validationError in validationException.ValidationErrors)
                {
                    ModelState.AddModelError("validationError", validationError);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(UpdateAssignmentViewModel model)
        {
            try
            {
                Mapper.CreateMap<UpdateAssignmentViewModel, Assignment>();
                var assignment = Mapper.Map<Assignment>(model);
                _assignmentService.Update(assignment);
                return Ok();
            }
            catch (DomainValidationException validationException)
            {
                foreach (var validationError in validationException.ValidationErrors)
                {
                    ModelState.AddModelError("validationError", validationError);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _assignmentService.Delete(id);
                return Ok();
            }
            catch (DomainValidationException validationException)
            {
                foreach (var validationError in validationException.ValidationErrors)
                {
                    ModelState.AddModelError("validationError", validationError);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("findbyid")]
        public IHttpActionResult FindById(int id)
        {
            try
            {
                var assignment = _assignmentService.FindById(id);
                return Ok(assignment);
            }
            catch (DomainValidationException validationException)
            {
                foreach (var validationError in validationException.ValidationErrors)
                {
                    ModelState.AddModelError("validationError", validationError);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("findall")]
        public IHttpActionResult FindAll()
        {
            var assignments = _assignmentService.FindAll();
            if (assignments != null && assignments.Any())
            {
                return Ok(assignments);
            }
            return Ok();
        }

        [HttpGet]
        [Route("findfortoday")]
        public IHttpActionResult FindForToday()
        {
            var assignments = _assignmentService.FindForToday();
            if (assignments != null)
            {
                return Ok(assignments);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("findfornextweek")]
        public IHttpActionResult FindForNextWeek()
        {
            var assignments = _assignmentService.FindForNextWeek();
            if (assignments != null)
            {
                return Ok(assignments);
            }
            return NotFound();
        }
    }
}
