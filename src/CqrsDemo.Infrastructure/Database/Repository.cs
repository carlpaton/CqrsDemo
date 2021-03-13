using CqrsDemo.Domain.Interfaces;
using CqrsDemo.Domain.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CqrsDemo.Infrastructure.Database
{
    public class Repository : IRepository, IDisposable
    {
        private readonly SqliteConnection _dbConn;

        public Repository()
        {
            var connectionString = "Data Source=Todo.db;";
            _dbConn = new SqliteConnection(connectionString);
        }

        private void Open()
        {
            if (_dbConn.State == ConnectionState.Closed)
                _dbConn.Open();
        }

        public void Dispose()
        {
            _dbConn.Close();
            _dbConn.Dispose();
        }

        public int Insert(Todo obj)
        {
            var sql = @"
INSERT INTO Todo
(Name, Completed)
VALUES
(@Name, @Completed);
SELECT last_insert_rowid();";

            using (_dbConn)
            {
                Open();
                return _dbConn.ExecuteScalar<int>(sql, obj);
            }
        }

        public Todo Select(int id)
        {
            var sql = @"
SELECT * FROM Todo
WHERE
Id = @id;";

            using (_dbConn)
            {
                Open();
                return _dbConn.Query<Todo>(sql, new { id })
                    .FirstOrDefault();
            }
        }

        public void ExecuteNonQuery(string sql)
        {
            using (_dbConn)
            {
                Open();
                using var command = new SqliteCommand(sql, _dbConn);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            var sql = @"
DELETE FROM Todo
WHERE
Id = @id;";

            using (_dbConn)
            {
                Open();
                _dbConn.Execute(sql, new { id });
            }
        }

        public List<Todo> SelectList()
        {
            var sql = @"
SELECT * FROM Todo
ORDER BY Id;";

            using (_dbConn)
            {
                Open();
                return _dbConn.Query<Todo>(sql)
                    .ToList();
            }
        }

        public void Update(Todo obj)
        {
            var sql = @"
UPDATE Todo SET
Name = @Name, 
Completed = @Completed
WHERE Id = @Id;";

            using (_dbConn)
            {
                Open();
                _dbConn.Execute(sql, obj);
            }
        }
    }
}
