using Name_Here.Models;

namespace Name_Here.Repositories
{
    public interface IRepository
    {
        bool AddUser(AppUser user);
        AppUser GetUser(string email);
        bool UpdateUser(AppUser user);
    }
}