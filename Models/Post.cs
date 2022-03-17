// using Hotel.DTOs;
// using Socialmedia.DTOs;

using Socialmedia.DTOs;

namespace Socialmedia.Models;


public record Post
{
    public long PostId { get; set; }
    
    public string  PostType { get; set; }
    public DateTimeOffset DateCreated  { get; set; }
    public DateTimeOffset DateUpdated { get; set; }
    public long UserId { get; set; }
    public PostDTO asDto =>new PostDTO{
        PostId=PostId,
        PostType=PostType,
        DateCreated=DateCreated,
        DateUpdated=DateUpdated,
        UserId=UserId

        
    };

 

}