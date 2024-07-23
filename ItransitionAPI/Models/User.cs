using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ItransitionAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Identification { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(255, ErrorMessage = "Email can't be longer than 255 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Last login time is required")]
        public DateTime LastLoginTime { get; set; } 

        [Required(ErrorMessage = "Registration time is required")]
        public DateTime RegistrationTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters")]
        public string Status { get; set; } = "Active";

        [Required(ErrorMessage = "Status is required")]
        public string Password { get; set; }
    }
}
