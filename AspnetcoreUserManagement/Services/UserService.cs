using AspnetcoreUserManagement.Models.DTOs;
using System.Text.Json;

namespace AspnetcoreUserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        public UserService(HttpClient client)
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
