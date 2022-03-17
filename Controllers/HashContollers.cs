// using Socialmedia.DTOs;
// using Hotel.Models;
// using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;
using Socialmedia.DTOs;
using Socialmedia.Models;
using Socialmedia.Repositories;

namespace Socialmedia.Controllers;

[ApiController]
[Route("api/hash")]
public class HashController : ControllerBase
{
    private readonly ILogger<HashController> _logger;
    private readonly IHashRepository _hash;
    private readonly IPostRepository _post;
    // private readonly IRoomRepository _room;

    public HashController(ILogger<HashController> logger, IHashRepository Hash,IPostRepository post)

    {
        _logger = logger;
        _hash = Hash;
        _post=post;
        // _schedule = schedule;
        // this._room = _room;
    }
    [HttpGet]
    public async Task<ActionResult<List<HashDTO>>> GetList()
    {
    var res =await _hash.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{hash_id}")]

    public async Task<ActionResult> GetById([FromRoute] long hash_id)
    {
        var res = await _hash.GetById(hash_id);
        if (res == null)
            return NotFound("No Product found with given employee number");
        var dto = res.asDto;
        dto.MyPost = (await _post.GetPostsByHashId(hash_id))
                        .Select(x => x.asDto).ToList();
        // dto.Rooms = (await _room.GetListByHashId(Hash_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<HashDTO>> CreateHash([FromBody] CreateHashDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateHash= new Hash
        {

           HashId=Data.HashId,
           HashName=Data.HashName,
           
        };
        var createdHash = await _hash.Create(toCreateHash);

        return StatusCode(StatusCodes.Status201Created, createdHash.asDto);


    }

    [HttpPut("{hash_id}")]
    public async Task<ActionResult> UpdateHash([FromRoute] long hash_id,
    [FromBody] HashUpdateDTO Data)
    {
        var existing = await _hash.GetById(hash_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateHash = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _hash.Update(toUpdateHash);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{hash_id}")]
    public async Task<ActionResult> DeleteHash([FromRoute] long hash_id)
    {
        var existing = await _hash.GetById(hash_id);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _hash.Delete(hash_id);
        return NoContent();
    }
}