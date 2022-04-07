using AutoMapper;
using StoreAPI.Dtos;
using StoreAPI.Enums;
using StoreAPI.Helpers;
using StoreAPI.Infra;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Services.Communication;
using StoreAPI.TestUser;
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
        protected readonly IMapper _mapper;

        public TestAccountService()
        { }

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
            var digits = 5;

            var fetchResponse = await FetchUser();
            if (fetchResponse is FailedServiceResponse<RandomedUser>)
            {
                var producedUser = TestAccountUserRegisterFactory.Produce(digits); // fallback data
                return new ServiceResponse<UserRegisterDto>(producedUser);
            }

            var randomUser = fetchResponse.Data;

            var userRegisterDto = _mapper.Map<UserRegisterDto>(randomUser);
            userRegisterDto.UserName = StringFormatter.RemoveAccents(userRegisterDto.FirstName).ToLower() + TestAccountUserRegisterFactory.RandomUserNameNumber(digits);
            userRegisterDto.Password = AppConstants.TestUser.Password;

            var result = new ServiceResponse<UserRegisterDto>(userRegisterDto);

            return result;
        }


        public virtual async Task<ServiceResponse<RandomedUser>> FetchUser()
        {
            var randomuserApiUrl = "https://randomuser.me/api/?nat=br&inc=name,dob";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponse = await client.GetAsync(randomuserApiUrl);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return new FailedServiceResponse<RandomedUser>()
                    .SetTitle("Error fetching random user data")
                    .SetErrorData(httpResponse)
                    .SetDetail($"A failed status code response ({httpResponse.StatusCode}) was received when trying to fetch random user data. Reason phrase: {httpResponse.ReasonPhrase}");
            }

            var formatters = new List<MediaTypeFormatter>
            {
                new JsonMediaTypeFormatter()
            };

            var response = await httpResponse.Content.ReadAsAsync<Response>(formatters);
            var user = response.Results[0];

            if (user is null)
            {
                return new FailedServiceResponse<RandomedUser>()
                    .SetTitle("Error fetching random user data")
                    .SetErrorData(httpResponse)
                    .SetDetail($"Successfully fetched user data but no user data was received.");
            }

            var result = new ServiceResponse<RandomedUser>(user);

            return result;
        }

        public async Task<ServiceResponse<AuthResponse>> RegisterTestAcc(List<RolesEnum> roleId)
        {
            var randomUserResponse = await GetRandomUser();
            if (!randomUserResponse.Success)
            {
                return new FailedServiceResponse<AuthResponse>(randomUserResponse.Error)
                    .SetTitle("Error registering test account")
                    .SetDetail($"Error generating random user. See {ServiceResponse.ErrorKey} for more details");
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
                return new FailedServiceResponse<AuthResponse>(authenticateResponse);
            }

            var resultData = authenticateResponse.Data;
            var result = new ServiceResponse<AuthResponse>(resultData);

            return result;
        }
    }
}