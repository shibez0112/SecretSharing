using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SecretSharing.Controllers;
using SecretSharing.Core.Entities.Identity;
using SecretSharing.Core.Interfaces;
using SecretSharing.Dtos;

namespace SecretSharingTest
{
    public class AccountControllerTests
    {
        private readonly Mock<ITokenService> tokenService;
        private readonly Mock<UserManager<AppUser>> userManager;
        private readonly Mock<SignInManager<AppUser>> signInManager;
        public AccountControllerTests()
        {
            tokenService = new Mock<ITokenService>();

            userManager = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<AppUser>>().Object,
                new IUserValidator<AppUser>[0],
                new IPasswordValidator<AppUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<AppUser>>>().Object);

            signInManager = new Mock<SignInManager<AppUser>>(
                userManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<AppUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                null);
        }

        [Fact]
        public async void Register_Success()
        {
            // Arrange
            var user = new RegisterDto
            {
                Email = "ginta2888@gmail.com",
                Password = "To@n0112",
            };

            userManager
                .Setup(userManager => userManager.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));


            var accountController = new AccountController(signInManager.Object, userManager.Object, tokenService.Object);

            // Act
            var registerResult = await accountController.Register(user);

            // Assert
            Assert.NotNull(registerResult);
            Assert.Equal("ginta2888@gmail.com", registerResult.Value.Email);

        }

        [Fact]
        public async void Register_Failed_Create_User()
        {
            // Arrange
            var user = new RegisterDto
            {
                Email = "ginta2888@gmail.com",
                Password = "Toan0112",
            };

            userManager
                .Setup(userManager => userManager.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Failed to register user" })));


            var accountController = new AccountController(signInManager.Object, userManager.Object, tokenService.Object);

            // Act
            var registerResult = await accountController.Register(user);

            // Assert
            Assert.NotNull(registerResult);
            // Assert that the response is a BadRequestObjectResult
            Assert.IsType<BadRequestObjectResult>(registerResult.Result);

        }


    }




}
