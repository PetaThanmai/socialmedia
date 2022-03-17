// using Hotel.DTOs;
// using Socialmedia.DTOs;

using Socialmedia.DTOs;

namespace Socialmedia.Models;


public record User
{

    public  long UserId { get; set; }

    public string UserName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    
    public long Mobile { get; set; }
    public string Email { get; set; }
    
    
    public string Address { get; set; }
    public  DateTimeOffset CreatedAt { get; set; }
    
    public UserDTO asDto =>new UserDTO{
        UserId=UserId,
        UserName=UserName,
        DateOfBirth=DateOfBirth,
        Mobile=Mobile,
        Email=Email,
        Address=Address,
        CreatedAt=CreatedAt,
        
    };

 

}