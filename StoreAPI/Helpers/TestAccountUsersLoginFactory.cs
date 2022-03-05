using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Infra;

namespace StoreAPI.Helpers
{
    public static class TestAccountUsersLoginFactory
    {
        public static UserLoginDto Generate(UserEnum userEnum)
        {
            var user = new UserLoginDto();

            switch (userEnum)
            {
                case UserEnum.Administrator:
                    {
                        user.UserName = AppConstants.Users.Admin.UserName;
                        user.Password = AppConstants.Users.Admin.Password;
                        break;
                    }

                case UserEnum.Manager:
                    {
                        user.UserName = AppConstants.Users.Manager.UserName;
                        user.Password = AppConstants.Users.Manager.Password;
                        break;
                    }

                case UserEnum.Stock:
                    {
                        user.UserName = AppConstants.Users.Stock.UserName;
                        user.Password = AppConstants.Users.Stock.Password;
                        break;
                    }

                case UserEnum.Seller:
                    {
                        user.UserName = AppConstants.Users.Seller.UserName;
                        user.Password = AppConstants.Users.Seller.Password;
                        break;
                    }

                case UserEnum.Public:
                    {
                        user.UserName = AppConstants.Users.Public.UserName;
                        user.Password = AppConstants.Users.Public.Password;
                        break;
                    }
            }

            return user;
        }
    }
}