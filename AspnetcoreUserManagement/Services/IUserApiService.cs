using AspnetcoreUserManagement.Models.DTOs;

namespace AspnetcoreUserManagement.Services
{
    public interface IUserApiService
    {
        Task<List<UserDto>> GetUsersAsync();
    }
}
