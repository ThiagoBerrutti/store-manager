using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Helpers;
using StoreAPI.Persistence.Repositories;
using StoreAPI.TestUser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class TestAccountService : ITestAccountService
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestAccountService(IAuthService authService, IUserService userService, IRoleService roleService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<UserRegisterDto> GetRandomUser()
        {
            var randomUser = await FetchUser();
            if (randomUser is null)
            {
                return TestAccountUserRegisterFactory.Produce(); // fallback data
            }

            var userRegisterDto = _mapper.Map<UserRegisterDto>(randomUser);
            userRegisterDto.UserName = StringFormatter.RemoveAccents(userRegisterDto.FirstName) + RandomUserNameNumber();
            userRegisterDto.Password = "test";

            return userRegisterDto;
        }


        private async Task<RandomedUser> FetchUser()
        {
            var randomuserApiUrl = "https://randomuser.me/api/?nat=br&inc=name,dob";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponse = await client.GetAsync(randomuserApiUrl);

            var formatters = new List<MediaTypeFormatter>()
            {
                new JsonMediaTypeFormatter()
            };

            var response = await httpResponse.Content.ReadAsAsync<Response>(formatters);
            var user = response.Results[0];

            return user;
        }


        private static string RandomUserNameNumber()
        {
            var random = new Random();
            var number = random.Next(int.MaxValue).ToString();

            return number;
        }


        public async Task<AuthResponse> RegisterTestAcc(List<RolesEnum> roleId)
        {
            var userDto = await GetRandomUser();

            var registerResponse = await _authService.RegisterAsync(userDto);

            var userId = registerResponse.User.Id;
            var user = await _userService.GetByIdAsync(userId);

            foreach (int id in roleId)
            {
                var role = await _roleService.GetByIdAsync(id);
                user.Roles.Add(role);
            }

            await _unitOfWork.CompleteAsync();

            var authenticateResponse = await _authService.AuthenticateAsync(userDto);

            return authenticateResponse;
        }
    }
}