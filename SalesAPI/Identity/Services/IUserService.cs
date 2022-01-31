﻿using Microsoft.AspNetCore.Identity;
using SalesAPI.Dtos;
using SalesAPI.Models;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IUserService
    {
        public Task<AuthResponse<SignInResult>> Login(UserLoginDto userLogin);
        public Task<AuthResponse<IdentityResult>> Register(UserRegisterDto userDto);
        public Task<UserRegisterDto> GetUserByUserName(string userName);

    }
}