// using Socialmedia.DTOs;
// using Hotel.Models;
// using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;
using Socialmedia.DTOs;
using Socialmedia.Models;
using Socialmedia.Repositories;

namespace Socialmedia.Controllers;

[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _User;
    // private readonly IScheduleRepository _schedule;
    private readonly IPostRepository _post;

    public UserController(ILogger<UserController> logger, IUserRepository User,IPostRepository Post)

    {
        _logger = logger;
        _User = User;
        _post= Post;
        // this._room = _room;
    }
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetList()
    {
    var res =await _User.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{user_id}")]

    public async Task<ActionResult> GetById([FromRoute] long user_id)
    {
        var User = await _User.GetById(user_id);
        if (User == null)
            return NotFound("No Product found with given employee number");
            var dto = User.asDto;
        // dto.Like = (await _like.GetListByUserId(user_id))
        //                 .Select(x => x.asDto).ToList();
        dto.Post = (await _post.GetListPostById(user_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateUser= new User
        {

           UserId=Data.UserId,
           UserName=Data.UserName,
           DateOfBirth=Data.DateOfBirth,
           Mobile=Data.Mobile,
           Email=Data.Email,
           Address=Data.Address,
           CreatedAt=Data.CreatedAt,  

        };
        var createdUser = await _User.Create(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);


    }

    [HttpPut("{user_id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] long user_id,
    [FromBody] UserUpdateDTO Data)
    {
        var existing = await _User.GetById(user_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateUser = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _User.Update(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    // [HttpDelete("{User_id}")]
    // public async Task<ActionResult> DeleteUser([FromRoute] long UserId)
    // {
    //     var existing = await _User.GetById(UserId);
    //     if (existing is null)
    //         return NotFound("No Product found with given employee number");
    //     await _User.Delete(UserId);
    //     return NoContent();
    // }
}