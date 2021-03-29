using CqrsDemo.Api.Models;
using CqrsDemo.Domain.Interfaces;
using CqrsDemo.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CqrsDemo.Api.Controllers
{
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IRepository _repository;

        public TodoItemsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/todoitems/{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            var todo = _repository.Select(id);
            if (todo == null) 
            {
                return NotFound();
            }
            
            return Ok(Map(todo));
        }

        [HttpGet("/todoitems")]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            var todos = _repository.SelectList();

            return Ok(Map(todos));
        }

        [HttpPost("/todoitems")]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem) 
        {
            var id = _repository.Insert(Map(todoItem));
            var newTodo = _repository.Select(id);

            return CreatedAtAction(nameof(GetTodoItem), new { id }, todoItem);
        }

        [HttpDelete("/todoitems/{id}")]
        public ActionResult<TodoItem> DeleteTodoItem(int id)
        {
            var todo = _repository.Select(id);
            if (todo == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return Ok(Map(todo));
        }

        [HttpPut("/todoitems/{id}")]
        public IActionResult PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _repository.Update(Map(todoItem));

            return NoContent();
        }

        private static TodoItem Map(Todo todo)
        {
            return new TodoItem()
            {
                Id = todo.Id,
                Name = todo.Name,
                Completed = todo.Completed
            };
        }

        private static Todo Map(TodoItem todoItem)
        {
            return new Todo()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                Completed = todoItem.Completed
            };
        }

        private static IEnumerable<TodoItem> Map(IEnumerable<Todo> todoItems)
        {
            var todos = new List<TodoItem>();

            foreach (var todoItem in todoItems)
            {
                todos.Add(new TodoItem() { 
                    Id = todoItem.Id,
                    Name = todoItem.Name,
                    Completed = todoItem.Completed
                });
            }

            return todos;
        }
    }
}
