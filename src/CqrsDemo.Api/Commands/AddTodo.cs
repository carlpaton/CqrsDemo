using CqrsDemo.Domain.Interfaces;
using CqrsDemo.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CqrsDemo.Api.Commands
{
    public static class AddTodo
    {
        // Command
        public record Command(string Name, bool Completed) : IRequest<Response>;

        // Handler
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository _repository;

            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var todo = new Todo()
                {
                    Name = request.Name,
                    Completed = request.Completed
                };

                var newTodoId = _repository.Insert(todo);
                var newTodo = _repository.Select(newTodoId);

                return new Response(newTodo.Id, newTodo.Name, newTodo.Completed);
            }
        }

        // Response
        public record Response(int Id, string Name, bool Completed);
    }
}
