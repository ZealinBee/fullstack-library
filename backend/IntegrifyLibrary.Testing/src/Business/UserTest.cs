// using AutoMapper;
// using IntegrifyLibrary.Business;
// using IntegrifyLibrary.Domain;
// using Moq;
// using System;
// using Xunit;

// namespace IntegrifyLibrary.Testing.Business;

// public class UserTest
// {
//     private readonly Mock<IUserRepo> _mockUserRepo;
//     private readonly IMapper _mapper;

//     public UserTest()
//     {
//         _mockUserRepo = new Mock<IUserRepo>();
//         _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
//     }

//     [Fact]
//     public void CreateOne_Should_Create_New_User_Successfully()
//     {
//         var userService = new UserService(_mockUserRepo.Object, _mapper);
//         var createDto = new CreateUserDto
//         {
//             FirstName = "Zhiyuan",
//             LastName = "Liu",
//             Email = "zhiyuanliu@mail.com",
//             Password = "123456"
//         };
//         var createdUser = _mapper.Map<User>(createDto);

//         _mockUserRepo.Setup(repo => repo.CreateOne(It.IsAny<User>())).Returns(createdUser);

//         var result = userService.CreateOne(createDto);

//         Assert.NotNull(result);
//         Assert.Equal(createDto.FirstName, result.FirstName);
//         Assert.Equal(createDto.LastName, result.LastName);
//         Assert.Equal(createDto.Email, result.Email);
//     }

//     [Fact]
//     public void LoginUser_Wrong_Credentials_Error()
//     {
//         var authService = new AuthService(_mockUserRepo.Object);
//         var loginDto = new LoginUserDto
//         {
//             Email = "wrongemail@mail.com",
//             Password = "wrongpassword"
//         };

//         Assert.Throws<Exception>(() => authService.VerifyCredentials(loginDto));
//     }

// }