using AtmEquityProject.Models;

namespace AtmEquityProject.Interfaces
{
    public interface IUser
    {
        ICollection<User> GetUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
