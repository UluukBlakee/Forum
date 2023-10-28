using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Forum.Models
{
    public class User : IdentityUser<int>
    {
        public string? Avatar { get; set; }
    }
}
