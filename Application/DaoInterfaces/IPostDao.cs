using FileData.DAOs;
using Models;
using Models.DTOs;
namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(Post post);

    Task<Post?> GetByIdAsync(int dtoId);
    Task DeleteAsync(int id);
}