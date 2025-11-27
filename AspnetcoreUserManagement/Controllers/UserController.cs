using AspnetcoreUserManagement.Helpers;
using AspnetcoreUserManagement.Models.DTOs;
using AspnetcoreUserManagement.Models.ViewModels;
using AspnetcoreUserManagement.Models.Entities;
using AspnetcoreUserManagement.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;
using AspnetcoreUserManagement.Data;
using AspnetcoreUserManagement.Manageres;

namespace AspnetcoreUserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiService _uesrService;
        private readonly IUserManager _userManager;

        public UserController(IUserApiService userService, IUserManager userManager)
        {
            _uesrService = userService;
            _userManager = userManager;            
        }
        public IActionResult Index()
        {
            return View(new UsersPageViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> LoadUsers()
        {
            var userDTOs = await _uesrService.GetUsersAsync();        
            var userViews = userDTOs.ToViewModels();

            var vm = new UsersPageViewModel
            {
                Users = userViews
            };

            return View("Index", vm);
        }
        
        public async Task<IActionResult> SaveAll(UsersPageViewModel model)
        {           
            if (!ModelState.IsValid)
            {
                // Return the same view with validation errors
                TempData["Error"] = $"Error in ModelState";
                return View("Index", model);
            }
            
            await _userManager.ReplaceAllUsersAsync(model.Users);

            TempData["Success"] = "All users saved successfully!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Manage()
        {
            var users = await _uesrService.GetUsersAsync();
            return View(users);
        }
    }
}
