using AspnetcoreUserManagement.Models.DTOs;

namespace AspnetcoreUserManagement.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
    }
}
