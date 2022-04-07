using AutoMapper;
using Moq;
using StoreAPI.Persistence.Repositories;
using StoreAPI.Services;
using StoreAPI.Services.Communication;
using StoreAPI.TestUser;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Store.API.UnitTests
{
    public class TestAccountServiceTests : IClassFixture<ConfigurationFixture>
    {
        public Mock<TestAccountService> _mockTestAccountService;
        public IConfigurationProvider _configurationProvider { get; set; }
        public IMapper _mapper;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IRoleService> _roleServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public TestAccountServiceTests(ConfigurationFixture configurationFixture)
        {
            _authServiceMock = new Mock<IAuthService>();
            _userServiceMock = new Mock<IUserService>();
            _roleServiceMock = new Mock<IRoleService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = configurationFixture.Mapper;

            _mockTestAccountService = new Mock<TestAccountService>(
                _authServiceMock.Object, _userServiceMock.Object,
                _roleServiceMock.Object, _unitOfWorkMock.Object, _mapper);
        }


        [Fact]
        public async Task GetRandomUser_ReturnsUserRegisterDto_WhenFetchUserFails()
        {
            // Arrange
            var fetchFailedResponse = new FailedServiceResponse<RandomedUser>();

            _mockTestAccountService.Setup(s => s.FetchUser()).ReturnsAsync(fetchFailedResponse);

            var service = _mockTestAccountService.Object;

            // Act
            var result = await service.GetRandomUser();
            var userDto = result.Data;


            // Assert
            Assert.True(result.Success);
            Assert.True(userDto.UserName.Contains("user") &&
                userDto.FirstName.Contains("TestUser") &&
                userDto.LastName.Contains("LastName"));
        }


        [Fact]
        public async Task GetRandomUser_ReturnsUserRegisterDto_WhenFetchUserSucceeds()
        {
            // Arrange

            var dob = new Dob { Date = new DateTime(2000, 1, 1) };
            var name = new Name { First = "TestFirstName", Last = "TestLastName" };

            var responseData = new RandomedUser
            {
                Dob = dob,
                Name = name
            };

            var fetchSuccessResponse = new ServiceResponse<RandomedUser>(responseData);

            _mockTestAccountService.Setup(s => s.FetchUser()).ReturnsAsync(fetchSuccessResponse);

            var service = _mockTestAccountService.Object;

            var expectedOnUsername = name.First.ToLower();

            // Act
            var result = await service.GetRandomUser();
            var userDto = result.Data;

            // Assert
            Assert.True(result.Success);
            Assert.Contains(expectedOnUsername, userDto.UserName);
            Assert.Matches(@"\d", userDto.UserName);
            Assert.Contains(name.First, userDto.FirstName);
            Assert.Contains(name.Last, userDto.LastName);
        }
    }
}