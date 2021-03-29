using CqrsDemo.Api.Commands;
using CqrsDemo.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CqrsDemo.Api.Controllers
{
    [Route("mediator/[controller]")]
    [ApiController]
    public class TodoItemsController2 : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController2(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("mediator/todoitems")]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            var command = new AddTodo.Command(todoItem.Name, todoItem.Completed);
            return Ok(await _mediator.Send(command));
        }

        //[HttpGet("/select-query/{id}")]
        //public async Task<IActionResult> SelectQuery(int id)
        //{
        //    var query = new GetTodoById.Query(id);
        //    var response = await _mediator.Send(query);
        //    return response == null ? NotFound() : Ok(response);
        //}
    }
}