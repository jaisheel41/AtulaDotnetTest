using System.ComponentModel.DataAnnotations;

namespace AtulaDotnetTest.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
