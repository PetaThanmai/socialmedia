// using Hotel.DTOs;
// using Socialmedia.DTOs;

using Socialmedia.DTOs;

namespace Socialmedia.Models;


public record Hash
{
    public long HashId { get; set; }
    public string HashName { get; set; }
    
    public HashDTO asDto =>new HashDTO{
        HashId=HashId,
        HashName=HashName,
        
    };

 

}