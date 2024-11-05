using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.UserModel
{
    public class UserRegisterDTO
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public required string Password { get; set; }
    }
}

