namespace AspnetcoreUserManagement.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public byte IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Address> Addresses { get; set; } = default!;
    }
}
