using System.Collections.ObjectModel;

namespace Name_Here
{

    /// <summary>
    ///     this should read-write to a database 
    ///     
    ///     mocking results till further along on that front
    /// </summary>
    public class Repository
    {
        // fake
        private readonly static List<User> _users = new List<User>
        {
            new User
            {
                Email = "test0@gmail.com",
                Role = Role.Admin
            },
             new User
            {
                Email = "test1@gmail.com",
                Role = Role.User
            },
             new User
            {
                Email = "test2@gmail.com",
                Role = Role.Editor
            },
             new User
            {
                Email = "test3@gmail.com",
                Role = Role.PowerUser
            }
        };

        // for convienance
        public static ReadOnlyCollection<User> Users { get; set; } = _users.AsReadOnly();

        public User GetUser(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public bool AddUser(User user)
        {
            _users.Add(user);
            return true;
        }

        public bool UpdateUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Email == user.Email);
            if (existing != null) _users.Remove(existing);
            _users.Add(existing);
            return true;
        }
    }
}
