﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FlightSaverApi.Models.UserModel
{
    public class UserRegisterDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

