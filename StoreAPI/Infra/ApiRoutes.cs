using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Infra
{
    public class ApiRoutes
    {        
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            public const string Register = Base + "/auth/register";
            public const string Authenticate = Base + "/auth/authenticate";
            public const string RegisterTestAcc = Base + "/auth/register/testAccount";
            public const string AuthenticateTestUser = Base + "/auth/authenticate/testAccount/{user}";
        }

        public static class Products
        {
            public const string GetAllProductsPaginated = Base + "/products";
            public const string GetProductById = Base + "/products/{id}";
            public const string CreateProduct = Base + "/products";
            public const string DeleteProduct = Base + "/products/{id}";
            public const string UpdateProduct = Base + "/products/{id}";
        }

        public static class Stock
        {
            public const string GetAllStocksPaginated = Base + "/stock";
            public const string GetStockByProductId = Base + "/stock/product/{productId}";
            public const string GetStockById = Base + "/stock/{id}";
            public const string UpdateStock = Base + "/stock/{id}";
            public const string AddQuantityToStock = Base + "/stock/{id}/add/{quantity}";
            public const string RemoveQuantityFromStock = Base + "/stock/{id}/remove/{quantity}";
        }

        public static class Roles
        {
            public const string GetAllRolesPaginated = Base + "/roles";
            public const string GetRoleById = Base + "/roles/{id:int}";
            public const string GetRoleByName = Base + "/roles/{roleName}";
            public const string GetUsersOnRole = Base + "/roles/{id}/users";
            public const string CreateRole = Base + "/roles";
            public const string DeleteRole = Base + "/roles/{id}";
        }

        public static class Users
        {
            public const string GetAllUsersPaginated = Base + "/users";
            public const string GetUserById = Base + "/users/{id}";
            public const string GetCurrentUser = Base + "/users/current";
            public const string GetRolesFromUser = Base + "/users/{id}/roles";
            public const string UpdateUser = Base + "/users/{id}";
            public const string UpdateCurrentUser = Base + "/users/current";
            public const string ChangePassword = Base + "/users/{id}/password";
            public const string ChangeCurrentUserPassword = Base + "/users/current/password";
            public const string ResetPassword = Base + "/users/{id}/password/reset";
            public const string AddUserToRole = Base + "/users/{id}/roles/add/{roleId}";
            public const string RemoveFromRole = Base + "/users/{id}/roles/remove/{roleId}";
        }
    }
}
