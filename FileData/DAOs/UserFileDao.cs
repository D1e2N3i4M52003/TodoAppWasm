using Application.DaoInterfaces;
using Models;
using Models.DTOs;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName));
        return Task.FromResult(user);
    }
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetById(int dtoOwnerId)
    {
        User? user = context.Users.First(u => u.Id == dtoOwnerId);
        return Task.FromResult(user);
    }
}
