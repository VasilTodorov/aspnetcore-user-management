using AspnetcoreUserManagement.Models.DTOs;

namespace AspnetcoreUserManagement.Models.ViewModels
{
    public class UsersPageViewModel
    {
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
