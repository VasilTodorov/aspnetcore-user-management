namespace AspnetcoreUserManagement.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Suite { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
