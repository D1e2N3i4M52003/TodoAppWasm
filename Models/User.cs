using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{

    [Required]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public User() { }
}