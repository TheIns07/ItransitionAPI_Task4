using System.ComponentModel.DataAnnotations;

namespace Frontend.Client.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public Guid Identification { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Status { get; set; }
        public bool IsSelected { get; set; }
    }
}
