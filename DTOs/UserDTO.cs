using System.Text.Json.Serialization;
// using Socialmedia.Models;

namespace Socialmedia.DTOs;
public record UserDTO
{
    [JsonPropertyName("user_id")]
    public  long UserId { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }
    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }
    
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("created_at")]
    public  DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("post")]
         public List<PostDTO> Post { get; set; }
    
    
    
    

    // public List<RoomDTO> Rooms { get; set; }
}
    
    // public List<GuestDTO> Guest { get; set; }


public record CreateUserDTO

{
 [JsonPropertyName("user_id")]
    public  long UserId { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }
    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }
    
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("created_at")]
    public  DateTimeOffset CreatedAt { get; set; }
    
    


}  


public record UserUpdateDTO
{
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    
    [JsonPropertyName("address")]
    public string Address { get; set; }     
    
}