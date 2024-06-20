using AtmEquityProject.Data;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AtmEquityProject.Repositories
{
    public class BalanceRepository : IBalance
    {
        private readonly DataContext _context;
        public BalanceRepository(DataContext context) 
        {
            _context = context;
        }
        public ICollection<Balance> GetBalances()
        {
            return _context.Balances.ToList();
        }
        public Balance GetBalance(int id)
        {
            return _context.Balances.Where(b => b.Id == id).FirstOrDefault();
        }
        public bool BalanceExists(int id)
        {
            return _context.Balances.Any(b => b.Id == id);
        }
        public bool CreateBalance(Balance balance)
        {
            _context.Add(balance);
            return Save();
        }
        public bool DeleteAtm(Balance balance)
        {
            _context.Remove(balance);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteBalance(int idBalance)
        {
            _context.Remove(idBalance);
            return Save();
        }

        public bool DeleteBalance(Balance balanceToDelete)
        {
            _context.Remove(balanceToDelete);
            return Save();
        }

        public bool UpdateBalance(Balance balance)
        {
            _context.Update(balance);
            return Save();
        }
    }
}

