using AspnetcoreUserManagement.Helpers;
using AspnetcoreUserManagement.Models.DTOs;
using AspnetcoreUserManagement.Models.ViewModels;
using AspnetcoreUserManagement.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace AspnetcoreUserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _uesrService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _uesrService = userService;
            _configuration = configuration;
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
        [HttpPost]
        public async Task<IActionResult> SaveAll(UsersPageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                // Handle validation errors
                TempData["Error"] = $"Error in ModelState: {ModelState.Count}";
                return View("Index", model);
            }
            // 1️⃣ Validation
            if (model.Users == null || !model.Users.Any())
            {
                TempData["Error"] = "No users to save.";
                return RedirectToAction("Index");
            }

            if (model.Users.Any(u => string.IsNullOrWhiteSpace(u.Password)))
            {
                TempData["Error"] = "Password is required for all users.";
                return RedirectToAction("Index");
            }

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // 2️⃣ Begin Transaction
            using var transaction = connection.BeginTransaction();

            try
            {
                // 3️⃣ Clear existing data
                await connection.ExecuteAsync("DELETE FROM Addresses", transaction: transaction);
                await connection.ExecuteAsync("DELETE FROM Users", transaction: transaction);

                // 4️⃣ Insert Users and Addresses
                foreach (var u in model.Users)
                {
                    // Insert User and get new Id
                    var userInsertSql = @"
                INSERT INTO Users (Name, Username, Password, Email, Phone, Website, Note, IsActive, CreatedAt)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Username, @Password, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt)";

                    //ToDo investigate note
                    string note;
                    var userId = await connection.ExecuteScalarAsync<int>(userInsertSql, new
                    {
                        u.Name,
                        u.Username,
                        u.Password,
                        u.Email,
                        u.Phone,
                        u.Website,
                        note = u.Note ?? string.Empty,
                        IsActive = u.IsActive ? 1 : 0,
                        CreatedAt = DateTime.UtcNow
                    }, transaction);

                    // Insert Address with foreign key
                    var addressInsertSql = @"
                INSERT INTO Addresses (Street, Suite, City, Zipcode, Lat, Lng, UserId)
                VALUES (@Street, @Suite, @City, @Zipcode, @Lat, @Lng, @UserId)";

                    await connection.ExecuteAsync(addressInsertSql, new
                    {
                        u.Street,
                        u.Suite,
                        u.City,
                        u.Zipcode,
                        Lat = double.TryParse(u.Lat, out var latVal) ? latVal : 0,
                        Lng = double.TryParse(u.Lng, out var lngVal) ? lngVal : 0,
                        UserId = userId
                    }, transaction);
                }

                // 5️⃣ Commit Transaction
                transaction.Commit();

                TempData["Success"] = "All users saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                TempData["Error"] = $"Error saving users: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Manage()
        {
            var users = await _uesrService.GetUsersAsync();
            return View(users);
        }
    }
}
