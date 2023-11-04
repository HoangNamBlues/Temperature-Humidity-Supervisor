using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Core_Layer.DTOs
{
    public class LoginRequest
    {
        /* Email */
        [EmailAddress(ErrorMessage = "Email should be in a proper Email format")]
        /* Check if the email is already used by using a controller named "Account" */
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already registered")]
        public string Email { get; set; } = string.Empty;

        /* Password */
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; } = string.Empty;
    }
}