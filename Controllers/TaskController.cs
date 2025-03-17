using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using TaskAppBackEnd.Model;

namespace TaskAppBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly DBManagement _context;

        public TaskController(ITaskService taskService, DBManagement context)
        {
            _taskService = taskService;
            _context = context;
        }

        [HttpGet]
        [Route("GetTasks")]
        public IActionResult GetTasks()
        {
            string email = Request.Headers["email"]!;
            string pass = String.IsNullOrEmpty(Request.Headers["pass"]) ? "No aplica" : Request.Headers["pass"]!;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReponseModel result = _taskService.GetTask(email, pass);
            return StatusCode(result.statusCode, result);
        }

        [Route("CreateTask")]
        [HttpPost]
        public IActionResult CreateTask(TaskModel task)
        {
            string email = Request.Headers["email"]!;
            string pass = String.IsNullOrEmpty(Request.Headers["pass"]) ? "No aplica" : Request.Headers["pass"]!;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReponseModel result = _taskService.CreateTask(email, pass, task);
            return StatusCode(result.statusCode, result);
        }

        [Route("UpdateTask")]
        [HttpPost]
        public IActionResult UpdateTask(TaskModel task)
        {
            string email = Request.Headers["email"]!;
            string pass = String.IsNullOrEmpty(Request.Headers["pass"]) ? "No aplica" : Request.Headers["pass"]!;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReponseModel result = _taskService.UpdateTask(email, pass, task);
            return StatusCode(result.statusCode, result);
        }

        [Route("DeleteTask")]
        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            string email = Request.Headers["email"]!;
            string pass = String.IsNullOrEmpty(Request.Headers["pass"]) ? "No aplica" : Request.Headers["pass"]!;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ReponseModel result = _taskService.DeleteTask(email, pass, id);
            return StatusCode(result.statusCode, result);
        }

    }
}
