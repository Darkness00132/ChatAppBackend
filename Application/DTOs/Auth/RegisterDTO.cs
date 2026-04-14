using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
