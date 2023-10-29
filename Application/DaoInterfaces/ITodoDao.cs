using FileData.DAOs;
using Models;
using Models.DTOs;
namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
    Task UpdateAsync(Todo todo);

    Task<Todo?> GetByIdAsync(int dtoId);
    Task DeleteAsync(int id);
}