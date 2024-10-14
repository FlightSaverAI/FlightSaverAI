using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.User
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public byte[] PasswordHash { get; set; } = null!;

        [Required]
        public byte[] PasswordSalt { get; set; } = null!;
    }
}

