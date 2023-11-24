using System.ComponentModel.DataAnnotations;

namespace Models;

public class Post
{
    [Required]
    public int Id { get; set; }
    public User Owner { get; }
    public int OwnerId { get; }
    public string Title { get; }
    public string Body { get; set; }
    

    public Post(User owner, string title,string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
    public Post()
    {
    }
}