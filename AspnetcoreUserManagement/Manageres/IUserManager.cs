using AspnetcoreUserManagement.Models.ViewModels;

namespace AspnetcoreUserManagement.Manageres
{
    public interface IUserManager
    {
        Task ReplaceAllUsersAsync(List<UserViewModel> models);
    }
}
