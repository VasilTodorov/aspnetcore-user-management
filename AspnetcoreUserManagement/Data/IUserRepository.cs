using AspnetcoreUserManagement.Models.Entities;

namespace AspnetcoreUserManagement.Data
{
    public interface IUserRepository
    {
        Task DeleteAllAsync();
        Task InsertUserAsync(User user);
    }
}
