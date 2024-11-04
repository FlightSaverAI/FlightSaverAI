using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums;

namespace FlightSaverApi.Models.UserModel
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        [Required]
        public byte[] PasswordHash { get; set; } = null!;

        [Required]
        public byte[] PasswordSalt { get; set; } = null!;
    }
}

