using SecretSharing.Dtos;
using SecretSharing.Test;
using System.Net.Http.Json;

namespace SecretSharingTest
{
    public class AccountControllerTests
    {

        private readonly TestApplication _application;
        private readonly HttpClient _client;
        public AccountControllerTests()
        {
            _application = new TestApplication();
            _client = _application.CreateClient();
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

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/register", user);
            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);

        }

        [Fact]
        public async void Register_Failed_Existing_Email()
        {
            // Arrange
            var user = new RegisterDto
            {
                Email = "ginta2888@gmail.com",
                Password = "To@n0112",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/register", user);

            // Assert
            Assert.False(response.IsSuccessStatusCode);

        }

        [Fact]
        public async void Register_Failed_Simple_Password()
        {
            // Arrange
            var user = new RegisterDto
            {
                Email = "ginta2777@gmail.com",
                Password = "Toan",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/register", user);

            // Assert
            Assert.False(response.IsSuccessStatusCode);

        }

        [Fact]
        public async void Login_Failed_Non_Existing_User()
        {
            // Arrange
            var user = new LoginDto
            {
                Email = "ginta2777@gmail.com",
                Password = "Toan",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/login", user);

            // Assert
            Assert.False(response.IsSuccessStatusCode);

        }

        [Fact]
        public async void Login_Success()
        {
            // Arrange
            var user = new LoginDto
            {
                Email = "ginta2888@gmail.com",
                Password = "To@n0112",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/login", user);
            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);

        }

        [Fact]
        public async void Login_Failed_Wrong_Password()
        {
            // Arrange
            var user = new RegisterDto
            {
                Email = "ginta2888@gmail.com",
                Password = "To@n011",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/register", user);


            // Assert
            Assert.False(response.IsSuccessStatusCode);


        }


    }


}
