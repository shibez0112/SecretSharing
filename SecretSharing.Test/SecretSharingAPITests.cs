using SecretSharing.Dtos;
using SecretSharing.Test;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SecretSharingTest
{
    [Collection("AuthenticationTestCollection")]
    public class SecretSharingAPITests
    {

        private readonly TestApplication _application;
        private readonly HttpClient _client;
        private readonly AuthenticationTestFixture _fixture;
        public SecretSharingAPITests(AuthenticationTestFixture fixture)
        {
            _application = fixture._application;
            _client = _application.CreateClient();
            _fixture = fixture;
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

        [Fact]
        public async void Upload_Text_Failed_Unauthorized()
        {
            // Arrange
            string userContent = "Testing in progress";

            // Act
            var response = await _client.PostAsJsonAsync("/api/Text/UploadText", new { content = userContent, isAutoDeleted = true });

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void Upload_Text_Success()
        {
            // Arrange
            var url = "/api/Text/UploadText?content=Haha&isAutoDeleted=true";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.AuthenticationToken);

            // Act
            var response = await _client.PostAsync(url, null);
            var result = response.Content;

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);

        }

        [Fact]
        public async void Upload_Another_Text_Success()
        {
            // Arrange
            var url = "/api/Text/UploadText?content=HahaHa&isAutoDeleted=false";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.AuthenticationToken);

            // Act
            var response = await _client.PostAsync(url, null);
            var result = response.Content;

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);

        }

        [Fact]
        public async void Get_List_Text_Return_2()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.AuthenticationToken);

            // Act
            var response = await _client.GetAsync(("/api/Text/ListUserText"));
            var result = await response.Content.ReadFromJsonAsync<List<UserFileDto>>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(result);
            Assert.Equal(result.Count(), 2);

        }

        public async void Access_Text_Then_Auto_Delete()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.AuthenticationToken);
            // Get list of text
            var listResponse = await _client.GetAsync(("/api/Text/ListUserText"));
            var listResult = await listResponse.Content.ReadFromJsonAsync<List<UserFileDto>>();
            // Get id of the one that will be deleted after access
            var textId = listResult[0].Id.ToString();

            // Act
            // Access into the text
            var accessResponse = await _client.GetAsync("/api/Text/AccessUserText?textId=" + textId);
            // List all the text again
            listResponse = await _client.GetAsync(("/api/Text/ListUserText"));
            listResult = await listResponse.Content.ReadFromJsonAsync<List<UserFileDto>>();

            // Assert
            Assert.True(listResponse.IsSuccessStatusCode);
            Assert.NotNull(listResult);
            // Now one text is auto deleted, so there will be only 1 text
            Assert.Equal(listResult.Count(), 1);

        }


    }


}
