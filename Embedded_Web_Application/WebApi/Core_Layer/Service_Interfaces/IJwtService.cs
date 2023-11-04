using System.Security.Claims;
using WebApi.Core_Layer.DTOs;
using WebApi.Core_Layer.Identity;

namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface IJwtService
    {
        /* Method to create Jwt token from the ApplicationUser object */
        AuthenticationResponse CreateJwtToken(ApplicationUser applicationUser, string role);

        /* Method to get the user detail (User Id, User Email, ...) from the JWT token */
        ClaimsPrincipal GetClaimsPrincipalFromJwtToken(string jwtToken);
    }
}