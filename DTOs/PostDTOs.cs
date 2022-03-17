using System.Text.Json.Serialization;
// using Socialmedia.Models;

namespace Socialmedia.DTOs;
public record PostDTO
{
    [JsonPropertyName("post_id")]
    public  long PostId { get; set; }

    [JsonPropertyName("post_type")]
    public string PostType { get; set; }
    
    [JsonPropertyName("date_created")]
    public DateTimeOffset DateCreated { get; set; }
    [JsonPropertyName("date_updated")]
    public DateTimeOffset DateUpdated { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
     public List<HashDTO> Hash { get; set; }
    
   
}
    
    


public record CreatePostDTO

{
 [JsonPropertyName("post_id")]
    public  long PostId { get; set; }

    [JsonPropertyName("post_type")]
    public string PostType { get; set; }
    
    [JsonPropertyName("date_created")]
    public DateTimeOffset DateCreated { get; set; }
    [JsonPropertyName("date_updated")]
    public DateTimeOffset DateUpdated { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    

        //  public List<ScheduleDTO> Schedule { get; set; }


}  


public record PostUpdateDTO
{
      [JsonPropertyName("post_type")]
    public string PostType { get; set; }
    
}