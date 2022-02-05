using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Helpers

{
    public static class ApiRoutes
    {
        public const string Root = "http://localhost:5000/api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;


        public static class Auth
        {
            public const string Authenticate = Base + "/auth/authenticate";
            public const string Register = Base + "/auth/register";
        }

        public static class Product
        {
            public const string GetAllProducts = Base + "/product";
            public const string GetProductById = Base + "/product/{id:int}";
            public const string CreateProduct = Base + "/product";
            public const string DeleteProduct = Base + "/product/{id:int}";
            public const string UpdateProduct = Base + "/product/{id:int}";
        }

        public static class Role
        {
            public const string GetAllRoles = Base + "/role";
            public const string GetRoleById = Base + "/role/{id:int}";
            public const string GetRoleByName = Base + "/role/name";
            public const string GetUsersOnRole = Base + "/role/{id:int}/users";
            public const string SearchRole = Base + "/role/search";
            public const string CreateRole = Base + "/role";
            public const string DeleteRole = Base + "/role/{id:int}";
        }

        public static class Stock
        {
            public const string GetAllStocks = Base + "/stock";
            public const string GetStockByProductId = Base + "/stock/product/{productId:int}";
            public const string GetStockById = Base + "/stock/{id:int}";
            public const string UpdateStock = Base + "/stock/{id:int}";
            public const string AddToStock = Base + "/stock/{id:int}/add";
            public const string RemoveFromStock = Base + "/stock/{id:int}/add";
        }

        public static class User
        {
            public const string GetAllUsers = Base + "/user";
            public const string GetUserById = Base + "/user/{id:int}";
            public const string GetCurrentUser = Base + "/user";
            public const string GetUserByUserName = Base + "/user/name";
            public const string UpdateUser = Base + "/user";
            public const string ChangePassword = Base + "/user/{id:int}/password";
            public const string ChangeCurrentUserPassword = Base + "/user/current/password";
            public const string ResetPassword = Base + "/user/{id:int}/password";
            public const string AddUserToRole = Base + "/user/{id:int}/roles";
            public const string RemoveFromRole = Base + "/user/{id:int}/roles";
        }
    }
}
