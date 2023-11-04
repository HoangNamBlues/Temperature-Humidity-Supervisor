namespace WebApi.Core_Layer.DTOs
{
    public class AuthenticationResponse
    {
        public string JwtToken { get; set; } 
        public string UserName { get; set;}
        public string Email { get; set; }
        public string MyProperty { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
        public string Role { get; set; }
    }
}