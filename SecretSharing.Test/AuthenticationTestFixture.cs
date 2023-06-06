using SecretSharing.Dtos;
using System.Net.Http.Json;

namespace SecretSharing.Test
{
    public class AuthenticationTestFixture : IAsyncLifetime
    {
        public string AuthenticationToken { get; private set; }
        public TestApplication _application { get; set; }
        private HttpClient _client { get; set; }

        public async Task InitializeAsync()
        {
            _application = new TestApplication();
            _client = _application.CreateClient();
            AuthenticationToken = await PerformLoginAndGetToken();
        }

        public Task DisposeAsync()
        {
            // Clean up resources used by the test
            return Task.CompletedTask;
        }

        private async Task<string> PerformLoginAndGetToken()
        {
            // Arrange
            var user = new RegisterDto
            {
                Email = "ginta2111@gmail.com",
                Password = "To@n0112",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/register", user);
            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            return result.Token;
        }
    }

    [CollectionDefinition("AuthenticationTestCollection")]
    public class AuthenticationTestCollection : ICollectionFixture<AuthenticationTestFixture>
    {
        // No additional code needed here
    }
}
