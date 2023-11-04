using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Core_Layer.DTOs
{
    public class RegisterRequest
    {
        /* Email */
        [EmailAddress(ErrorMessage = "Email should be in a proper Email format")]
        /* Check if the email is already used by using a controller named "Account" */
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already registered")]
        public string Email { get; set; } = string.Empty;

        /* Phone number */
        [Required(ErrorMessage = "Phone number cannot be blank")]
        /* Make the require element just only contain digits */
        [RegularExpression("^[0-9]*$" ,ErrorMessage = "Phone number should contain digits only")]
        public string PhoneNumber { get; set; } = string.Empty;
            
        /* Username */
        [Required(ErrorMessage = "Person's Name cannot be blank")]
        public string PersonName { get; set; } = string.Empty;

        /* Password */
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; } = string.Empty;

        /* Confirm Password  */
        [Required(ErrorMessage = "Password cannot be blank")]
        /* Compare with the password above */
        [Compare("Password", ErrorMessage = "Password and Confirm Password must be similar")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /*Roles*/
        [Required(ErrorMessage = "Roles cannot be blank")]
        public string Role { get; set; }
    }
}