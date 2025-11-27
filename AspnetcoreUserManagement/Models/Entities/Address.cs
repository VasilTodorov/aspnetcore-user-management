using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetcoreUserManagement.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Suite { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Zipcode { get; set; } = string.Empty;

        public double Lat { get; set; }
        public double Lng { get; set; }

        // Foreign key
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; } = default!;
    }
}
