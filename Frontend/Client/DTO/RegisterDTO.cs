﻿namespace Frontend.Client.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; } = "Active";
    }
}
