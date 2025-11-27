using AspnetcoreUserManagement.Data;
using AspnetcoreUserManagement.Models.Entities;
using AspnetcoreUserManagement.Models.ViewModels;

namespace AspnetcoreUserManagement.Manageres
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ReplaceAllUsersAsync(List<UserViewModel> models)
        {
            //Can not replace users with an empty list
            //think if it should throw something?
            if (models == null || !models.Any())
            {                
                return;
            }

            // Delete old users
            await _repository.DeleteAllAsync();            

            // Insert new users
            foreach (var u in models)
            {
                double.TryParse(u.Lat, out double lat);
                double.TryParse(u.Lng, out double lng);

                var user = new User
                {
                    Name = u.Name,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Phone = u.Phone,
                    Website = u.Website,
                    Note = u.Note,
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

                await _repository.InsertUserAsync(user);
            }
        }
    }
}
