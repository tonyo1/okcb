using System.Collections.ObjectModel;
using Name_Here.Models;

namespace Name_Here.Repositories
{

    /// <summary>
    ///     this should read-write to a database 
    ///     
    ///     mocking results till further along on that front
    /// </summary>
    public class Repository : IRepository
    {
        // fake
        private readonly static List<AppUser> _users = new List<AppUser>
        {
            new AppUser
            {
                Email = "test0@gmail.com",
                Role = Roles.Admin
            },
             new AppUser
            {
                Email = "test1@gmail.com",
                Role = Roles.RegisteredUser
            },
             new AppUser
            {
                Email = "test2@gmail.com",
                Role = Roles.PowerUser
            },
             new AppUser
            {
                Email = "test3@gmail.com",
                Role = Roles.Fighter
            }
        };

        // for convienance
        public static ReadOnlyCollection<AppUser> Users { get; set; } = _users.AsReadOnly();

        public AppUser GetUser(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public bool AddUser(AppUser user)
        {
            _users.Add(user);
            return true;
        }

        public bool UpdateUser(AppUser user)
        {
            var existing = _users.FirstOrDefault(u => u.Email == user.Email);
            if (existing != null) _users.Remove(existing);
            _users.Add(existing);
            return true;
        }
    }
}
