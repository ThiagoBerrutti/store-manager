using AutoMapper;
using SalesAPI.Dtos;
using SalesAPI.Identity;
using SalesAPI.Models;

namespace SalesAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductWriteDto, Product>().ReverseMap();
            CreateMap<ProductReadDto, Product>().ReverseMap();

            CreateMap<StockReadDto, ProductStock>().ReverseMap();
            CreateMap<StockWriteDto, ProductStock>().ReverseMap();

            CreateMap<UserLoginDto, User>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<User, UserViewModel>();
                
            CreateMap<Employee, EmployeeReadDto>().ReverseMap();
            CreateMap<Employee, EmployeeWriteDto>().ReverseMap();

            CreateMap<Role, RoleReadDto>().ReverseMap();
            CreateMap<Role, RoleWriteDto>().ReverseMap();
        }
    }
}
