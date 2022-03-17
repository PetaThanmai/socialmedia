// using Socialmedia.DTOs;
// using Hotel.Models;
// using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;
using Socialmedia.DTOs;
using Socialmedia.Models;
using Socialmedia.Repositories;

namespace Socialmedia.Controllers;

[ApiController]
[Route("api/Post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _Post;
    private readonly IHashRepository _hash;
    // private readonly IRoomRepository _room;

    public PostController(ILogger<PostController> logger, IPostRepository Post,IHashRepository Hash)

    {
        _logger = logger;
        _Post = Post;
        _hash = Hash;
        // this._room = _room;
    }
    [HttpGet]
    public async Task<ActionResult<List<PostDTO>>> GetList()
    {
    var res =await _Post.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{post_id}")]

    public async Task<ActionResult> GetById([FromRoute] long post_id)
    {
        var res = await _Post.GetById(post_id);
        if (res == null)
            return NotFound("No Product found with given employee number");
        var dto = res.asDto;
        dto.Hash = (await _hash.GetListByPostId(post_id))
                        .Select(x => x.asDto).ToList();
        // dto.Rooms = (await _room.GetListByPostId(Post_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreatePost= new Post
        {

           PostId=Data.PostId,
           PostType=Data.PostType,
           DateCreated=Data.DateCreated,
           DateUpdated=Data.DateUpdated,
           UserId=Data.UserId,
        
        };
        var createdPost = await _Post.Create(toCreatePost);

        return StatusCode(StatusCodes.Status201Created, createdPost.asDto);


    }

    [HttpPut("{post_id}")]
    public async Task<ActionResult> UpdatePost([FromRoute] long post_id,
    [FromBody] PostUpdateDTO Data)
    {
        var existing = await _Post.GetById(post_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdatePost = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Post.Update(toUpdatePost);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{post_id}")]
    public async Task<ActionResult> DeletePost([FromRoute] long post_id)
    {
        var existing = await _Post.GetById(post_id);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Post.Delete(post_id);
        return NoContent();
    }
}