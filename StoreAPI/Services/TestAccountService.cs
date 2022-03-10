using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Helpers;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Services.Communication;
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



        public async Task<ServiceResponse<UserRegisterDto>> GetRandomUser()
        {
            var randomUser = await FetchUser();
            if (randomUser is null)
            {
                var producedUser = TestAccountUserRegisterFactory.Produce(); // fallback data
                return new ServiceResponse<UserRegisterDto>(producedUser);
            }

            var userRegisterDto = _mapper.Map<UserRegisterDto>(randomUser);
            userRegisterDto.UserName = StringFormatter.RemoveAccents(userRegisterDto.FirstName) + RandomUserNameNumber();
            userRegisterDto.Password = "test";

            var result = new ServiceResponse<UserRegisterDto>(userRegisterDto);

            return result;
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


        public async Task<ServiceResponse<AuthResponse>> RegisterTestAcc(List<RolesEnum> roleId)
        {
            var randomUserResponse = await GetRandomUser();
            if (!randomUserResponse.Success)
            {
                return new ServiceResponse<AuthResponse>()
                    .HasFailed(randomUserResponse.Error)
                    .SetTitle("Error registering test account")
                    .SetDetail($"Error generating random user. Check {ServiceResponse.ErrorKey} for more details")
                    .AddToExtensionsErrors(randomUserResponse.Error);
            }

            var userDto = randomUserResponse.Data;

            var registerResponse = await _authService.RegisterAsync(userDto);
            if (!registerResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(registerResponse);
            }

            var registerAuthResponse = registerResponse.Data;

            var userId = registerAuthResponse.User.Id;
            var userResponse = await _userService.GetByIdAsync(userId);
            if (!userResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(userResponse);
            }

            var user = userResponse.Data;

            foreach (int id in roleId)
            {
                var roleResponse = await _roleService.GetByIdAsync(id);
                if (!roleResponse.Success)
                {
                    return new FailedServiceResponse<AuthResponse>(roleResponse);
                }

                var role = roleResponse.Data;

                user.Roles.Add(role);
            }

            await _unitOfWork.CompleteAsync();

            var authenticateResponse = await _authService.AuthenticateAsync(userDto);
            if (!authenticateResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(authenticateResponse.Error);
            }

            var resultData = authenticateResponse.Data;
            var result = new ServiceResponse<AuthResponse>(resultData);

            return result;
        }
    }
}