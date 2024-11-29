using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums;

namespace FlightSaverApi.Models.UserModel
{
    public class UserLoginDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

