using System;
using System.Collections.Generic;

namespace StoreAPI.Infra
{
    public class AppConstants
    {
        public struct Roles
        {
            public struct Admin
            {
                public const int Id = 1;
                public const string Name = "Administrator";
                public const string NormalizedName = "ADMINISTRATOR";
            };

            public struct Manager
            {
                public const int Id = 2;
                public const string Name = "Manager";
                public const string NormalizedName = "MANAGER";
            };

            public struct Stock
            {
                public const int Id = 3;
                public const string Name = "Stock";
                public const string NormalizedName = "STOCK";
            };

            public struct Seller
            {
                public const int Id = 4;
                public const string Name = "Seller";
                public const string NormalizedName = "SELLER";
            };
        }

        public struct Users
        {
            public struct Admin
            {
                public const int Id = 1;
                public const string UserName = "admin";
                public const string NormalizedUserName = "ADMIN";
                public const string FirstName = "Admin";
                public const string LastName = "Istrator";
                public const string Password = "admin";
                public const string DateOfBirth = "1980-1-1";

            };

            public struct Manager
            {
                public const int Id = 2;
                public const string UserName = "manager";
                public const string NormalizedUserName = "MANAGER";
                public const string FirstName = "Manager";
                public const string LastName = "Test Acc";
                public const string Password = "manager";
                public const string DateOfBirth = "1990-1-1";

            };

            public struct Stock
            {
                public const int Id = 3;
                public const string UserName = "stock";
                public const string NormalizedUserName = "STOCK";
                public const string FirstName = "Stock";
                public const string LastName = "Test Acc";
                public const string Password = "stock";
                public const string DateOfBirth = "1995-1-1";

            };

            public struct Seller
            {
                public const int Id = 4;
                public const string UserName = "seller";
                public const string NormalizedUserName = "SELLER";
                public const string FirstName = "Seller";
                public const string LastName = "Test Acc";
                public const string Password = "seller";
                public const string DateOfBirth = "2000-1-1";

            };

            public struct Public
            {
                public const int Id = 5;
                public const string UserName = "public";
                public const string NormalizedUserName = "PUBLIC";
                public const string FirstName = "Public";
                public const string LastName = "Test Acc";
                public const string Password = "public";
                public const string DateOfBirth = "2002-1-1";

            };


        }
    }
}