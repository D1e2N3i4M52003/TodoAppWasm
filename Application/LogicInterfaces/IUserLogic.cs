using Models;
using Models.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User?> CreateAsync(UserCreationDto userToCreate);

}