// using Socialmedia.DTOs;
// using Hotel.Models;
// using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;
using Socialmedia.DTOs;
using Socialmedia.Models;
using Socialmedia.Repositories;

namespace Socialmedia.Controllers;

[ApiController]
[Route("api/Like")]
public class LikeController : ControllerBase
{
    private readonly ILogger<LikeController> _logger;
    private readonly ILikeRepository _Like;
    // private readonly IScheduleRepository _schedule;
    // private readonly IRoomRepository _room;

    public LikeController(ILogger<LikeController> logger, ILikeRepository Like)

    {
        _logger = logger;
        _Like = Like;
        // _schedule = schedule;
        // this._room = _room;
    }
    [HttpGet]
    public async Task<ActionResult<List<LikeDTO>>> GetList()
    {
    var res =await _Like.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{like_id}")]

    public async Task<ActionResult> GetById([FromRoute] long like_id)
    {
        var res = await _Like.GetById(like_id);
        if (res == null)
            return NotFound("No Product found with given employee number");
            // var dto = res.asDto;
        // dto.Schedules = (await _schedule.GetListByLikeId(Like_id))
        //                 .Select(x => x.asDto).ToList();
        // dto.Rooms = (await _room.GetListByLikeId(Like_id)).Select(x => x.asDto).ToList();

        return Ok();
    }

    [HttpPost]

    public async Task<ActionResult<LikeDTO>> CreateLike([FromBody] CreateLikeDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateLike= new Like
        {

           LikeId=Data.LikeId,
           DateCreated=Data.DateCreated,
           UserId=Data.UserId,
           PostId=Data.PostId,
        };
        var createdLike = await _Like.Create(toCreateLike);

        return StatusCode(StatusCodes.Status201Created, createdLike.asDto);


    }

    [HttpPut("{like_id}")]
    public async Task<ActionResult> UpdateLike([FromRoute] long like_id,
    [FromBody] LikeUpdateDTO Data)
    {
        var existing = await _Like.GetById(like_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateLike = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Like.Update(toUpdateLike);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{like_id}")]
    public async Task<ActionResult> DeleteLike([FromRoute] long like_id)
    {
        var existing = await _Like.GetById(like_id);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Like.Delete(like_id);
        return NoContent();
    }
}