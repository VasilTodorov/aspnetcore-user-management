using AspnetcoreUserManagement.Models.DTOs;
using System.Text.Json;

namespace AspnetcoreUserManagement.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _client;
        public UserApiService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<UserDto>> GetUsersAsync()
        {
            var response = await _client.GetFromJsonAsync<List<UserDto>>("users");
            if( response is null)
                response = new List<UserDto>();
            
            return response;
        }
    }
}
