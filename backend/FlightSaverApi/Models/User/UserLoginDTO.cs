using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums;

namespace FlightSaverApi.Models.User
{
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        
        [Required]
        public string Password { get; set; } = null!;
    }
}

