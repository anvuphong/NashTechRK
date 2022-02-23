using Assignment.DTO;
using Assignment.Entities;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoTaskController : Controller
    {
        private readonly ILogger<ToDoTaskController> _logger;
       private IToDoTaskService _service;

        public ToDoTaskController(ILogger<ToDoTaskController> logger, IToDoTaskService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("/AddTask")]
        public void AddTask([FromBody] DTOTask task)
        {
            _service.AddTask(task);
        }
        [HttpPost("/AddMultipleTask")]
        public void AddMultipleTask([FromBody] List<DTOTask> task)
        {
            _service.AddMultipleTask(task);
        }

        [HttpGet("/GetAllTask")]
        public List<ToDoTask> GetAllTask()
        {
            return _service.GetAllTask();
        }
        [HttpGet("/GetTaskByID")]
        public ToDoTask GetTaskById(int id)
        {
            return _service.GetToDoTaskById(id);
        }
        [HttpDelete("/DeleteTaskById")]
        public void DeleteTask(int id)
        {
            _service.DeleteTask(id);
        }
        [HttpPut("/UpdateTask")]
        public void UpdateTask([FromBody] ToDoTask task)
        {
            _service.UpdateTask(task);
        }
        [HttpDelete("/DeleteMultipleTask")]
        public void DeleteMultipleTask([FromBody] List<int> ids)
        {
            _service.DeleteMultipleTask(ids);
        }
    }
}