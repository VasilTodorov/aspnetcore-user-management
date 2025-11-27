using System.ComponentModel.DataAnnotations;

namespace AspnetcoreUserManagement.Models.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Password must be at least 6 and less than 200 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password must contain upper and lower case letters, a number, and a special character.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required.")]        
        [Phone(ErrorMessage = "Invalid phone format.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Website is required.")]
        [RegularExpression(@"^(https?://)?([a-z0-9\-]+\.)+[a-z]{2,6}(/.*)?$",
            ErrorMessage = "Invalid website URL.")]
        public string Website { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Suite is required.")]
        public string Suite { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zipcode is required.")]
        [StringLength(10, ErrorMessage = "Zipcode cannot exceed 10 characters.")]
        public string Zipcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Latitude is required.")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Invalid latitude format.")]
        public string Lat { get; set; } = string.Empty;

        [Required(ErrorMessage = "Longitude is required.")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Invalid longitude format.")]
        public string Lng { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string? Note { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

}
