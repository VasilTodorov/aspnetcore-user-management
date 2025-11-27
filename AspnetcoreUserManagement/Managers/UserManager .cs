using AspnetcoreUserManagement.Data;
using AspnetcoreUserManagement.Helpers;
using AspnetcoreUserManagement.Models.Entities;
using AspnetcoreUserManagement.Models.ViewModels;
using AspnetcoreUserManagement.Services;

namespace AspnetcoreUserManagement.Manageres
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;
        private readonly IUserApiService _service;

        public UserManager(IUserRepository repository, IUserApiService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<List<UserViewModel>> LoadUserViews()
        {
            var userDTOs = await _service.GetUsersAsync();
            var userViews = userDTOs.ToViewModels();
            return userViews;
        }

        public async Task ReplaceAllUsersAsync(List<UserViewModel> models)
        {
            if (models == null || !models.Any())
                return;

            var users = models.Select(u => {
                double.TryParse(u.Lat, out double lat);
                double.TryParse(u.Lng, out double lng);

                return new User
                {
                    Name = u.Name,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Phone = u.Phone,
                    Website = u.Website,
                    Note = u.Note ?? string.Empty,
                    IsActive = u.IsActive ? (byte)1 : (byte)0,
                    CreatedAt = DateTime.UtcNow,
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            Street = u.Street,
                            Suite = u.Suite,
                            City = u.City,
                            Zipcode = u.Zipcode,
                            Lat = lat,
                            Lng = lng
                        }
                    }
                };
            }).ToList();

            // one call, one transaction inside
            await _repository.ReplaceAllUsersAsync(users);
        }

    }
}
