using StoreAPI.Dtos;
using System;

namespace Store.API.IntegrationTests.Roles
{
    public class RoleObjects
    {
        public static RoleWriteDto TestRoleWriteDto = new RoleWriteDto { Name = "TEST_ROLE" };

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
        }
    }
}