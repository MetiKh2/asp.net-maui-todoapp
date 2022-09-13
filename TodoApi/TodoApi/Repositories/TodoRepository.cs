using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private IDbConnection db;

        public TodoRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<Todo> Add(Todo todo)
        {
            var result =await db.QueryAsync<int>("Insert into Todos (Title,Description,CreateDate,IsDone) Values (@Title,@Description,@CreateDate,@IsDone);"+
                "select CAST(SCOPE_IDENTITY() as int)",todo);
            todo.Id = result.Single();
            return todo;
        }

        public async Task<Todo> Get(int id)
        {
            var todos = await db.QueryAsync<Todo>("Select * from Todos where Id=@id",new { id});
            return todos.FirstOrDefault();
        }

        public async Task<List<Todo>> GetAll()
        {
            var todos = await db.QueryAsync<Todo>("Select * from Todos");
            return todos.ToList();
        }

        public async Task Remove(int id)
        {
           await db.ExecuteAsync("DELETE FROM Todos WHERE Id = @Id", new { id });
        }

        public async Task<Todo> Update(Todo todo)
        {
              await db.ExecuteAsync("Update Todos Set Title=@Title,Description=@Description,CreateDate=@CreateDate,IsDone=@IsDone where Id=@Id"
               , todo);
            return todo;
        }
    }
}
