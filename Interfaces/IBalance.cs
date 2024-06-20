using AtmEquityProject.Models;

namespace AtmEquityProject.Interfaces
{
    public interface IBalance
    {
        ICollection<Balance> GetBalances();
        Balance GetBalance(int idBalance);
        bool BalanceExists(int idBalance);
        bool CreateBalance(Balance balance);
        bool UpdateBalance(Balance balance);
        bool DeleteBalance(int idBalance);
        bool Save();
        bool DeleteBalance(Balance balanceToDelete);
    }
}
