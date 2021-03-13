using CqrsDemo.Api.Commands;
using CqrsDemo.Api.Queries;
using CqrsDemo.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CqrsDemo.Api.Controllers
{
    [ApiController]
    public class ToDoWithMediatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoWithMediatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/insert-command")]
        public async Task<IActionResult> InsertCommand([FromForm] Todo todo)
        {
            var command = new AddTodo.Command(todo.Name, todo.Completed);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("/select-query/{id}")]
        public async Task<IActionResult> SelectQuery(int id)
        {
            var query = new GetTodoById.Query(id);
            var response = await _mediator.Send(query);
            return response == null ? NotFound() : Ok(response);
        }
    }
}
