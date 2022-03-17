using System.Text.Json.Serialization;
// using Socialmedia.Models;

namespace Socialmedia.DTOs;
public record HashDTO
{
    [JsonPropertyName("hash_id")]
    public  long HashId { get; set; }

    [JsonPropertyName("hash_name")]
    public string HashName { get; set; }
     public List<PostDTO> MyPost { get; set; }
    
    // public List<RoomDTO> Rooms { get; set; }
}
    
    // public List<GuestDTO> Guest { get; set; }


public record CreateHashDTO

{
 [JsonPropertyName("hash_id")]
    public long HashId { get; set; }

    [JsonPropertyName("hash_name")]
    public string HashName { get; set; }
   

        //  public List<ScheduleDTO> Schedule { get; set; }


}  


public record HashUpdateDTO
{
        [JsonPropertyName("hash_name")]
    public string HashName { get; set; }
    
}