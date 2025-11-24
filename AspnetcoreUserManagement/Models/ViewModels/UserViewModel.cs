namespace AspnetcoreUserManagement.Models.ViewModels
{
    public class UserViewModel
    {        
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Note { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
