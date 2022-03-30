using StoreAPI.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreAPI.Infra
{
    public static class AppConstants
    {
        public static class Roles
        {
            public static class Admin
            {
                public const int Id = 1;
                public const string Name = "Administrator";
                public const string NormalizedName = "ADMINISTRATOR";
            };

            public static class Manager
            {
                public const int Id = 2;
                public const string Name = "Manager";
                public const string NormalizedName = "MANAGER";
            };

            public static class Stock
            {
                public const int Id = 3;
                public const string Name = "Stock";
                public const string NormalizedName = "STOCK";
            };

            public static class Seller
            {
                public const int Id = 4;
                public const string Name = "Seller";
                public const string NormalizedName = "SELLER";
            };

            public const int Count = 4;
        }

        public static class Users
        {
            public static class Admin
            {
                public const int Id = 1;
                public const string UserName = "admin";
                public const string NormalizedUserName = "ADMIN";
                public const string FirstName = "Admin";
                public const string LastName = "Istrator";
                public const string Password = "admin";
                public const string DateOfBirth = "1980-1-1";
            };

            public static class Manager
            {
                public const int Id = 2;
                public const string UserName = "manager";
                public const string NormalizedUserName = "MANAGER";
                public const string FirstName = "Manager";
                public const string LastName = "Test Acc";
                public const string Password = "manager";
                public const string DateOfBirth = "1990-1-1";
            };

            public static class Stock
            {
                public const int Id = 3;
                public const string UserName = "stock";
                public const string NormalizedUserName = "STOCK";
                public const string FirstName = "Stock";
                public const string LastName = "Test Acc";
                public const string Password = "stock";
                public const string DateOfBirth = "1995-1-1";
            };

            public static class Seller
            {
                public const int Id = 4;
                public const string UserName = "seller";
                public const string NormalizedUserName = "SELLER";
                public const string FirstName = "Seller";
                public const string LastName = "Test Acc";
                public const string Password = "seller";
                public const string DateOfBirth = "2000-1-1";
            };

            public static class Public
            {
                public const int Id = 5;
                public const string UserName = "public";
                public const string NormalizedUserName = "PUBLIC";
                public const string FirstName = "Public";
                public const string LastName = "Test Acc";
                public const string Password = "public";
                public const string DateOfBirth = "2002-1-1";
            };


            public const int Count = 5;
        }

        public static class TestUser
        {
            public const string Password = "test";
        }

        public static class Pagination
        {
            public const int MaxPageSize = 50;
            public const int DefaultPageSize = 10;

            public const string MetadataHeaderDescription =
                "Pagination data on **JSON** format" +
                "\n\n" +
                "Example:" +
                "\n\n" +
                "{\"TotalCount\":30,\"PageSize\":10,\"CurrentPage\":1,\"TotalPages\":3,\"HasNext\":true,\"HasPrevious\":false}\n" +
                "- TotalCount: total number of items found\n" +
                "- CurrentPage: the page number that was received\n" +
                "- PageSize: number of items per page\n" +
                "- TotalPages: total number of pages\n" +
                "- HasPrevious: true if it has another page before that, false if it don't\n" +
                "- HasNext: true if it has another page after that, false if it don't\n";
        }

        public static class Validations
        {
            public static class User
            {
                public const int UsernameMaxLength = 50;
                public const int UsernameMinLength = 4;
                public const int PasswordMinLength = 4;
                public const int PasswordMaxLength = 50;
                public const int FirstNameMaxLength = 50;
                public const int LastNameMaxLength = 50;
                public const int MinimumAge = 18;
            }

            public static class Product
            {
                public const int NameMaxLength = 50;
                public const double PriceMinValue = 0;
                public const double PriceMaxValue = double.MaxValue;
                public const int DescriptionMaxLength = 200;
            }

            public static class Role
            {
                public const int NameMaxLength = 50;
            }

            public static class Stock
            {
                public const int QuantityMinValue = 0;
                public const int QuantityMaxValue = int.MaxValue;
            }
        }
    }
}