using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.UserModel
{
    public class UserRegisterDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = null!;
    }
}

