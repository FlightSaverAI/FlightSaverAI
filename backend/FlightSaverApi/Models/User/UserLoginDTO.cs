using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.User
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

