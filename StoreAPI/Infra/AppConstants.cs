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

        public struct Pagination
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

        public struct Validations
        {
            public struct User
            {
                public const int UsernameMaxLength = 50;
                public const int UsernameMinLength = 4;
                public const int PasswordMinLength = 4;
                public const int PasswordMaxLength = 50;
                public const int FirstNameMaxLength = 50;
                public const int LastNameMaxLength = 50;
                public const int MinimumAge = 18;
            }

            public struct Product
            {
                public const int NameMaxLength = 50;
                public const double PriceMinValue = 0;
                public const double PriceMaxValue = double.MaxValue;
                public const int DescriptionMaxLength = 200;
            }

            public struct Role
            {
                public const int NameMaxLength = 50;
            }

            public struct Stock
            {
                public const int QuantityMinValue = 0;
                public const int QuantityMaxValue = int.MaxValue;
            }
        }
    }
}