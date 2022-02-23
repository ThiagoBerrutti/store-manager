﻿using System;

namespace StoreAPI.Dtos
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}