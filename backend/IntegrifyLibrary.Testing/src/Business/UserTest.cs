using AutoMapper;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Domain;
using Moq;
using System;
using Xunit;

namespace IntegrifyLibrary.Testing.Business;

public class UserTest
{
    private readonly Mock<IUserRepo> _mockUserRepo;
    private readonly IMapper _mapper;

    public UserTest()
    {
        _mockUserRepo = new Mock<IUserRepo>();
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
    }

    [Fact]
    public async Task CreateOne_RightCredentials_Successfully()
    {
        var userService = new UserService(_mockUserRepo.Object, _mapper);
        var createDto = new CreateUserDto
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "zhiyuanliu@mail.com",
            Password = "123456"
        };
        var createdUser = _mapper.Map<User>(createDto);

        _mockUserRepo.Setup((repo) => repo.CreateOne(It.IsAny<User>())).ReturnsAsync(createdUser);

        var result = await userService.CreateOne(createDto);

        Assert.NotNull(result);
        Assert.Equal(createDto.FirstName, result.FirstName);
        Assert.Equal(createDto.LastName, result.LastName);
        Assert.Equal(createDto.Email, result.Email);
    }

    public async Task MakeUserAdmin_IfExists_Successfully()
    {
        var userService = new UserService(_mockUserRepo.Object, _mapper);
        string userEmail = "zhiyuanliu@mail.com";
        var createDto = new CreateUserDto
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = userEmail,
            Password = "123456"
        };
        var createdUser = _mapper.Map<User>(createDto);
        _mockUserRepo.Setup((repo) => repo.CreateOne(It.IsAny<User>())).ReturnsAsync(createdUser);
        userService.MakeUserLibrarian(userEmail);
        Assert.Equal(Role.Librarian, createdUser.Role);
    }

    [Fact]
    public void LoginUser_WrongCredentials_Error()
    {
        var authService = new AuthService(_mockUserRepo.Object);
        var loginDto = new LoginUserDto
        {
            Email = "wrongemail@mail.com",
            Password = "wrongpassword"
        };

        Assert.ThrowsAsync<Exception>(() => authService.VerifyCredentials(loginDto));
    }
}