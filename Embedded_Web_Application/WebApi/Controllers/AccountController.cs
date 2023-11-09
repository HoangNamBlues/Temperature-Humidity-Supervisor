using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core_Layer.DTOs;
using WebApi.Core_Layer.Identity;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        /* Private fields */
        /* This user manager is used to add/remove/... a user */
        private readonly UserManager<ApplicationUser> userManager;
        /* This role manager is used to manipulate the role of a user */
        public readonly RoleManager<ApplicationRole> roleManager;
        /* This sign in manager is used to sign in and sign a user */
        private readonly SignInManager<ApplicationUser> signInManager;
        /* This jwtService is used to generate JWT token */
        private readonly IJwtService jwtService;

        /* Constructor */
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
        {
            this.jwtService = jwtService;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        /* Action method to check if the email is already used */
        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser applicationUser = await userManager.FindByEmailAsync(email);
            if (applicationUser == null)
            {
                return Ok("false");
            }
            else
            {
                return Ok("true");
            }
        }

        /* Action to register a new user */
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ApplicationUser>> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            /* Validation */
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            /* Create user */
            ApplicationUser applicationUser = new()
            {
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.Email,
                PersonName = registerRequest.PersonName
            };
            IdentityResult result = await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {
                /* Add roles to the user */
                var resultRole = await userManager.AddToRoleAsync(applicationUser, registerRequest.Role);
                if (!resultRole.Succeeded)
                    return BadRequest("Failed to add role to the user");

                /* Sign in */
                await signInManager.SignInAsync(applicationUser, isPersistent: false);

                /* Create Jwt token (concurrently create Refresh Token) and insert role for the user, then filling them in the response to the user */
                AuthenticationResponse authenticationResponse = jwtService.CreateJwtToken(applicationUser, registerRequest.Role);

                /* Fill the created Refresh token in ApplicationUser object and update it to database */
                applicationUser.RefreshToken = authenticationResponse.RefreshToken;
                applicationUser.RefreshTokenExpirationTime = authenticationResponse.RefreshTokenExpirationTime;
                await userManager.UpdateAsync(applicationUser);

                return Ok(authenticationResponse);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description)); // error 1 | error 2 | ...
                return BadRequest(errorMessage);
            }
        }

        /* Action to login */
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequest loginRequest)
        {
            /* Create an object for ApplicationUser used for generating JWT token */
            ApplicationUser applicationUser = new ApplicationUser();

            /* Validation */
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            /* Login the user */
            /* lockoutOnFailure property for disable the user from login if he has many login failures */
            var result = await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                applicationUser = await userManager.FindByEmailAsync(loginRequest.Email);
            }

            if (applicationUser == null)
                return NoContent();
            else
            {
                // Get roles from user
                IEnumerable<string> roles = await userManager.GetRolesAsync(applicationUser);

                /* Create Jwt token (concurrently create Refresh Token) and insert role for the user, then filling them in the response to the user */
                AuthenticationResponse authenticationResponse = jwtService.CreateJwtToken(applicationUser, roles.ToList()[0]);

                /* Fill the created Refresh token in ApplicationUser object */
                applicationUser.RefreshToken = authenticationResponse.RefreshToken;
                applicationUser.RefreshTokenExpirationTime = authenticationResponse.RefreshTokenExpirationTime;
                /* Update Refresh token to database */
                await userManager.UpdateAsync(applicationUser);

                return Ok(authenticationResponse);
            }
        }

        /* Action to log out */
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> UserLogout()
        {
            await signInManager.SignOutAsync();
            Response.Headers.Remove("Authorization");
            return NoContent();
        }

        /* Action to delete a user from Identity Database by the user's email */
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string userEmail) {
            if (userEmail == null)
                return BadRequest("Please enter the User's email in the request");
            ApplicationUser user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return NotFound("The user is not found");
            var result = await userManager.DeleteAsync(user);
            return Ok(result);
        }

        /* Action to generate a new jwt token from the refresh token */
        [HttpPost]
        [Route("Generate-New-Jwt-Token")]
        public async Task<IActionResult> GenerateNewJwtToken(TokenModelRequest tokenModelRequest)
        {
            if (tokenModelRequest == null)
            {
                return BadRequest("Invalid Client Request");
            }

            string jwtToken = tokenModelRequest.JwtToken;
            string refreshToken = tokenModelRequest.RefreshToken;

            ClaimsPrincipal claimsPrincipal = jwtService.GetClaimsPrincipalFromJwtToken(jwtToken);
            if (claimsPrincipal == null)
            {
                return BadRequest("Invalid Jwt Token");
            }

            // Get the user's email from extracted data
            string email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);   
            // Find the user by email
            ApplicationUser user = await userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != tokenModelRequest.RefreshToken || user.RefreshTokenExpirationTime <= DateTime.Now)
            {
                return BadRequest("Invalid Refresh Token");
            }

            // Get roles from user
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);
            // Create a new Jwt token
            AuthenticationResponse authenticationResponse = jwtService.CreateJwtToken(user, roles.ToList()[0]);

            // Udate the refresh token
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationTime = authenticationResponse.RefreshTokenExpirationTime;

            // Update the user's claims
            await userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }
    }
}