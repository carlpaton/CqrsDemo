using CqrsDemo.Domain.Interfaces;
using CqrsDemo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CqrsDemo.Api.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IRepository _repository;

        public TodoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/selectlist")]
        public IActionResult Get()
        {
            var selectList = _repository.SelectList();

            return Ok(selectList);
        }

        [HttpPost("/insert")]
        public ActionResult<Todo> Insert([FromForm] Todo todo) 
        {
            var newTodoId = _repository.Insert(todo);
            var newTodo = _repository.Select(newTodoId);

            return Ok(newTodo);
        }

        [HttpPost("/select/{id}")]
        public ActionResult<Todo> Select(int id)
        {
            var todo = _repository.Select(id);

            return (todo == null) ? NotFound() : Ok(todo);
        }

        [HttpDelete("/delete/{id}")]
        public ActionResult<Todo> Delete(int id)
        {
            _repository.Delete(id);

            return Ok();
        }
    }
}
