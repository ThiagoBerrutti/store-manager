using StoreAPI.Dtos;
using StoreAPI.Identity;
using System;

namespace Store.API.IntegrationTests.Roles
{
    public class RoleObjects
    {
        public static class Factory
        {
            public static RoleWriteDto GenerateRoleWriteDto()
            {
                var random = new Random().Next(1000, 9999);
                var result = new RoleWriteDto
                {
                    Name = "TestRole" + random
                };

                return result;
            }


            public static Role GenerateRole()
            {
                var random = new Random().Next(1000, 9999);
                var result = new Role
                {
                    Name = "TestRole" + random
                };

                result.NormalizedName = result.Name.ToUpper();

                return result;
            }
        }
    }
}