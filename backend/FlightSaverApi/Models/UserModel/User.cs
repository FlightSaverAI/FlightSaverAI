﻿using System;
using System.ComponentModel.DataAnnotations;
using FlightSaverApi.Enums;
using FlightSaverApi.Models.ReviewModel;

namespace FlightSaverApi.Models.UserModel
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}

