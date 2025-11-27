using AspnetcoreUserManagement.Models.ViewModels;

namespace AspnetcoreUserManagement.Manageres
{
    public interface IUserManager
    {
        Task<List<UserViewModel>> LoadUserViews(); 
        Task ReplaceAllUsersAsync(List<UserViewModel> models);
    }
}
