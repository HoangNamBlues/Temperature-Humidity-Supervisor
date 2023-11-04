using Microsoft.AspNetCore.Identity;

namespace WebApi.Core_Layer.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PersonName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
} 