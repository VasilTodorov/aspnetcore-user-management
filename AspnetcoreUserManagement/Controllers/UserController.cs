using AspnetcoreUserManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AspnetcoreUserManagement.Manageres;

namespace AspnetcoreUserManagement.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserManager _userManager;

        public UserController( IUserManager userManager)
        {           
            _userManager = userManager;            
        }
        public IActionResult Index()
        {
            return View(new UsersPageViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> LoadUsers()
        {            
            var userViews = await _userManager.LoadUserViews();

            var vm = new UsersPageViewModel
            {
                Users = userViews
            };

            return View("Index", vm);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAll(UsersPageViewModel model)
        {   
            //ToDo make validation
            if (!ModelState.IsValid)
            {
                // Return the same view with validation errors
                TempData["Error"] = $"Error in ModelState";
                return View("Index", model);
            }

            try
            {
                await _userManager.ReplaceAllUsersAsync(model.Users);
                TempData["Success"] = "All users saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error couldn't save all";
                return View("Index", model);
            }
                                                          
        }
        
    }
}
