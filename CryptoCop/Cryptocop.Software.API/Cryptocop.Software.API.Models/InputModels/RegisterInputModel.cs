using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Need to provide password confirmation")]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string PasswordConfirmation { get; set; }
    }
}