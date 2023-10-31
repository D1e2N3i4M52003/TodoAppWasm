namespace Models.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    public string Password { get; set; }

    public UserCreationDto(string userName)
    {
        UserName = userName;
    }
}