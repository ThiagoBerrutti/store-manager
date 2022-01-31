using SalesAPI.Dtos;
using SalesAPI.Models;
using SalesAPI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Persistence.Data
{
    public class ProductSeed
    {
        private readonly SalesDbContext _context;
        private readonly IProductService _productService;


        public ProductSeed(SalesDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task Seed()
        {
            if (!_context.Products.Any())
            {

                await _productService.CreateAsync(new ProductWriteDto { Name = "Abacate", Price = 9.99 });//).Wait();
                await _productService.CreateAsync(new ProductWriteDto { Name = "Berinjela", Price = 3.00 });//).Wait();
                await _productService.CreateAsync(new ProductWriteDto { Name = "Coco", Price = 5.50 });//).Wait();
                await _productService.CreateAsync(new ProductWriteDto { Name = "Danoninho", Price = 6.00 });//).Wait();
                await _productService.CreateAsync(new ProductWriteDto { Name = "Espaguete", Price = 4.00 });//).Wait();
            }

            //if (!_context.EmployeeRoles.Any())
            //{
            //    _context.EmployeeRoles.AddRange(
            //        new Role { Name = "Manager" },
            //        new Role { Name = "Seller" },
            //        new Role { Name = "Trainee" },
            //        new Role { Name = "Slave" }
            //        );

            //    await _context.SaveChangesAsync();
            //}

            if (!_context.Employees.Any())
            {
                _context.Employees.AddRange(
                    new Employee { BaseSalary = 20000, Name = "Alex Almeida" },
                    new Employee { BaseSalary = 2000, Name = "Bruno Bertoli" },
                    new Employee { BaseSalary = 2000, Name = "Carlos Costa" },
                    new Employee { BaseSalary = 2000, Name = "Diego Dias" },
                    new Employee { BaseSalary = 500, Name = "Ester E." },
                    new Employee { BaseSalary = 500, Name = "Fernando Franco" },
                    new Employee { BaseSalary = 500, Name = "Gustavo Gusmão" },
                    new Employee { BaseSalary = 0, Name = "Escravo 1" },
                    new Employee { BaseSalary = 0, Name = "Escravo 2" },
                    new Employee { BaseSalary = 0, Name = "Escravo 3" },
                    new Employee { BaseSalary = 0, Name = "Escravo 4" }
                );

                await _context.SaveChangesAsync();

            }

        }
    }
}