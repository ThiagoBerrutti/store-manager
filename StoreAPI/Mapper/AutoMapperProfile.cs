using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Helpers;
using StoreAPI.Identity;
using StoreAPI.Domain;
using StoreAPI.TestUser;

namespace StoreAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductWriteDto, Product>().ReverseMap();
            CreateMap<ProductReadDto, Product>()
                .ReverseMap();
            CreateMap<Product, ProductReadWithStockDto>().ReverseMap();

            CreateMap<PaginatedList<Product>, PaginatedList<ProductReadDto>>();
            CreateMap<PaginatedList<Role>, PaginatedList<RoleReadDto>>();
            CreateMap<PaginatedList<User>, PaginatedList<UserReadDto>>();
            CreateMap<PaginatedList<ProductStock>, PaginatedList<ProductStockReadDto>>();

            CreateMap<ProductStockReadDto, ProductStock>().ReverseMap();
            CreateMap<ProductStockWriteDto, ProductStock>().ReverseMap();

            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserAuthDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<UserRegisterDto, UserLoginDto>().ReverseMap();
            CreateMap<User, UserReadDto>()
                .ForMember(dto => dto.FullName, o => o.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dto => dto.Age, o => o.MapFrom(src => AgeCalculator.Calculate(src.DateOfBirth)));
            CreateMap<User, UserDetailedReadDto>()
                .ForMember(dto => dto.Age, o => o.MapFrom(src => AgeCalculator.Calculate(src.DateOfBirth)));

            CreateMap<Role, RoleReadDto>().ReverseMap();
            CreateMap<Role, RoleWriteDto>().ReverseMap();

            CreateMap<RandomedUser, UserRegisterDto>()
                .ForMember(dest => dest.DateOfBirth, o => o.MapFrom(src => src.Dob.Date))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.Name.First))
                .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.Name.Last));

        }
    }
}