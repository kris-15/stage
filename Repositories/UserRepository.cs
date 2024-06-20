using AtmEquityProject.Data;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;

namespace AtmEquityProject.Repositories
{
    public class UserRepository : IUser
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        bool IUser.DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        bool IUser.UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
