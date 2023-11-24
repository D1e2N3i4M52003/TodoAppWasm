using Application.DaoInterfaces;
using Models;
using Models.DTOs;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(t => t.Id);
            postId++;
        }

        post.Id = postId;
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
        
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> posts = context.Posts.AsEnumerable();

        if (searchParameters.Username != null)
        {
            posts = context.Posts.Where(t => t.Owner.UserName.Contains(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }
        if (searchParameters.UserId != null)
        {
            posts = posts.Where(u => u.Owner.Id == searchParameters.UserId);
        }
        if (searchParameters.BodyContains != null)
        {
            posts = posts.Where(t => t.Body.Contains(searchParameters.BodyContains, StringComparison.OrdinalIgnoreCase));
        }
        if (searchParameters.TitleContains != null)
        {
            posts = posts.Where(t => t.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(posts);
    }


    public Task UpdateAsync(Post toUpdate)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"Post with id {toUpdate.Id} does not exist!");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(toUpdate);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }


    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.Id == postId);
        return Task.FromResult(existing);
    }
}