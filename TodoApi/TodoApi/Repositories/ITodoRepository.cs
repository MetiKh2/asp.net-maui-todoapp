using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> Get(int id);
        Task<List<Todo>> GetAll();
        Task<Todo> Add(Todo company);
        Task<Todo> Update(Todo company);
        Task Remove(int id);
    }
}
