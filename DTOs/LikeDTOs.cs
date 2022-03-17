using System.Text.Json.Serialization;
// using Socialmedia.Models;

namespace Socialmedia.DTOs;
public record LikeDTO
{
    [JsonPropertyName("like_id")]
    public  long LikeId { get; set; }

    [JsonPropertyName("date_created")]
    public DateTimeOffset DateCreated { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId{ get; set; }
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }
    
    
    
    // public List<RoomDTO> Rooms { get; set; }
}
    
    // public List<GuestDTO> Guest { get; set; }


public record CreateLikeDTO

{
 
    [JsonPropertyName("like_id")]
    public  long LikeId { get; set; }

    [JsonPropertyName("date_created")]
    public DateTimeOffset DateCreated { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId{ get; set; }
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }
        //  public List<ScheduleDTO> Schedule { get; set; }


}  


public record LikeUpdateDTO
{
 [JsonPropertyName("like_id")]
    public  long LikeId { get; set; }

    
}