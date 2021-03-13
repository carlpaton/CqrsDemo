using CqrsDemo.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CqrsDemo.Api.Queries
{
    public static class GetTodoById
    {
        // Query
        public record Query(int Id) : IRequest<Response>;

        // Handler
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IRepository _repository;

            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = _repository.Select(request.Id);
                return (todo == null)
                    ? null
                    : new Response(todo.Id, todo.Name, todo.Completed);
            }
        }

        // Response
        public record Response(int Id, string Name, bool Completed);
    }
}
