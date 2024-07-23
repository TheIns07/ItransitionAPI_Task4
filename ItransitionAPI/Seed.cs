using ItransitionAPI.Data;
using ItransitionAPI.Models;

namespace ItransitionAPI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Identification = new Guid(),
                    Email = "Manuel@gmail.com",
                    LastLoginTime = DateTime.Now,
                    RegistrationTime = DateTime.Now,
                    Name = "Manuel",
                    Status = "Active",
                    Password = "Password",

                },
                new User()
                {
                    Identification = new Guid(),
                    Email = "John@gmail.com",
                    LastLoginTime = DateTime.Now,
                    RegistrationTime = DateTime.Now,
                    Name = "John",
                    Status = "Blocked",
                    Password = "Password"

                }
            };
            dataContext.Users.AddRange(users);
            dataContext.SaveChanges();
        }
    }
}
