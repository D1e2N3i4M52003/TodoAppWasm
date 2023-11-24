namespace Models.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public string? BodyContains { get;}
    public string? TitleContains { get;}

    public SearchPostParametersDto(string? username, int? userId, string? bodyContains, string? titleContains)
    {
        Username = username;
        UserId = userId;
        BodyContains = bodyContains;
        TitleContains = titleContains;
    }
}