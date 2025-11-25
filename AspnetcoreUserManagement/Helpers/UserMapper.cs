using AspnetcoreUserManagement.Models.DTOs;
using AspnetcoreUserManagement.Models.ViewModels;

namespace AspnetcoreUserManagement.Helpers
{
    public static class UserMapper
    {
        /// <summary>
        /// Maps a list of UserDto to a list of UserViewModel.
        /// </summary>
        public static List<UserViewModel> ToViewModels(this IEnumerable<UserDto> userDtos)
        {
            if (userDtos == null) return new List<UserViewModel>();

            return userDtos.Select(user => new UserViewModel
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Website = user.Website,
                Street = user.Address?.Street ?? string.Empty,
                Suite = user.Address?.Suite ?? string.Empty,
                City = user.Address?.City ?? string.Empty,
                Zipcode = user.Address?.Zipcode ?? string.Empty,
                Lat = user.Address?.Geo?.Lat ?? string.Empty,
                Lng = user.Address?.Geo?.Lng ?? string.Empty,
                Note = string.Empty,       // default empty
                Password = string.Empty,   // default empty
                IsActive = false           // default false
            }).ToList();
        }
    }
}
