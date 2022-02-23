using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.Identity;
using StoreAPI.Models;

namespace StoreAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductWriteDto, Product>().ReverseMap();
            CreateMap<ProductReadDto, Product>().ReverseMap();
            CreateMap<Product, ProductWithStockDto>()
                .ForMember(dto => dto.ProductStock, o => o.MapFrom(src => src.ProductStock));


            CreateMap<ProductStockReadDto, ProductStock>().ReverseMap();
            CreateMap<ProductStockWriteDto, ProductStock>().ReverseMap();

            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserAuthViewModel>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<UserRegisterDto, UserLoginDto>().ReverseMap();
            CreateMap<User, UserReadDto>()
                .ForMember(dto => dto.FullName, o => o.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dto => dto.Age, o => o.MapFrom(src => AgeCalculator.Calculate(src.DateOfbirth)));

            CreateMap<Role, RoleReadDto>().ReverseMap();
            CreateMap<Role, RoleWriteDto>().ReverseMap();
        }
    }
}