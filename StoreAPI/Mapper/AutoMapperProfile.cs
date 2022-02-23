using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.Identity;
using StoreAPI.Domain;

namespace StoreAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductWriteDto, Product>().ReverseMap();
            CreateMap<ProductReadDto, Product>()
                .ReverseMap();
            CreateMap<Product, ProductWithStockDto>().ReverseMap();

            CreateMap<PagedList<Product>, PagedList<ProductReadDto>>()
                .ConvertUsing<PagedListConverter<Product, ProductReadDto>>();

            CreateMap<ProductStockReadDto, ProductStock>().ReverseMap();
            CreateMap<ProductStockWriteDto, ProductStock>().ReverseMap();

            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserAuthDto>().ReverseMap();
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