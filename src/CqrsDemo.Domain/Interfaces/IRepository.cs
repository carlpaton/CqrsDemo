using CqrsDemo.Domain.Models;
using System.Collections.Generic;

namespace CqrsDemo.Domain.Interfaces
{
    public interface IRepository
    {
        void Update(Todo obj);
        void Delete(int id);
        void ExecuteNonQuery(string sql);
        int Insert(Todo obj);
        Todo Select(int id);
        List<Todo> SelectList();
    }
}
