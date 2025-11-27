using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspnetcoreUserManagement.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty; // nvarchar(max)

        public string Note { get; set; } = string.Empty; // nvarchar(max)

        public byte IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
