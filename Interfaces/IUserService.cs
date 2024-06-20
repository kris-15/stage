using AtmEquityProject.Models;

namespace AtmEquityProject.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        User GetUserById(int id);
        Task<User?> AddAndUpdateUser(User userObj);
        User GetUserInfo(string token);
    }
}
