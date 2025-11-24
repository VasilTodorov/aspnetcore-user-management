using AspnetcoreUserManagement.Models.DTOs;
using AspnetcoreUserManagement.Models.ViewModels;
using AspnetcoreUserManagement.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace AspnetcoreUserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _uesrService;

        public UserController(IUserService userService)
        {
            _uesrService = userService;
        }
        public IActionResult Index()
        {
            return View(new UsersPageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoadUsers()
        {
            var userDTOs = await _uesrService.GetUsersAsync();
            var userViews = userDTOs.Select(user => new UserViewModel
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Website = user.Website,
                Street = user.Address.Street,
                Suite = user.Address.Suite,
                City = user.Address.City,
                Zipcode = user.Address.Zipcode,
                Lat = user.Address.Geo.Lat,
                Lng = user.Address.Geo.Lng,
                Note = "",
                Password = "",
                IsActive = false
            }).ToList();
            var vm = new UsersPageViewModel
            {
                Users = userViews
            };

            return View("Index", vm);
        }
        public async Task<IActionResult> Manage()
        {
            var users = await _uesrService.GetUsersAsync();
            return View(users);
        }
    }
}
