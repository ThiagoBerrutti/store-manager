using AutoMapper;
using SalesAPI.Dtos;
using SalesAPI.Models;

namespace SalesAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserLoginDto, User>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<User, UserViewModel>()
                .ForMember(dto => dto.Roles, o => o.MapFrom(src => src.Roles));
                

            CreateMap<Employee, EmployeeReadDto>().ReverseMap();
            CreateMap<Employee, EmployeeWriteDto>().ReverseMap();

            CreateMap<Role, RoleReadDto>().ReverseMap();
            CreateMap<Role, RoleWriteDto>().ReverseMap();
        }
    }
}
