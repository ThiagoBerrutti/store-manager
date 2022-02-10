namespace SalesAPI
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
            };

            public struct Manager
            {
                public const int Id = 2;
                public const string UserName = "manager";
                public const string NormalizedUserName = "MANAGER";
                public const string FirstName = "Manager";
                public const string LastName = "Test Acc";
            };

            public struct Stock
            {
                public const int Id = 3;
                public const string UserName = "stock";
                public const string NormalizedUserName = "STOCK";
                public const string FirstName = "Stock";
                public const string LastName = "Test Acc";
            };

            public struct Seller
            {
                public const int Id = 4;
                public const string UserName = "seller";
                public const string NormalizedUserName = "SELLER";
                public const string FirstName = "Seller";
                public const string LastName = "Test Acc";
            };
        }
    }
}