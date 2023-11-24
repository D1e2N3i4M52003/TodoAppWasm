using Application.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IPostLogic _postLogic;
    public PostController(IPostLogic postLogic)
    {
        this._postLogic = postLogic;
    }
    [HttpPost,Authorize("MustBeUser")]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto)
    {
        try
        {
            Post? post = await _postLogic.CreateAsync(dto);
            return Created($"/post/{post.Id}", post);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet(), AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] string? bodyContains, [FromQuery] string? titleContains)
    {
        try
        {
            SearchPostParametersDto parameters = new(userName, userId, bodyContains, titleContains);
            var posts = await _postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPatch,Authorize("MustBeUser")]
    public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
    {
        try
        {
            await _postLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpDelete("{id:int}"),Authorize("MustBeUser")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _postLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    [HttpGet("{id:int}"), Authorize("MustBeUser")]
    public async Task<ActionResult<PostBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            PostBasicDto result = await _postLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}