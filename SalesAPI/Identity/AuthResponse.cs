using Microsoft.AspNetCore.Identity;
using SalesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Models
{
    public class AuthResponse<T> where T : class
    {
        public UserViewModel User { get; set; }
        public string Token { get; set; }
        public T Result { get; set; }

        public bool Succeeded
        {
            get
            {
                if (Result is SignInResult signInResult)
                {
                    return signInResult.Succeeded;
                }

                if (Result is IdentityResult identityResult)
                {
                    return identityResult.Succeeded;
                }

                return false;
            }
        }
        

        public AuthResponse()
        {
        }

        public AuthResponse(T result)
        {
            Result = result;
            User = null;
            Token = string.Empty;
        }

        public AuthResponse(UserViewModel user, string token, T result)
        {
            User = user;
            Token = token;
            Result = result;
        }

        
    }
}
