using Application.DaoInterfaces;
using Application.LogicInterfaces;
using FileData.DAOs;
using Models;
using Models.DTOs;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this._postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetById(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidatePost(dto);
        Post post = new Post(user, dto.Title,dto.Body);
        Post created = await _postDao.CreateAsync(post);
        return created;    
    }
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return _postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await _postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetById((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }
        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string body = dto.Body ?? existing.Body;
    
        Post updated = new (userToUse, titleToUse,body)
        {
            Id = existing.Id,
        };

        ValidatePost(updated);

        await _postDao.UpdateAsync(updated);

    }
    public async Task DeleteAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        await _postDao.DeleteAsync(id);
    }
    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new PostBasicDto(post.Id, post.Owner.UserName, post.Title, post.Body);
    }
    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}