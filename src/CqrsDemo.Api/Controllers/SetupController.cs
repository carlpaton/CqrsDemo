using CqrsDemo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CqrsDemo.Api.Controllers
{
    [ApiController]
    public class SetupController : Controller
    {
        private readonly IRepository _repository;

        public SetupController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/setup-database")]
        public IActionResult CreateTables()
        {
            var sql = @"
DROP TABLE IF EXISTS Todo; 
CREATE TABLE Todo (Id INTEGER PRIMARY KEY, Name TEXT, Completed BOOL)";
            _repository.ExecuteNonQuery(sql);

            return Ok("Tables created in `Todo.db`. " + DateTime.Now);
        }
    }
}
