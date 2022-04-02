using StoreAPI.Domain;
using StoreAPI.Dtos;
using System;

namespace Store.API.IntegrationTests.Products
{
    public static class ProductObjects
    {
        public static int Random = new Random().Next(1000,9999);
        public static string Name = "TestProduct";
        public static ProductWriteDto ProductWriteDto = new ProductWriteDto
        {
            Name = "TestProduct",
            Description = "Description Test",
            Price = 123.45
        };

        
        public static class Factory
        {
            public static ProductWriteDto GenerateProductWriteDto()
            {
                var random = new Random().Next(1000, 9999);
                var result = new ProductWriteDto
                {
                    Name = "TestProduct" + Random,
                    Description = "TestDescription" + Random,
                    Price = random * 1.0 / 100
                };

                return result;
            }

            public static Product GenerateProduct()
            {
                var random = new Random().Next(1000, 9999);
                var result = new Product
                {                    
                    Name = "TestProduct" + Random,
                    Description = "TestDescription" + Random,
                    Price = random * 1.0 / 100
                };

                return result;
            }



        }
    }
}