using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Core_Layer.DTOs;
using WebApi.Core_Layer.Identity;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class JwtService : IJwtService
    {
        /* Private fields */
        private readonly IConfiguration configuration;

        /* Constructor */
        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        /* Method to create Jkt token */
        public AuthenticationResponse CreateJwtToken(ApplicationUser applicationUser, string role)
        {
            /* Get the expiration time of JWT token */
            DateTime expiration = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:EXPIRATION_MINUTES"]));

            /* Create claim of the user */
            Claim[] claims = new Claim[] { 
                /* Mandatory claims */
                // Subject (user id)
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
                // JWT unique ID for per user
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // The time when the JWT Token is generated
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                // The role of the user
                new Claim(ClaimTypes.Role, role),           

                /* Optional claims */
                // Unique name identifier of the user, in this case we use email of the user
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Email),
                // Name of the user
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                // Email of the user
                new Claim(ClaimTypes.Email, applicationUser.Email)
            };

            /* Convert the secret key into array of bytes and get it in form of symmetric key*/
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            /* Define which the hashed algorithm used to generate the JWt token */
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            /* Create Jwt token generator */
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            /* return */
            return new AuthenticationResponse()
            {
                JwtToken = token,
                UserName = applicationUser.PersonName,
                Email = applicationUser.Email,
                ExpirationTime = expiration,
                Role = role,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["RefreshToken:EXPIRATION_MINUTES"])) // Coordinated Universal Time (UTC) is a time standard that is used to regulate clock and time zones around the world. All time zones are defined by their offset from UTC.
            };
        }

        /* Private method to create Refresh token */
        private string CreateRefreshToken()
        {
            byte[] bytes = new byte[64];

            // this generator is used to generate random number and fill them in array of bytes
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);

            // convert the array of bytes into base-64 string and return them
            return Convert.ToBase64String(bytes);
        }

        /* Method to Get the user claims (user detail) from Jwt Token */
        public ClaimsPrincipal GetClaimsPrincipalFromJwtToken(string jwtToken)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                // Validate the Audience
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                // Validate the Issuer
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                // Validate the Secret Key
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                // Don't validate the lifetime because this jwt token maybe aldready expired
                ValidateLifetime = false
            };

            // Extract the user's claims from the jwt token
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken securityToken);
            // if the jwtToken is valid, this securityToken will be a SecurityToken object and the jwtSecurityToken's header must contain the appropriate algorithm. Otherwise the jwt token is invalid  
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            else
            {
                return claimsPrincipal;
            }
        }
    }

}