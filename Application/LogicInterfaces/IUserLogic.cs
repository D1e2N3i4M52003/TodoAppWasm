using Models;
using Models.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User?> CreateAsync(UserCreationDto userToCreate);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    public Task<User?> GetUserByUsername(string username);


}